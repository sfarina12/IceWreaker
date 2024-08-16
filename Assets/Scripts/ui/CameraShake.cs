using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CameraShake : MonoBehaviour
{
	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform camTransform;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;
	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	float act_shakeAmount = 0;
	bool shake = false;
	
	void Start() { originalPos = camTransform.localPosition; }

	void Awake() {
		if (camTransform == null) { camTransform = GetComponent(typeof(Transform)) as Transform; }
	}
	
	void Update() {
		if(shake) {
			if (shakeDuration > 0)
			{
				camTransform.localPosition = originalPos + Random.insideUnitSphere * act_shakeAmount;

				shakeDuration -= Time.deltaTime * decreaseFactor;
				if(act_shakeAmount > 0) { act_shakeAmount -= Time.deltaTime * decreaseFactor; }
				else { act_shakeAmount = 0; }
			}
			else
			{
				shakeDuration = 0f;
				camTransform.localPosition = originalPos;
				shake = false;
			}
		}
	}

	public void shakeCamera(float force = -1,float duration = -1) { 
		shakeDuration = duration == -1 ? 1 : force; 
		act_shakeAmount = force == -1 ? shakeAmount : force; 
		shake = true; 
	}
}
