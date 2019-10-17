using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalCollisionCheck : MonoBehaviour {

	MazeManager mazeManager;
    ArrowManager arrowManager;
    FadeManager fadeManager;
    [SerializeField] bool debug = false;
    private bool triggered = false;
    void Start(){
        mazeManager = MazeManager.instance;
        arrowManager = ArrowManager.instance;
        fadeManager = FadeManager.instance;
    }
	void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            if (triggered == false){
                fadeManager.QuickFade();
			    mazeManager.CompleteMaze();
                triggered = true; //extra safe to make sure if doesn't accidentally trigger twice
            }
        }
    }
    
}
