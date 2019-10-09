using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalCollisionCheck : MonoBehaviour {

	MazeManager mazeManager;
    ArrowManager arrowManager;
    SimpleFade fadeManager;
    [SerializeField] bool debug = false;
    private bool triggered = false;
    void Start(){
        mazeManager = MazeManager.instance;
        arrowManager = ArrowManager.instance;
        fadeManager = SimpleFade.Instance;
    }
	void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            Debug.Log("player arrived");
            if (triggered == false){
                fadeManager.fadingNeeded = true;
			    mazeManager.CompleteMaze();
                triggered = true; //extra safe to make sure if doesn't accidentally trigger twice
                StartCoroutine(fadeManager.Unfade(0.5f));
            }
        }
    }
    
}
