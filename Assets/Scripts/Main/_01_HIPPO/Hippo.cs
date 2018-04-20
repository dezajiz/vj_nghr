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
		public GameObject cube;

		private Material cubeMat;

		public GameObject sphere;

		public GameObject capsule;

		
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
		private HippoRotation[] _hippoScripts;
		private MeshRenderer[] _rendererList;
		private MeshFilter[] _meshList;

		/// <summary>
		/// メッシュのタイプ一覧
		/// </summary>
		private enum MESH_MODE{
			CUBE,
			CAPSULE,
			SPHERE
		};

		/// <summary>
		/// 並びのタイプ一覧
		/// </summary>
		private enum SEIRETSU_MODE{
			KIREI,
			AIDA,
			KITANAI
		}

		/// <summary>
		/// 現在のメッシュタイプ
		/// </summary>
		private MESH_MODE meshMode = MESH_MODE.CUBE;

		/// <summary>
		/// 現在の整列タイプ
		/// </summary>
		private SEIRETSU_MODE seiretsuMode = SEIRETSU_MODE.KITANAI;

		private bool globalActiveState = true;

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
			_hippoScripts = new HippoRotation[_totalNum];
			_rendererList = new MeshRenderer[_totalNum];
			_meshList = new MeshFilter[_totalNum];

			Color defaultColor = new Color(0, 0, 0, 0);

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

				GameObject instance = Instantiate (cube, pos, Quaternion.identity);
				instance.transform.parent = gameObject.transform;

				MeshRenderer meshrender = instance.GetComponent<MeshRenderer>();
				meshrender.material.color = defaultColor;

				_hippoScripts[i] = instance.GetComponent<HippoRotation>();
				_rendererList[i] = meshrender;
				_meshList[i] = instance.GetComponent<MeshFilter>();

				_hippoList.Add(instance);
			}

			//
			// 行列整列
			cubePositions1 = new Vector3[_totalNum];

			for (int x = 0; x < _lineNum; x++) {
				for (int y = 0; y < _lineNum; y++) {
					for (int z = 0; z < _lineNum; z++) {
						cubePositions1[x * _lineNum * _lineNum + y * _lineNum + z] = new Vector3(
							x - _lineNum * .5f,
							y - _lineNum * .5f,
							z - _lineNum * .5f
						);
					}
				}
			}

			gameObject.SetActive(false);
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

			if (globalActiveState) {
				gameObject.SetActive(true);
			}

			//
			// アルファセット
			float value = (float)midiVal / 127;
			_matColor = new Color(_matColor.r, _matColor.g, _matColor.b, value);

			for (int i = 0; i < _totalNum; i++)
			{
				_rendererList[i].material.color = _matColor;
			}
		}


		public void SetColorR(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(value, _matColor.g, _matColor.b, _matColor.a);

			for (int i = 0; i < _totalNum; i++)
			{
				_rendererList[i].material.color = _matColor;
			}
		}
		public void SetColorG(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(_matColor.r, value, _matColor.b, _matColor.a);

			for (int i = 0; i < _totalNum; i++)
			{
				_rendererList[i].material.color = _matColor;
			}

		}
		public void SetColorB(int col)
		{
			float value = (float)col / 127;
			_matColor = new Color(_matColor.r, _matColor.g, value, _matColor.a);
			
			for (int i = 0; i < _totalNum; i++)
			{
				_rendererList[i].material.color = _matColor;
			}
		}

		/// <summary>
		/// メッシュをキューブに変更
		/// </summary>
		public void changeToCube() {

			if (gameObject.activeSelf && meshMode == MESH_MODE.CUBE) {
				return;
			}

			meshMode = MESH_MODE.CUBE;

			Mesh mesh = cube.GetComponent<MeshFilter>().sharedMesh;
			for (int i = 0; i < _totalNum; i++)
			{
				_meshList[i].mesh = mesh;
			}
		}

		/// <summary>
		/// メッシュをカプセルに変更
		/// </summary>
		public void changeToCapsule() {

			if (gameObject.activeSelf && meshMode == MESH_MODE.CAPSULE) {
				return;
			}

			meshMode = MESH_MODE.CAPSULE;

			Mesh mesh = capsule.GetComponent<MeshFilter>().sharedMesh;
			for (int i = 0; i < _totalNum; i++)
			{
				_meshList[i].mesh = mesh;
			}
		}

		/// <summary>
		/// メッシュを球に変更
		/// </summary>
		public void changeToSphere() {

			if (gameObject.activeSelf && meshMode == MESH_MODE.SPHERE) {
				return;
			}

			meshMode = MESH_MODE.SPHERE;

			Mesh mesh = sphere.GetComponent<MeshFilter>().sharedMesh;
			for (int i = 0; i < _totalNum; i++)
			{
				_meshList[i].mesh = mesh;
			}
		}

		/// <summary>
		/// 整列さす
		/// </summary>
		public void Seiretsu()
		{
			if (_isMoving || seiretsuMode == SEIRETSU_MODE.KIREI) return;
			
			_isMoving = true;

			for (int i = 0; i < _totalNum; i++)
			{
				_hippoScripts[i].Forward();

				if (i != _totalNum - 1)
					LeanTween.move(_hippoList[i], cubePositions1[i], 0.5f).setEaseOutQuint();
				else
					LeanTween.move(_hippoList[i], cubePositions1[i], 0.5f).setEaseOutQuint()
					.setOnComplete(Moved);
			}

			seiretsuMode = SEIRETSU_MODE.KIREI;
		}

		public void Chukan()
		{
			if (_isMoving || seiretsuMode == SEIRETSU_MODE.AIDA) return;

			_isMoving = true;

			for (int i = 0; i < _totalNum; i++)
			{
				_hippoScripts[i].CancelForward();

				if (i != _totalNum - 1)
					LeanTween.move(_hippoList[i], (cubePositions1[i] + cubePosition0[i]) * .5f, 0.5f).setEaseOutQuint();
				else
					LeanTween.move(_hippoList[i], (cubePositions1[i] + cubePosition0[i]) * .5f, 0.5f).setEaseOutQuint()
					.setOnComplete(Moved);
			}

			seiretsuMode = SEIRETSU_MODE.AIDA;
		}

		/// <summary>
		/// ランダムにちらす
		/// </summary>
		public void Chirasu()
		{
			if (_isMoving || seiretsuMode == SEIRETSU_MODE.KITANAI) return;

			_isMoving = true;

			for (int i = 0; i < _totalNum; i++)
			{
				_hippoScripts[i].CancelForward();

				if (i != _totalNum - 1)
					LeanTween.move(_hippoList[i], cubePosition0[i], 0.7f).setEaseOutQuint();
				else
					LeanTween.move(_hippoList[i], cubePosition0[i], 0.7f).setEaseOutQuint()
					.setOnComplete(Moved);
			}

			seiretsuMode = SEIRETSU_MODE.KITANAI;
		}

		public void ChangeActiveState(float _posX) {
			globalActiveState = !globalActiveState;
			gameObject.SetActive(globalActiveState);

			if (globalActiveState) {
				LeanTween.move(gameObject, new Vector3(_posX, 0, 0), 0.7f).setEaseOutQuint();
			} else {
				gameObject.transform.localPosition = new Vector3(-2.5f, 0, 0);
			}
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
			for (int i = 0; i < _totalNum; i++)
			{
				_hippoScripts[i].Bang();
			}
		}
	}
}