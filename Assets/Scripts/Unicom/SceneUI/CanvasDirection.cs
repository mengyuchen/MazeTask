using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDirection : MonoBehaviour {

	Transform Target;
	Canvas canvas;
	void Start () {
		canvas = GetComponent<Canvas>();
		Target = CameraMarker.instance.transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.position + Target.transform.rotation * Vector3.forward,
			Target.transform.rotation * Vector3.up);
		Vector3 eulerAngles = transform.eulerAngles;
		eulerAngles.z = 0;
		transform.eulerAngles = eulerAngles;
	}
}
