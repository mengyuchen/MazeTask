using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFade : MonoBehaviour {
	SimpleFade simpleFade;
	void Start(){
		simpleFade = SimpleFade.Instance;
	}
	// Use this for initialization
	private void OnTriggerEnter(Collider other) {
		simpleFade.fadingNeeded = true;
	}
	private void OnTriggerExit(Collider other) {
		simpleFade.fadingNeeded = false;	
	}
}
