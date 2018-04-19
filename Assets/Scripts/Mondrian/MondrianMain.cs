using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace vjt.Mondrian
{
	public class MondrianMain : MonoBehaviour
	{
		/// <summary>
		/// セルのプレファブ
		/// </summary>
		[SerializeField] private GameObject _cellPrefab;


		/// <summary>
		/// キャンバス
		/// </summary>
		[SerializeField] private GameObject _canvas;


		/// <summary>
		/// セルの数
		/// </summary>
		[SerializeField] private int _cellNum = 20;


		/// <summary>
		/// セルの配列
		/// </summary>
		/// <returns></returns>
		private List<Image> _cells = new List<Image>();


		/// <summary>
		/// ここからスタート
		/// </summary>
		void Start()
		{
			Init();
		}


		/* 
			■手順
			1 スクリーン上でランダムな点を描画
			2 直近の点を選出（ここでペアになる？）(ペアではなくその点から見た直近の点を捕捉)
			3 距離の割合を決める（ここはパラメータ値でもランダム値でもいいかも）
			4 そこで四角形の大きさが決まるのでトゥイーンさせる
		 */


		/// <summary>
		/// 初期化
		/// </summary>
		private void Init()
		{

			for (int i = 0; i < _cellNum; i++)
			{
				Image newcell = Instantiate(_cellPrefab).GetComponent<Image>();
				newcell.transform.parent = _canvas.transform;
				// newcell.GetComponent<RectTransform>().anchoredPosition = new Vector2(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height));
				newcell.transform.position = new Vector3(UnityEngine.Random.Range(0, Screen.width), UnityEngine.Random.Range(0, Screen.height), 1);
				_cells.Add(newcell);
			}





		}
	}
}

