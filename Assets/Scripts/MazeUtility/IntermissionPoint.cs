using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionPoint : MonoBehaviour {
    MazeManager mazeManager;
    IntermissionManager intermissionPointManager;
    public GameObject instructionText;
	// Use this for initialization
	void Start () {
        mazeManager = MazeManager.instance;
        intermissionPointManager = IntermissionManager.instance;
	}

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            if (intermissionPointManager.approval)
            {
                mazeManager.PrepareLevel();
                intermissionPointManager.approval = false;
                this.gameObject.SetActive(false);
            }
        }
    }
    public void SetInstructionText()
    {
        instructionText.SetActive(true);
        instructionText.transform.position = transform.position;
    }
}
