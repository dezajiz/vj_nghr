using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using vjt.blur;
using vjt.glitch;
namespace vjt.Main
{
	/* 
		* --- 全ての統括クラス --- *

		・それぞれの素材はGameObjectで管理
		・各素材の手前に3D上に暗幕を置いてそれをフェードでいじる

		[TODO]
		・素材いれていく
	
		by nagahora
	 */


	public class Main : MonoBehaviour {

		/// <summary>
		/// MIDIコンソース
		/// フェード・ノブ　0 - 127
		/// </summary>
		/// <returns></returns>
		[SerializeField] private NanoKontrol2 _nanoKon;


		/// <summary>
		/// マイク入力
		/// </summary>
		[SerializeField] private MicInput _micInput;

		/// <summary>
		/// 07 blur
		/// </summary>
		[SerializeField] private BlurController _07blur;

		
		/// <summary>
		/// 00 glitch
		/// </summary>
		[SerializeField] private GlitchController _00glitch;

		/// <summary>
		/// Start
		/// </summary>
		void Start ()
		{
			//
			// MIDIコン
			_nanoKon.valueChangedFunctions.Add (nanoKontrol2_valueChanged);
			_nanoKon.keyPushedFunctions.Add (nanoKontrol2_keyPushed);

			//
			// ブラー
			_07blur.Init();

			//
			// glitch
			_00glitch.Init();

			// 同時Tweenの制限を増やす
			LeanTween.init(1500);
		}


		/// <summary>
		/// キー（スライダー・ノブ）の値が変更された場合に呼び出される関数
		/// </summary>
		/// <param name="keyName"></param>
		/// <param name="keyValue"></param>
		public void nanoKontrol2_valueChanged(string keyName, int keyValue)
		{
			// Debug.Log(keyName);
			// Debug.Log(keyValue);

			switch (keyName)
			{
				case "Slider1":
					break;

				case "Slider2":
					break;

				case "Slider3":
					break;
				case "Slider4":
					break;
				case "Slider5":
					_00glitch.SetColorDrift(keyValue);
					break;

				case "Slider6":
					_00glitch.SetHorizontalShake(keyValue);
					break;

				case "Slider7":
					_00glitch.SetVerticalJump(keyValue);
					break;

				case "Slider8":
					_00glitch.SetScanLineJitter(keyValue);
					break;


				case "Knob1":
					break;

				case "Knob2":
					break;

				case "Knob3":
					break;

				case "Knob4":
					break;

				case "Knob5":
					break;

				case "Knob6":
					break;

				case "Knob7":
					_07blur.SetAlpha(keyValue);
					break;
				
				case "Knob8":
					_micInput.threshold = keyValue;
					break;
			}
		}
	

		/// <summary>
		/// キーが押された場合に呼び出される関数
		/// </summary>
		/// <param name="keyName"></param>
		public void nanoKontrol2_keyPushed(string keyName)
		{
			switch (keyName)
			{
				case "S1":
					break;

				case "M1":
					break;

				case "R1":
					break;

				case "S2":
					break;

				case "M2":
					break;

				case "R2":

					break;

				case "S3":
					break;
				case "M3":
					break;
			}
		}
	

		/// <summary>
		/// アプデ
		/// </summary>
		void Update ()
		{
			// Debug.Log (_nanoKon.Slider1);
			//Debug.Log (nanoKontrol2.Slider2);
			//Debug.Log (nanoKontrol2.knob1);
			//Debug.Log (nanoKontrol2.knob2);

		}
	}
}
