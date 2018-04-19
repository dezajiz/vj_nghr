Shader "Custom/Dekoboko" {
	Properties {
        _Color ("Color", Color) = (1.0,1.0,1.0)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Strength ("Strength", Range(0.0, 1.0)) = 0.5
        _Fineness ("Fineness", Range(1.0, 100.0)) = 1.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        CGPROGRAM
        #pragma surface surf Lambert vertex:vert
        #pragma target 3.0

        fixed4 _Color;
        sampler2D _MainTex;
        float _Strength;
        float _Fineness;

        struct Input {
            float2 uv_MainTex;
        };

        float3 mod289(float3 x)
        {
            return x - floor(x / 289.0) * 289.0;
        }

        float2 mod289(float2 x)
        {
            return x - floor(x / 289.0) * 289.0;
        }

        float3 permute(float3 x)
        {
            return mod289((x * 34.0 + 1.0) * x);
        }

        float3 taylorInvSqrt(float3 r)
        {
            return 1.79284291400159 - 0.85373472095314 * r;
        }

        float snoise(float2 v)
        {
            const float4 C = float4( 0.211324865405187,  // (3.0-sqrt(3.0))/6.0
                                    0.366025403784439,  // 0.5*(sqrt(3.0)-1.0)
                                    -0.577350269189626,  // -1.0 + 2.0 * C.x
                                    0.024390243902439); // 1.0 / 41.0
            // First corner
            float2 i  = floor(v + dot(v, C.yy));
            float2 x0 = v -   i + dot(i, C.xx);

            // Other corners
            float2 i1;
            i1.x = step(x0.y, x0.x);
            i1.y = 1.0 - i1.x;

            // x1 = x0 - i1  + 1.0 * C.xx;
            // x2 = x0 - 1.0 + 2.0 * C.xx;
            float2 x1 = x0 + C.xx - i1;
            float2 x2 = x0 + C.zz;

            // Permutations
            i = mod289(i); // Avoid truncation effects in permutation
            float3 p =
            permute(permute(i.y + float3(0.0, i1.y, 1.0))
                            + i.x + float3(0.0, i1.x, 1.0));

            float3 m = max(0.5 - float3(dot(x0, x0), dot(x1, x1), dot(x2, x2)), 0.0);
            m = m * m;
            m = m * m;

            // Gradients: 41 points uniformly over a line, mapped onto a diamond.
            // The ring size 17*17 = 289 is close to a multiple of 41 (41*7 = 287)
            float3 x = 2.0 * frac(p * C.www) - 1.0;
            float3 h = abs(x) - 0.5;
            float3 ox = floor(x + 0.5);
            float3 a0 = x - ox;

            // Normalise gradients implicitly by scaling m
            m *= taylorInvSqrt(a0 * a0 + h * h);

            // Compute final noise value at P
            float3 g;
            g.x = a0.x * x0.x + h.x * x0.y;
            g.y = a0.y * x1.x + h.y * x1.y;
            g.z = a0.z * x2.x + h.z * x2.y;
            return 130.0 * dot(m, g);
        }

        float rand(float3 co)
        {
            return frac(sin(dot(co.xyz, float3(12.9898, 78.233, 56.787))) * 43758.5453);
        }

        float3 hsv_to_rgb(float3 HSV)
        {
            float3 RGB = HSV.z;
        
                float var_h = HSV.x * 6;
                float var_i = floor(var_h);   // Or ... var_i = floor( var_h )
                float var_1 = HSV.z * (1.0 - HSV.y);
                float var_2 = HSV.z * (1.0 - HSV.y * (var_h-var_i));
                float var_3 = HSV.z * (1.0 - HSV.y * (1-(var_h-var_i)));
                if      (var_i == 0) { RGB = float3(HSV.z, var_3, var_1); }
                else if (var_i == 1) { RGB = float3(var_2, HSV.z, var_1); }
                else if (var_i == 2) { RGB = float3(var_1, HSV.z, var_3); }
                else if (var_i == 3) { RGB = float3(var_1, var_2, HSV.z); }
                else if (var_i == 4) { RGB = float3(var_3, var_1, HSV.z); }
                else                 { RGB = float3(HSV.z, var_1, var_2); }
           
           return (RGB);
        }

        void vert(inout appdata_full v, out Input o )
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
			float2 position = UnityObjectToClipPos(v.vertex);
            float3 pos = v.vertex;
            float amp = snoise(v.texcoord * sin(_SinTime) * _Fineness) * _Strength;
            v.vertex.xyz = pos + v.normal * amp;
        }

        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb * _Color;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
