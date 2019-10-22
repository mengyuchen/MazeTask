using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public List<GameObject> targets = new List<GameObject>();
    [SerializeField] GameObject placeHolder;
    [SerializeField] GameObject placeHolderContainer;
    private List<GameObject> placeHolders = new List<GameObject>();

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
        placeHolders.Clear();
        DestroyPlaceHolders();
        //Debug.Log(targets.Count + " Reset ");
    }
   
    public void TargetSearch()
    {
        targets.Clear();
        placeHolders.Clear();
        DestroyPlaceHolders();

        //Debug.Log(targets.Count + " search reset ");
        var objs = GameObject.FindGameObjectsWithTag("Target");
        foreach (var o in objs)
        {
            targets.Add(o);
        }
        InstantiatePlaceHolder();
        //Debug.Log("Targetmanager got target" + targets.Count);
    }
    private void InstantiatePlaceHolder()
    {
        foreach (var o in targets)
        {
            var ph = Instantiate(placeHolder, o.transform.position, Quaternion.identity);
            ph.SetActive(false);
            ph.transform.name = o.transform.name + "Dummy";
            ph.transform.parent = placeHolderContainer.transform;
            placeHolders.Add(ph);
        }
    }
    private void DestroyPlaceHolders()
    {
        foreach(var o in placeHolders)
        {
            Destroy(o);
        }
    }
}
