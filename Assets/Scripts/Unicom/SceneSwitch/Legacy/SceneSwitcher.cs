using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitcher : MonoBehaviour {
	public string scenename;
	[System.NonSerialized]public FadeManager fadeManager;
	[System.NonSerialized]public bool switching = false;
	[System.NonSerialized]public int i = 0;
	// Use this for initialization
	void Start () {
		fadeManager = FadeManager.Instance;
		if (fadeManager != null){
			Debug.Log("fademanager found");
		} else {
			Debug.LogError("fademanager not found");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
