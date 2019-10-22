using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CollisionCheck : MonoBehaviour {
	TrackPlayer logManager;
	FadeManager fadeManager;
    TargetManager targetManager;
	[SerializeField] bool isLogging = false;
    [SerializeField] string DetectName = "Wall";
    List<GameObject> targets = new List<GameObject>();
	//sets black screen to transparent
	void Start () {
		if (logManager == null) logManager = TrackPlayer.instance;
		if (fadeManager == null) fadeManager = FadeManager.instance;
        if (targetManager == null) targetManager = TargetManager.instance;
    }
    private void Update()
    {
        
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider other){
        //TO DO: need more test
		if (other.gameObject.tag == DetectName){
            fadeManager.FadeIn();
            GetTarget();
            //Debug.Log("player hit " + other.gameObject.name);
            
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
            //         Debug.Log("exit wall");
            foreach (var t in targets)
            {
                t.SetActive(true);
            }
        }
	}
    private void GetTarget()
    {
        targets = targetManager.targets;
        //Debug.Log("collision get target" + targets.Count);
    }
    
}
