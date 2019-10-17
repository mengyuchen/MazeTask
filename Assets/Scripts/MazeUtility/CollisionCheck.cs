using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionCheck : MonoBehaviour {
	TrackPlayer logManager;
	FadeManager fadeManager;
	[SerializeField] bool isLogging = false;
    [SerializeField] string DetectName = "Wall";
    GameObject[] targets;
	//sets black screen to transparent
	void Start () {
		logManager = TrackPlayer.instance;
		fadeManager = FadeManager.instance;
	}
    private void Update()
    {
        
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        //TO DO: need more test
		if (other.gameObject.tag == DetectName){
            fadeManager.FadeIn();
            SearchTarget();
            //Debug.Log("player hit " + other.gameObject.name);
            Debug.Log(targets.Length);
            foreach(var t in targets)
            {
                t.SetActive(false);
            }

			if (isLogging){
				string message = "Event: Player hit with " + other.gameObject.name;
				logManager.WriteCustomInfo(message);
			}
			
		}
	}
	void OnTriggerStay(Collider other){
	}
	void OnTriggerExit(Collider other){        
		if (other.gameObject.tag == DetectName){
            fadeManager.StopAllCoroutines();
            fadeManager.ResetFadingStatus();
            fadeManager.FadeOut();

            if (isLogging){
				logManager.WriteCustomInfo("Event with " + other.gameObject.name + " ends.");
			}

            foreach (var t in targets)
            {
                t.SetActive(true);
            }
        }
	}
    private void SearchTarget()
    {
        if (targets == null)
        {
            Debug.Log("finding from null");
            targets = GameObject.FindGameObjectsWithTag("Target");
        }
        else
        {
            if (targets.Length == 0)
            {
                Debug.Log("finding");
                targets = GameObject.FindGameObjectsWithTag("Target");
            }
        }
    }
}
