using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace vjt.Wave00
{
	/* 
		TODO
		https://keep.google.com/u/1/?pli=1#LIST/1520804081929.563296615
	 */

	public class Main : MonoBehaviour
	{
		/// <summary>
		/// 目オブジェクトのプレハブ
		/// </summary>
		/// <returns></returns>
		[SerializeField] private Eye _eyePrefab;


		/// <summary>
		/// グリッドの長さ
		/// </summary>
		/// <returns></returns>
		[SerializeField] private IntReactiveProperty _glidNum;


		/// <summary>
		/// グリッドの間隔
		/// </summary>
		/// <returns></returns>
		[SerializeField] private FloatReactiveProperty _intervalDist;


		/// <summary>
		/// 波の大きさ
		/// </summary>
		/// <returns></returns>
		[SerializeField] private FloatReactiveProperty _sinNum;


		/// <summary>
		/// カメラ制御
		/// </summary>
		private CameraController _camCon;


		/// <summary>
		/// Eye格納配列
		/// </summary>
		/// <returns></returns>
		private List<Eye> _eyes = new List<Eye>();


		/// <summary>
		/// 初期化しているか
		/// </summary>
		private bool _isInit = false;



		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			if (_isInit) return;

			//
			// パラメータ設定
			_glidNum = new IntReactiveProperty(10);
			_intervalDist = new FloatReactiveProperty(10);
			_sinNum = new FloatReactiveProperty(2);

			//
			// グリッド状に配置する x - z
			GenerateEyes(_glidNum.Value);

			//
			// グリッド数変化
			_glidNum.Subscribe(intval => RePutEyes(intval));

			//
			// 距離変化
			_intervalDist.Subscribe(floval => ReSizingEyes(floval));

			//
			// update
			this.UpdateAsObservable()
			.Subscribe(_ =>
			{
				//
				// 入力
				InputSetup();

				//
				// 波
				EyeWaving();
			});

			//
			// カメラ制御
			_camCon = GetComponent<CameraController>();
			_camCon.Init();


			_isInit = true;
		}


		/// <summary>
		/// 波
		/// </summary>
		private void EyeWaving()
		{
			int rCount = 0;
			int cCount = 0;
			for (int i = 0; i < _eyes.Count; i++)
			{
				int kake = (rCount < cCount)?cCount:rCount;
				Vector3 pos = _eyes[i].transform.position;
				_eyes[i].transform.position = new Vector3(pos.x, Mathf.Sin(Time.time + kake * 1000) * _sinNum.Value, pos.z);

				if (rCount < _glidNum.Value)
				{
					rCount++;
				}
				else
				{
					rCount = 0;
					cCount++;
				}
				
			}
		}
		


		/// <summary>
		/// 再配置
		/// </summary>
		private void RePutEyes(int val)
		{
			if (val < 0) val = 0;
			
			//
			// 今の消す
			for (int i = 0; i < _eyes.Count; i++)
			{
				_eyes[i].Kill();
				_eyes[i] = null;
			}
			_eyes.Clear();
		
			//
			// 生成
			GenerateEyes(val);
		}



		/// <summary>
		/// 間隔を変える
		/// </summary>
		private void ReSizingEyes(float val)
		{
			if(val < 0) val = 0;

			int rCount = 0;
			int cCount = 0;
			for (int i = 0; i < _eyes.Count; i++)
			{
				//
				// 配置
				float xx = val * rCount - (_glidNum.Value * val / 2);
				float zz = val * cCount - (_glidNum.Value * val / 2);
				_eyes[i].transform.localPosition = new Vector3(xx, 0, zz);

				if (rCount < _glidNum.Value-1)
				{
					rCount++;
				}
				else
				{
					rCount = 0;
					cCount++;
				}
			}
		}


		/// <summary>
		/// 目を生成していく
		/// </summary>
		private void GenerateEyes(int glidNum)
		{
			//
			// グリッド状に配置する x - z
			for (int i = 0; i < glidNum; i++)
			{
				for (int j = 0; j < glidNum; j++)
				{
					//
					// 
					Eye neweye = Instantiate(_eyePrefab).GetComponent<Eye>();
					neweye.isRotation = true;
					Destroy(neweye.GetComponent<Rigidbody>());

					//
					// 配置
					float xx = _intervalDist.Value * i - (glidNum * _intervalDist.Value / 2);
					float zz = _intervalDist.Value * j - (glidNum * _intervalDist.Value / 2);
					neweye.transform.localPosition = new Vector3(xx, 0, zz);
					
					_eyes.Add(neweye);
				}	
			}
		}


		/// <summary>
		/// 入寮系のセットアップ
		/// </summary>
		private void InputSetup()
		{
			//
			// space
			if (Input.GetKeyDown(KeyCode.Space))
			{
				_camCon.SetRandomPos();
			}

			//
			// a
			if (Input.GetKeyDown(KeyCode.A))
			{
				_camCon.isTween = !_camCon.isTween;
			}
		}
	}
}
