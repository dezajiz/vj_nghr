using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using vjt.hippo;
using vjt.yurayura;
using vjt.kumo;
using vjt.blur;
using vjt.glitch;
using vjt.rings;
using vjt.Text;

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
		[SerializeField] private Hippo _01hippo1;
		[SerializeField] private Hippo _01hippo2;
		[SerializeField] private Hippo _01hippo3;


		/// <summary>
		/// 02 yurayura
		/// </summary>
		[SerializeField] private Yurayura _02yurayura;


		/// <summary>
		/// 03 kumo
		/// </summary>
		[SerializeField] private KumoKumo _03kumo;

		/// <summary>
		/// 04 Text
		/// </summary>
		[SerializeField] private TextText _04text;

		/// <summary>
		/// 07 blur
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
			_01hippo1.Init();
			_01hippo2.Init();
			_01hippo3.Init();

			//
			// yurayura
			_02yurayura.Init();

			//
			// kumo
			// _03kumo.Init();

			//
			// yurayura
			_04text.Init();

			//
			// glitch
			_00glitch.Init();

			//
			// rings
			_rings.Init();

			// 同時Tweenの制限を増やす
			LeanTween.init(1500);

			// 2と3はボタンを押すまで非活性
			_01hippo2.ChangeActiveState(0.0f);
			_01hippo3.ChangeActiveState(0.0f);
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
					_01hippo1.SetAlpha(keyValue);
					_01hippo2.SetAlpha(keyValue);
					_01hippo3.SetAlpha(keyValue);
					break;

				case "Slider2":
					_02yurayura.SetAlpha(keyValue);
					break;

				case "Slider3":
					_rings.SetAlpha(keyValue);
					break;
				case "Slider4":
					_04text.SetAlpha(keyValue);
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
					_01hippo1.SetColorR(keyValue);
					_01hippo2.SetColorR(keyValue);
					_01hippo3.SetColorR(keyValue);
					break;

				case "Knob2":
					_01hippo1.SetColorG(keyValue);
					_01hippo2.SetColorG(keyValue);
					_01hippo3.SetColorG(keyValue);
					break;

				case "Knob3":
					_01hippo1.SetColorB(keyValue);
					_01hippo2.SetColorB(keyValue);
					_01hippo3.SetColorB(keyValue);
					break;

				case "Knob4":
					_02yurayura.SetColor(_nanoKon.knob4);
					break;

				case "Knob5":
					_02yurayura.SetFineness(keyValue);
					break;

				case "Knob6":
					_rings.SetInterval(keyValue);
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
					_01hippo1.Chirasu();
					_01hippo2.Chirasu();
					_01hippo3.Chirasu();
					break;

				case "M1":
					_01hippo1.Chukan();
					_01hippo2.Chukan();
					_01hippo3.Chukan();
					break;

				case "R1":
					_01hippo1.Seiretsu();
					_01hippo2.Seiretsu();
					_01hippo3.Seiretsu();
					break;

				case "S2":
					_01hippo1.changeToCube();
					_01hippo2.changeToCube();
					_01hippo3.changeToCube();
					break;

				case "M2":
					_01hippo1.changeToCapsule();
					_01hippo2.changeToCapsule();
					_01hippo3.changeToCapsule();
					break;

				case "R2":
					_01hippo1.changeToSphere();
					_01hippo2.changeToSphere();
					_01hippo3.changeToSphere();
					break;

				case "S3":
					_01hippo2.ChangeActiveState(-2.5f);
					break;
				case "M3":
					_01hippo3.ChangeActiveState(2.5f);
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


		/// <summary>
		/// バスドラのBang
		/// </summary>
		private void Bang()
		{
			_01hippo1.Bang();
			_01hippo2.Bang();
			_01hippo3.Bang();

			_02yurayura.Bang();

			_rings.Bang();
		}
	}
}
