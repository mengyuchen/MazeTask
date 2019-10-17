using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionCheck : MonoBehaviour {
	TrackPlayer logManager;
	FadeManager fadeManager;
	[SerializeField] bool isLogging = false;
    [SerializeField] string DetectName = "Wall";
	//sets black screen to transparent
	void Start () {
		logManager = TrackPlayer.instance;
		fadeManager = FadeManager.instance;
	}

    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        fadeManager.FadeIn();
		if (other.gameObject.tag == DetectName){
			Debug.Log("player hit " + other.gameObject.name);
			if (isLogging){
				string message = "Event: Player hit with " + other.gameObject.name;
				logManager.WriteCustomInfo(message);
			}
			
		}
	}
	void OnTriggerStay(Collider other){
	}
	void OnTriggerExit(Collider other){
        // TO DO: MAKE SURE WHEN DESTROYED, FADE OUT
        fadeManager.StopAllCoroutines();
        fadeManager.ResetFadingStatus();
        fadeManager.FadeOut();
		if (other.gameObject.tag == DetectName){	
			if (isLogging){
				logManager.WriteCustomInfo("Event with " + other.gameObject.name + " ends.");
			}
		}
	}

}
