using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace vjt.kumo
{
	public class KumoKumo : MonoBehaviour, ISozai
	{
		/// <summary>
		/// パーティクル本体
		/// </summary>
		[SerializeField] private ParticleSystem _kumoParti;


		/// <summary>
		/// カラー
		/// </summary>
		private Color _matColor;


		/// <summary>
		/// 
		/// </summary>
		public void Init()
		{
			_kumoParti.Stop();

			_matColor = new Color(0, 0, 0, 0);
			var main = _kumoParti.main;
			main.startColor = _matColor;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="midiVal"></param>
		public void SetAlpha(int midiVal)
		{
			//
			// 1以下ならdisactive
			if (midiVal < 1)
			{
				_kumoParti.Stop();
				gameObject.SetActive(false);
				return;
			}

			gameObject.SetActive(true);
			_kumoParti.Play();

			float value = (float)midiVal / 127 * 15;
			_matColor = new Color(_matColor.r, _matColor.g, _matColor.b, value);
			var main = _kumoParti.main;
			main.startColor = _matColor;
		}


		public void SetColorR(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(value, _matColor.g, _matColor.b, _matColor.a);
			var main = _kumoParti.main;
			main.startColor = _matColor;
		}
		public void SetColorG(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(_matColor.r, value, _matColor.b, _matColor.a);
			var main = _kumoParti.main;
			main.startColor = _matColor;
		}
		public void SetColorB(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(_matColor.r, _matColor.g, value, _matColor.a);
			var main = _kumoParti.main;
			main.startColor = _matColor;
		}



		/// <summary>
		/// 
		/// </summary>
		public void Bang()
		{

		}

	}
}