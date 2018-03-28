using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;


namespace vjt
{
	public class Eye : MonoBehaviour {

		private bool _isRotation = false;
		public bool isRotation
		{
			set
			{
				_isRotation = value;
			}
		}


		private Vector3 _randomRotate;


		// Use this for initialization
		void Start () {

			SpectrumAnalyzer spec = GameObject.Find("TestMic").GetComponent<SpectrumAnalyzer>();
			
			spec.interaScale
			.Subscribe(vec =>
			{
				transform.localScale = vec;

				// Debug.Log("hai"+ vec.x);
				// if (2 < vec.x)
				// {
				// 	// if (LeanTween.isTweening()){
				// 	// 	LeanTween.cancelAll();
				// 	// }

				// 	// LeanTween.scale(gameObject, new Vector3(2, 2, 2), 0.2f).setOnComplete(() =>
				// 	// 	{
				// 	// 		LeanTween.scale(gameObject, Vector3.one, 0.4f);
				// 	// 	}
				// 	// );
				// }
			});

			_randomRotate = new Vector3(UnityEngine.Random.Range(1.0f, 3.0f), UnityEngine.Random.Range(1.0f, 3.0f), UnityEngine.Random.Range(1.0f, 3.0f));
		}


		
		// Update is called once per frame
		void Update () {
			
			if(_isRotation)
			{
				transform.Rotate (_randomRotate);
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public void Kill()
		{
			Destroy(gameObject);
		}
	}
}