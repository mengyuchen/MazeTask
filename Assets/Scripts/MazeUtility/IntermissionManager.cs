using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntermissionManager : MonoBehaviour {

    public static IntermissionManager instance;
    public IntermissionPoint[] points;
    MazeManager mazeManager;
    ArrowManager arrowManager;
    [HideInInspector]public bool approval = false;
	void Awake () {
        if (instance == null) instance = this;
	}
    void Start()
    {
        mazeManager = MazeManager.instance;
        arrowManager = ArrowManager.instance;
        for(int i = 0; i < points.Length; i++)
        {
            points[i].gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivatePoints()
    {
        float maxDist = 0;
        int maxID = 0;
        for (int i = 0; i < points.Length; i++)
        {
            var dist = Vector3.Distance(points[i].transform.position, arrowManager.startingPoints[mazeManager.currentLevel - 1].transform.position      );
            if (dist > maxDist)
            {
                maxDist = dist;
                maxID = i;
            }
        }
        points[maxID].gameObject.SetActive(true);
        points[maxID].SetInstructionText();
        approval = true;
    }
}
