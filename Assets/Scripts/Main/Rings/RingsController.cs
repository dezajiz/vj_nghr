using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace vjt.rings
{
	public class RingsController : MonoBehaviour {

		[SerializeField] private GameObject _ringsPrefab;

		private List<GameObject> _ringsList = new List<GameObject>();

		private bool bungFlg = false;
		private float interval = 1;

		CanvasGroup group;

		public void Init()
		{
			group = gameObject.transform.parent.GetComponent<CanvasGroup>();
			gameObject.SetActive(false);
		}

		void Update() {
			if (bungFlg) {
				return;
			}

			bungFlg = true;

			Bang();
			StartCoroutine(Reset());
		}

		public void SetInterval(int midiVal) {
			float _interval = 1 - ((float)midiVal / 127);
			interval = _interval;
		}

		public void SetAlpha(int midiVal) {
			if (midiVal == 0) {
				bungFlg = false;
				gameObject.SetActive(false);
				return;
			}

			gameObject.SetActive(true);

			float alpha = (float)midiVal / 127;
			group.alpha = alpha;
		}

		IEnumerator Reset()
		{
			yield return new WaitForSeconds(interval);
			bungFlg = false;
		}

		public void Bang()
		{
			GameObject rings = Instantiate(_ringsPrefab);
			rings.transform.SetParent(gameObject.transform);
			rings.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
			rings.GetComponent<RectTransform>().localScale = Vector3.zero;
			_ringsList.Add(rings);

			float animTime = 0.5f;
			LeanTween.scale(rings.GetComponent<RectTransform>(), new Vector3(4, 4, 4), animTime).setEaseOutQuad()
			.setOnComplete(() =>
			{
				Destroy(_ringsList[0]);
				_ringsList.RemoveAt(0);
			});
		}
	}
}
