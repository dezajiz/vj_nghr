using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

namespace vjt.Wave00
{
	public class CameraController : MonoBehaviour
	{
		/// <summary>
		/// 制御カメラ
		/// </summary>		
		[SerializeField] private Camera _cam;


		/// <summary>
		/// tweenするか
		/// </summary>
		private bool _isTween = true;
		public bool isTween
		{
			get
			{
				return _isTween;
			}
			set
			{
				_isTween = value;
			}
		}


		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			//
			// 　x秒ごとに実行
			Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1)).Subscribe(_ =>
        	{
				// SetRandomPos();
				
        	})
			.AddTo(this);

			//
			// update
			this.UpdateAsObservable()
			.Subscribe(_ =>
			{
				//
				// 常にみる
				_cam.transform.LookAt(Vector3.zero);
			});
		}




		/// <summary>
		/// テキトーな座標に
		/// </summary>
		public void SetRandomPos()
		{
			float rx = UnityEngine.Random.Range(-100.0f, 100.0f);
			float ry = UnityEngine.Random.Range(-50.0f, 50.0f);
			float rz = UnityEngine.Random.Range(-100.0f, 100.0f);
			Vector3 pos = new Vector3(rx, ry, rz);
			if (_isTween)
			{
				LeanTween.move(_cam.gameObject, pos, 0.2f).setEaseOutCirc();
			}
			else
			{
				_cam.transform.localPosition = pos;
			}
		}
	}
}
