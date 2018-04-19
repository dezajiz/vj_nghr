﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using vjt.hippo;
using vjt.yurayura;
using vjt.kumo;
using vjt.blur;
using vjt.glitch;
using vjt.rings;

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
		/// 01 hippo
		/// </summary>
		[SerializeField] private Hippo _01hippo;


		/// <summary>
		/// 02 yurayura
		/// </summary>
		[SerializeField] private Yurayura _02yurayura;


		/// <summary>
		/// 03 kumo
		/// </summary>
		[SerializeField] private KumoKumo _03kumo;


		/// <summary>
		/// 03 kumo
		/// </summary>
		[SerializeField] private BlurController _07blur;

		
		/// <summary>
		/// 00 glitch
		/// </summary>
		[SerializeField] private GlitchController _00glitch;


		/// <summary>
		/// rings
		/// </summary>
		[SerializeField] private RingsController _rings;


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
			// マイク入力
			_micInput.onBang = Bang;

			//
			// ブラー
			_07blur.Init();

			//
			// ヒッポ
			_01hippo.Init();

			//
			// yurayura
			_02yurayura.Init();

			//
			// kumo
			_03kumo.Init();

			//
			// glitch
			_00glitch.Init();

			//
			// rings
			_rings.Init();
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
					_01hippo.SetAlpha(keyValue);
					break;

				case "Slider2":
					_02yurayura.SetAlpha(keyValue);
					break;

				case "Slider3":
					_03kumo.SetAlpha(keyValue);
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
					_01hippo.SetColorR(keyValue);
					break;

				case "Knob2":
					_01hippo.SetColorG(keyValue);
					break;

				case "Knob3":
					_01hippo.SetColorB(keyValue);
					break;

				case "Knob4":
					_03kumo.SetColorR(keyValue);
					break;

				case "Knob5":
					_03kumo.SetColorG(keyValue);
					break;

				case "Knob6":
					_03kumo.SetColorB(keyValue);
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
			// Debug.Log(keyName);

			switch (keyName)
			{
				case "S1":
					_01hippo.Chirasu();
					break;

				case "M1":
					_01hippo.Seiretsu();
					break;
			}
		}
	

		/// <summary>
		/// アプデ
		/// </summary>
		void Update ()
		{
			//Debug.Log (nanoKontrol2.Slider1);
			//Debug.Log (nanoKontrol2.Slider2);
			//Debug.Log (nanoKontrol2.knob1);
			//Debug.Log (nanoKontrol2.knob2);

		}


		/// <summary>
		/// バスドラのBang
		/// </summary>
		private void Bang()
		{
			_01hippo.Bang();

			_02yurayura.Bang();

			_rings.Bang();
		}
	}
}