using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

namespace vjt.glitch
{
	public class GlitchController : MonoBehaviour
	{
		/// <summary>
		/// 
		/// </summary>
		[SerializeField] private AnalogGlitch _glitch;
		

		public void Init()
		{
			_glitch.scanLineJitter = 0;
			_glitch.verticalJump = 0;
			_glitch.horizontalShake = 0;
			_glitch.colorDrift = 0;
		}


		public void SetScanLineJitter(int midiVal)
		{
			float value = (float)midiVal / 127;
			_glitch.scanLineJitter = value;
		}


		public void SetVerticalJump(int midiVal)
		{
			float value = (float)midiVal / 127;
			_glitch.verticalJump = value;
		}


		public void SetHorizontalShake(int midiVal)
		{
			float value = (float)midiVal / 127;
			_glitch.horizontalShake = value;
		}


		public void SetColorDrift(int midiVal)
		{
			float value = (float)midiVal / 127;
			_glitch.colorDrift = value;
		}


	}
}