using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFade : MonoBehaviour {
	FadeManager fadeManager;
	void Start(){
		fadeManager = FadeManager.instance;
	}
	// Use this for initialization
	private void OnTriggerEnter(Collider other) {
        fadeManager.FadeIn();
	}
	private void OnTriggerExit(Collider other) {
        fadeManager.FadeOut();
	}
}
