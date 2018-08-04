using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace vjt.blur
{
	public class BlurController : MonoBehaviour, ISozai
	{

		/// <summary>
		/// カメラのブラー
		/// </summary>
		[SerializeField] private MotionBlur _blur;


		public void Init()
		{
			_blur.extraBlur = false;
			_blur.blurAmount = 0;
			gameObject.SetActive(false);
		}

		public void SetAlpha(int midiVal)
		{
			//
			// 1以下ならdisactive
			if (midiVal < 1)
			{
				gameObject.SetActive(false);
				return;
			}

			//
			// アルファセット
			gameObject.SetActive(true);
			float value = (float)midiVal / 127;

			_blur.blurAmount = value;
		}

		public void Bang()
		{

		}
		
	}

}
