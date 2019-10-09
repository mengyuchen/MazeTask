using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOffOnFade : MonoBehaviour {

	SimpleFade simpleFade;
	[SerializeField]GameObject[] triggerObjects;
	void Start(){
		simpleFade = SimpleFade.Instance;
	}
	void Update(){
		if (simpleFade.fadingNeeded){
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
