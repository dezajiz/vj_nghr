﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  vjt.yurayura
{
	public class Yurayura : MonoBehaviour, ISozai
	{
		/// <summary>
		/// プレーン
		/// </summary>
		[SerializeField] private GameObject _plane;


		/// <summary>
		/// 適用マテリアル
		/// </summary>
		[SerializeField] private Material _targetMat;


		/// <summary>
		/// Bang途中
		/// </summary>
		private bool _isBanging = false;


		/// <summary>
		/// planeのスケール
		/// </summary>
		private Vector3 _planeScale;


		/// <summary>
		/// Bangのコルーチン
		/// </summary>
		private IEnumerator _bangCol;


		/// <summary>
		/// BangのときにいれるY
		/// </summary>
		private float _bangY = 20;


		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			_planeScale = new Vector3(0, 4, 4);
			_plane.transform.localScale = _planeScale;
		}


		/// <summary>
		/// フェード
		/// </summary>
		/// <param name="midiVal"></param>
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
			// スケール
			gameObject.SetActive(true);
			float value = (float)midiVal / 127 * 15;
			_planeScale = new Vector3(value, _planeScale.y, _planeScale.z);
			_plane.transform.localScale = _planeScale;

			//
			// マテリアルセット
			MeshRenderer meshrender = _plane.GetComponent<MeshRenderer>();
			meshrender.material = _targetMat;
		}


		/// <summary>
		/// Bang
		/// </summary>
		public void Bang()
		{
			if (_isBanging)
				_isBanging = true;

			_planeScale = new Vector3(_planeScale.x, _bangY, _planeScale.z);
			_plane.transform.localScale = _planeScale;

			LeanTween.value(_plane,SetScale, _planeScale.y, 4, 0.2f).setEaseOutCubic()
			.setOnComplete(() =>
			{
				_isBanging = false;
			});
		}

		private void SetScale(float value)
		{
			_planeScale = new Vector3(_planeScale.x, value, _planeScale.z);
			_plane.transform.localScale = _planeScale;
		}
	}	
}