using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class MIDIcontroller : MonoBehaviour {

	[SerializeField] private GameObject _eyePrefab;


	// Use this for initialization
	void Start () {




		this.UpdateAsObservable()
		.Subscribe(_ =>
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 50f);
				Debug.Log(pos);
				pos = Camera.main.ScreenToWorldPoint(pos);
				Debug.Log(pos);

				GameObject eye = Instantiate(_eyePrefab) as GameObject;
				eye.transform.position = pos;
			}

		});



		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
