using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace vjt.hippo
{

public class Cube : MonoBehaviour {

	public GameObject prefab;
	
	/// <summary>
	/// 一列あたりの数
	/// </summary>
	[SerializeField] private int _lineNum;

	float rotateY = 0;
	float RADIAN = Mathf.PI / 180;
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
	/// 生成したプレハブの配列
	/// </summary>
	/// <returns></returns>
	private List<GameObject> _hippoList = new List<GameObject>();


	void Start () {

		//
		// 配列の総数
		_totalNum = _lineNum * _lineNum * _lineNum;

		//
		// ランダム整列
		cubePosition0 = new Vector3[_totalNum];

		for (int i = 0; i < _totalNum; i++) {
			float radius = Random.Range(0.0f, 100);
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
		int length = 10;

		for (int x = 0; x < _lineNum; x++) {
			for (int y = 0; y < _lineNum; y++) {
				for (int z = 0; z < _lineNum; z++) {
					cubePositions1[x * _lineNum * _lineNum + y * _lineNum + z] = new Vector3(x - _lineNum * .5f,y - _lineNum * .5f,z - _lineNum*0.5f);
				}
			}
		}
	}
	
	void Update () {
		gameObject.transform.rotation = Quaternion.Euler(0, rotateY, 0);
		rotateY ++;

		if (Input.GetKey(KeyCode.M)) {
			Move();
		}
	}

	void Move () {

		_isRandomLine = !_isRandomLine;
		if (_isRandomLine)
		{
			for (int i = 0; i < _hippoList.Count; i++)
			{
				//
				// tween中キャンセル
				if (LeanTween.isTweening(_hippoList[i]))
					LeanTween.cancel(_hippoList[i]);

				LeanTween.move(_hippoList[i], cubePosition0[i], 0.7f).setEaseOutQuint();
			}


			// for (int i = 0; i < _totalNum; i++) {
			// 	Transform child = transform.GetChild(i);
			// 	child.transform.DOMove(cubePosition0[i], 2.0f).SetEase(Ease.InOutCirc);
			// }
			
		}
		else
		{
			for (int i = 0; i < _hippoList.Count; i++)
			{
				//
				// tween中キャンセル
				if (LeanTween.isTweening(_hippoList[i]))
					LeanTween.cancel(_hippoList[i]);

				LeanTween.move(_hippoList[i], cubePositions1[i], 0.7f).setEaseOutQuint();
			}



			// for (int i = 0; i < _totalNum; i++) {
			// 	Transform child = transform.GetChild(i);
			// 	child.transform.DOMove(cubePositions1[i], 2.0f).SetEase(Ease.InOutCirc);
			// }
		}
	}


	/// <summary>
	/// アルファ
	/// </summary>
	/// <param name="midiVal"></param>
	public void SetAlpha(int midiVal)
	{
		float value = midiVal / 127;

		for (int i = 0; i < _hippoList.Count; i++)
		{
			LeanTween.alpha(_hippoList[i], 0, value);
		}
	}
}

}