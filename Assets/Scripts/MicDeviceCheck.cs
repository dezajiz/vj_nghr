using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicDeviceCheck : MonoBehaviour {

	void Start()
	{
		Text text = gameObject.GetComponent<Text>();
        foreach (string device in Microphone.devices) {
            Debug.Log("Name: " + device);
			text.text += device + "\n";
        }
    }
}
