using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour {

	AudioSource audioSource;
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
