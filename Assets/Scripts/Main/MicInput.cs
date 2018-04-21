using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

[RequireComponent(typeof(AudioSource))]
public class MicInput : MonoBehaviour 
{

	/// <summary>
	/// バスドラのBangイベント
	/// </summary>
    public Action onBang;

    [SerializeField] private bool _isUseMic = false;


	[SerializeField] private float _threshold = 0.05f;
	public int threshold
	{
		set
		{
			_threshold = (float)value / 127;
			Debug.Log("mic threshold : " + _threshold);
		}
	}


    private AudioSource audio_;

    void Start()
    {
        audio_ = GetComponent<AudioSource>();

        if (_isUseMic)
        {
			//Name: Built-in Microphone
			//Name: Philips SHB5900
			//Name: USB2.0 MIC
			audio_.clip = Microphone.Start("USB PnP Sound Device", false, 1800, 44100);
			// audio_.clip = Microphone.Start(null, true, 10, 44100);
            // マイクが Ready になるまで待機（一瞬）
            // while (Microphone.GetPosition(null) <= 0) {}	
        }
		audio_.Play();
    }

    void Update() {

		float vol = GetAveragedVolume();
		if (_threshold < vol)
		{
			if (onBang != null)
				onBang();
		}
    	// print(vol);
    }


	/// <summary>
	/// マイク入力の平均値
	/// </summary>
	/// <returns></returns>
	private float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio_.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256.0f;
	}
}