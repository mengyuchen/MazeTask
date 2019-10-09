using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LearningPoint : MonoBehaviour {
	TrackPlayer logManager;
	SimpleFade fadeManager;
	MazeManager mazeManager;
    ArrowManager arrowManager;
	[SerializeField] bool isLogging = false;
	[SerializeField] GameObject[] presetPoints;
	[SerializeField] int loopTimes = 5;
	private int pointIndex = 0;
	private int loop = 0;
	private bool learningComplete = false;
	//sets black screen to transparent
	void Start () {
		logManager = TrackPlayer.instance;
		fadeManager = SimpleFade.Instance;
		mazeManager = MazeManager.instance;
        arrowManager = ArrowManager.instance;
		transform.position = presetPoints[pointIndex].transform.position;
	}

	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		fadeManager.fadingNeeded = true;
		if (other.tag == "Player"){
			if (isLogging){
				string message = "learning point at idx = " + pointIndex + " loop = " + loop;
				logManager.WriteCustomInfo(message);
			}
			StartCoroutine(fadeManager.Unfade(0.5f));
			pointIndex ++;
			if (pointIndex < presetPoints.Length){
				NextPoint();
			} else {
				loop ++;
				if (loop < loopTimes){
					pointIndex = 0;
					NextPoint();
				} else {
					if (!learningComplete){
						mazeManager.CompleteMaze();
						learningComplete = true; //make sure it doesn't trigger twice;
					}
				}
			}
			
			
		}
	}
	void OnTriggerStay(Collider other){
	}
	void OnTriggerLeave(Collider other){
		// fadeManager.fadingNeeded = false;
		// if (other.tag == "Player"){	
		// 	if (isLogging){
		// 		logManager.WriteCustomInfo("Event with learning point ended");
		// 	}
		// }
	}
	void NextPoint(){
		transform.position = presetPoints[pointIndex].transform.position;
		Debug.Log("Point Index = " + pointIndex + " loop No. " + (loop + 1));
	}


}

