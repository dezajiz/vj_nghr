using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vjt.Wave00
{
	public class Starter : MonoBehaviour
	{

		[SerializeField] private Main _main;

		// Use this for initialization
		void Start ()
		{
			_main.Init();
		}
	}
}