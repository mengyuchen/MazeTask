using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCheck : MonoBehaviour {
	TrackPlayer logManager;
	SimpleFade fadeManager;
	[SerializeField] bool isLogging = false;
	//sets black screen to transparent
	void Start () {
		logManager = TrackPlayer.instance;
		fadeManager = SimpleFade.Instance;
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		fadeManager.fadingNeeded = true;
		if (other.tag == "Player"){
			Debug.Log("player hit " + gameObject.name);
			if (isLogging){
				string message = "Event: Player hit with " + gameObject.name;
				logManager.WriteCustomInfo(message);
			}
			
		}
	}
	void OnTriggerStay(Collider other){
	}
	void OnTriggerLeave(Collider other){
		fadeManager.fadingNeeded = false;
		if (other.tag == "Player"){	
			if (isLogging){
				logManager.WriteCustomInfo("Event with " + gameObject.name + " ends.");
			}
		}
	}

}
