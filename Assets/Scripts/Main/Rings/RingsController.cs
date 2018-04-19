using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace vjt.rings
{
	public class RingsController : MonoBehaviour {

		[SerializeField] private GameObject _ringsPrefab;

		private List<GameObject> _ringsList = new List<GameObject>();

		public void Init()
		{

		}


		public void Bang()
		{
			GameObject rings = Instantiate(_ringsPrefab);
			rings.transform.parent = gameObject.transform;
			rings.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			rings.GetComponent<RectTransform>().localScale = Vector3.zero;
			_ringsList.Add(rings);

			float animTime = 0.5f;
			LeanTween.scale(rings.GetComponent<RectTransform>(), new Vector3(4, 4, 4), animTime).setEaseOutQuad();
			LeanTween.alpha(rings.GetComponent<RectTransform>(), 0, animTime).setEaseOutQuad()
			.setOnComplete(() =>
			{
				Destroy(_ringsList[0]);
				_ringsList.RemoveAt(0);
			});
		}
	}
}
