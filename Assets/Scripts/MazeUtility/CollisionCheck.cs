using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionCheck : MonoBehaviour {
	TrackPlayer logManager;
	FadeManager fadeManager;
    TargetManager targetManager;
    MazeManager mazeManager;
	[SerializeField] bool isLogging = false;
    [SerializeField] string DetectName = "Wall";
	//sets black screen to transparent
	void Start () {
		if (logManager == null) logManager = TrackPlayer.instance;
		if (fadeManager == null) fadeManager = FadeManager.instance;
        if (targetManager == null) targetManager = TargetManager.instance;
        if (mazeManager == null) mazeManager = MazeManager.instance;
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        if (mazeManager.currentLevel >= 3)
        {
            //TO DO: need more test
            if (other.gameObject.tag == DetectName)
            {
                fadeManager.FadeIn();

                targetManager.AllTargetsActiveStatus(false);

                if (isLogging)
                {
                    string message = "Event: Player hit with " + other.gameObject.name;
                    logManager.WriteCustomInfo(message);
                }

            }
        }
	}
	void OnTriggerStay(Collider other){
	}
	void OnTriggerExit(Collider other){
        if (mazeManager.currentLevel >= 3)
        {
            if (other.gameObject.tag == DetectName)
            {
                fadeManager.StopAllCoroutines();
                fadeManager.ResetFadingStatus();
                fadeManager.FadeOut();

                if (isLogging)
                {
                    logManager.WriteCustomInfo("Event with " + other.gameObject.name + " ends.");
                }
                targetManager.AllTargetsActiveStatus(true);
            }
        }
	}
   
}
