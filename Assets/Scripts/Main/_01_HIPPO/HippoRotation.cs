using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vjt.hippo
{
	public class HippoRotation : MonoBehaviour {

		float rotateX;
		float rotateY;
		float rotateZ;


		/// <summary>
		/// 前を向いている
		/// </summary>
		private bool _isForward = false;


		/// <summary>
		/// Bang途中か
		/// </summary>
		private bool _isBanging = false;

		
		/// <summary>
		/// スタート
		/// </summary>
		void Start () {
			rotateX = Random.Range(1, 360);
			rotateY = Random.Range(1, 360);
			rotateZ = Random.Range(1, 360);
		}

		
		/// <summary>
		/// アプデ
		/// </summary>
		void Update ()
		{
			if (!_isForward) {
				gameObject.transform.rotation = Quaternion.Euler(rotateX, rotateY, rotateZ);
				rotateX += 2;
				rotateY += 2;
				rotateZ += 2;
			}
		}


		/// <summary>
		/// バスドラ
		/// </summary>
		public void Bang()
		{
			if (_isBanging) return;
			_isBanging = true;

			float scale = 0.3f;
			transform.localScale = new Vector3(scale, scale, scale);
			LeanTween.scale(gameObject, new Vector3(0.1f, 0.1f, 0.1f), 0.2f).setEaseOutBounce()
			.setOnComplete(() =>
			{
				_isBanging = false;
			});
		}


		/// <summary>
		/// 前を向く
		/// </summary>
		public void Forward()
		{
			_isForward = true;
			LeanTween.rotate(gameObject, Vector3.zero, 0.5f);
		}


		/// <summary>
		/// 前を向くのをキャンセル
		/// </summary>
		public void CancelForward()
		{
			_isForward = false;
		}
	}
}