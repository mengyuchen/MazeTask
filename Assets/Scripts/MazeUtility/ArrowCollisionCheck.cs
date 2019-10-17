using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollisionCheck : MonoBehaviour
{
    MazeManager mazeManager;
    ArrowManager arrowManager;
    FadeManager fadeManager;
    [SerializeField] bool debug = false;
    void Start(){
        mazeManager = MazeManager.instance;
        arrowManager = ArrowManager.instance;
        fadeManager = FadeManager.instance;
    }
    void Update(){
        // if (debug){
        //  
        //     }
        // }
    }
    void OnTriggerEnter(Collider collider){
        if (collider.tag == "Player"){
            // Debug.Log("arrow collision detected");
            arrowManager.completed = true;
            transform.position = new Vector3(0, 100, 0);
            fadeManager.FadeIn();
            StartCoroutine(Trigger(2.0f));
        }
    }
    private IEnumerator Trigger(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime / 3.0f);
        fadeManager.FadeOut();
        yield return new WaitForSecondsRealtime(waitTime);
		mazeManager.LoadLevel();
        arrowManager.Reset();
        arrowManager.TriggerFootPrint();
    }
}
