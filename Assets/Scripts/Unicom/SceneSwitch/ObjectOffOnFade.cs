using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOffOnFade : MonoBehaviour {

	FadeManager fadeManager;
	[SerializeField]GameObject[] triggerObjects;
	void Start(){
		fadeManager = FadeManager.instance;
	}
	void Update(){
		if (fadeManager.Fading){
			for (int i = 0; i < triggerObjects.Length; i ++){
				triggerObjects[i].SetActive(false);
			}
		} else {
			for (int i = 0; i < triggerObjects.Length; i ++){
				triggerObjects[i].SetActive(true);
			}
		}
	}
}
