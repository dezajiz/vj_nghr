using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  vjt.Text
{
	public class TextText : MonoBehaviour
	{
		/// <summary>
		/// オーバーレイ
		/// </summary>
		[SerializeField] private GameObject _text;

		/// <summary>
		/// 適用マテリアル
		/// </summary>
		[SerializeField] private Material _textMat;

		/// <summary>
		/// フェード
		/// </summary>
		/// <param name="midiVal"></param>
		public void SetValue(int midiVal)
		{
			//
			// 1以下ならdisactive
			if (midiVal < 1)
			{
				gameObject.SetActive(false);
				return;
			}

			gameObject.SetActive(true);

			float alpha = (float)midiVal / 127;
			float scale = alpha * 2.0f;

			//
			// マテリアルセット
			Color color = _textMat.color;
			color.a = alpha;
			_textMat.color = color;
			// _text.transform.localScale = new Vector3(scale, 1.0f, scale);
		}
	}	
}
