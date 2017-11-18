using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public Camera mainCam;

	public float shakeAmount = 0;

	void Awake()
	{	
		if(mainCam == null)
			mainCam = Camera.main;
	}

	void beginShake()
	{
		if(shakeAmount>0)
		{
			Vector3 camPos = mainCam.transform.position;

			float shakeamtx = Random.value * shakeAmount * 2 - shakeAmount;
			float shakeamty = Random.value * shakeAmount * 2 - shakeAmount;
			camPos.x += shakeamtx;
			camPos.y += shakeamty;

			mainCam.transform.position = camPos;
		}

	}

	void stopShake()
	{
		CancelInvoke("beginShake");
		mainCam.transform.localPosition = Vector3.zero;
	}

	public void Shake(float amt, float length)
	{
		shakeAmount = amt;
		InvokeRepeating("beginShake", 0, 0.01f);
		Invoke("stopShake", length);
	}
}
