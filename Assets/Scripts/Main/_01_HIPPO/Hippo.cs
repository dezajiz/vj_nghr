using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace vjt.hippo
{
	public class Hippo : MonoBehaviour, ISozai
	{

		/// <summary>
		/// 対象オブジェクト
		/// </summary>
		public GameObject prefab;

		
		/// <summary>
		/// 一列あたりの数
		/// </summary>
		[SerializeField] private int _lineNum;


		float rotateY = 0;
		float RADIAN = Mathf.PI / 10;
		Vector3[] cubePosition0;
		Vector3[] cubePositions1;


		/// <summary>
		/// 配列の総数
		/// </summary>
		private int _totalNum;


		/// <summary>
		/// ランダム整列かどうか
		/// </summary>
		private bool _isRandomLine = true;


		/// <summary>
		/// 動き中か
		/// </summary>
		private bool _isMoving = false;


		/// <summary>
		/// 生成したプレハブの配列
		/// </summary>
		/// <returns></returns>
		private List<GameObject> _hippoList = new List<GameObject>();


		/// <summary>
		/// マテリアルのカラー
		/// </summary>
		private Color _matColor;


		/// <summary>
		/// 初期化
		/// </summary>
		public void Init()
		{
			//
			// 配列の総数
			_totalNum = _lineNum * _lineNum * _lineNum;

			//
			// ランダム整列
			cubePosition0 = new Vector3[_totalNum];

			for (int i = 0; i < _totalNum; i++) {
				float radius = Random.Range(0.0f, 10);
				float rad = Random.Range(0, 360) * RADIAN;
				float rad2 = Random.Range(0, 360) * RADIAN;

				Vector3 pos = new Vector3(
					Mathf.Cos(rad) * Mathf.Cos(rad2) * (radius),
					Mathf.Cos(rad) * Mathf.Sin(rad2) * (radius),
					Mathf.Sin(rad) * (radius)
				);

				cubePosition0[i] = pos;

				GameObject instance = Instantiate (prefab, pos, Quaternion.identity);
				instance.transform.parent = gameObject.transform;
				_hippoList.Add(instance);
			}

			//
			// 行列整列
			cubePositions1 = new Vector3[_totalNum];

			for (int x = 0; x < _lineNum; x++) {
				for (int y = 0; y < _lineNum; y++) {
					for (int z = 0; z < _lineNum; z++) {
						cubePositions1[x * _lineNum * _lineNum + y * _lineNum + z] = new Vector3(x - _lineNum * .5f,y - _lineNum * .5f,z - _lineNum*0.5f);
					}
				}
			}

			//
			// はじめはアルファ０
			for (int i = 0; i < _hippoList.Count; i++)
			{
				MeshRenderer meshrender = _hippoList[i].transform.Find("default").GetComponent<MeshRenderer>();
				_matColor = new Color(0, 0, 0, 0);
				meshrender.material.color = _matColor;
			}
		}


		/// <summary>
		/// アルファ
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
			// アルファセット
			gameObject.SetActive(true);
			float value = (float)midiVal / 127;
			for (int i = 0; i < _hippoList.Count; i++)
			{
				MeshRenderer meshrender = _hippoList[i].transform.Find("default").GetComponent<MeshRenderer>();
				_matColor = new Color(_matColor.r, _matColor.g, _matColor.b, value);
				meshrender.material.color = _matColor;
			}
		}


		public void SetColorR(int col)
		{
			float value = (float)col / 127;
			for (int i = 0; i < _hippoList.Count; i++)
			{
				MeshRenderer meshrender = _hippoList[i].transform.Find("default").GetComponent<MeshRenderer>();
				_matColor = new Color(value, _matColor.g, _matColor.b, _matColor.a);
				meshrender.material.color = _matColor;
			}
		}
		public void SetColorG(int col)
		{
			float value = (float)col / 127;
			for (int i = 0; i < _hippoList.Count; i++)
			{
				MeshRenderer meshrender = _hippoList[i].transform.Find("default").GetComponent<MeshRenderer>();
				_matColor = new Color(_matColor.r, value, _matColor.b, _matColor.a);
				meshrender.material.color = _matColor;
			}

		}
		public void SetColorB(int col)
		{
			float value = (float)col / 127;
			for (int i = 0; i < _hippoList.Count; i++)
			{
				MeshRenderer meshrender = _hippoList[i].transform.Find("default").GetComponent<MeshRenderer>();
				_matColor = new Color(_matColor.r, _matColor.g, value, _matColor.a);
				meshrender.material.color = _matColor;
			}

		}


		/// <summary>
		/// 整列さす
		/// </summary>
		public void Seiretsu()
		{
			if (!_isRandomLine) return;
			
			if (_isMoving) return;
			_isMoving = true;

			for (int i = 0; i < _hippoList.Count; i++)
			{
				_hippoList[i].GetComponent<HippoRotation>().Forward();

				if (i != _hippoList.Count - 1)
					LeanTween.move(_hippoList[i], cubePositions1[i], 0.5f).setEaseOutQuint();
				else
					LeanTween.move(_hippoList[i], cubePositions1[i], 0.5f).setEaseOutQuint()
					.setOnComplete(Moved);
			}

			_isRandomLine = false;
		}


		/// <summary>
		/// ランダムにちらす
		/// </summary>
		public void Chirasu()
		{
			if (_isRandomLine) return;
			
			if (_isMoving) return;
			_isMoving = true;

			for (int i = 0; i < _hippoList.Count; i++)
			{
				_hippoList[i].GetComponent<HippoRotation>().CancelForward();

				if (i != _hippoList.Count - 1)
					LeanTween.move(_hippoList[i], cubePosition0[i], 0.7f).setEaseOutQuint();
				else
					LeanTween.move(_hippoList[i], cubePosition0[i], 0.7f).setEaseOutQuint()
					.setOnComplete(Moved);
			}

			_isRandomLine = true;
		}


		/// <summary>
		/// 
		/// </summary>
		private void Moved()
		{
			_isMoving = false;
		}


		/// <summary>
		/// アプデ
		/// </summary>
		void Update () {
			gameObject.transform.rotation = Quaternion.Euler(0, rotateY, 0);
			rotateY ++;
		}


		/// <summary>
		/// バスドラ
		/// </summary>
		public void Bang()
		{
			for (int i = 0; i < _hippoList.Count; i++)
			{
				_hippoList[i].GetComponent<HippoRotation>().Bang();
			}
		}
	}
}