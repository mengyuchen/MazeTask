using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public List<GameObject> targets = new List<GameObject>();


    private MazeManager mazeManager;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        mazeManager = MazeManager.instance;
    }
    public void Reset()
    {
        targets.Clear();
        Debug.Log(targets.Count + " Reset ");
    }
    public void TargetSearch()
    {
        targets.Clear();

        Debug.Log(targets.Count + " search reset ");
        var objs = GameObject.FindGameObjectsWithTag("Target");
        foreach (var o in objs)
        {
            targets.Add(o);
        }
        Debug.Log("Targetmanager got target" + targets.Count);
    }
}
