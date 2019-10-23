using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    [SerializeField] string TargetTag = "Target";
    [HideInInspector] public List<GameObject> targets = new List<GameObject>();
    [HideInInspector] public bool TargetReady = false;
    [SerializeField] GameObject targetPlaceHolder;
    private bool DummyTargets = false;
    private List<GameObject> placeHolders = new List<GameObject>();
    private Renderer[] targetRenderers;
    private Renderer[] placeHolderRenderers;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
    }

    private void Update()
    {
        //debug use only
        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchDummyMode();
        }
    }
    public void Reset()
    {
        targets.Clear();
        placeHolders.Clear();
        //Debug.Log(targets.Count + " Reset ");
        TargetReady = false;
    }

    public void TargetSearch()
    {
        TargetReady = false;
        //make sure everything is cleared
        //Debug.Log(targets.Count + " search reset ");
        targets.Clear();
        placeHolders.Clear();

        FindOriginalTargets();
        //Debug.Log("original targets: " + targets.Count);
        //Debug.Log("target renderers:" + targetRenderers.Length);

          
        InstantiateDummyTargets();
        
        //Debug.Log("dummy targets: " + placeHolders.Count);
        //set dummy renderers
        placeHolderRenderers = new Renderer[placeHolders.Count];
        for (int i = 0; i < placeHolders.Count; i++)
        {
            placeHolderRenderers[i] = placeHolders[i].GetComponent<Renderer>();
            placeHolderRenderers[i].enabled = false;
        }
        //Debug.Log("dummy t renderers: " + placeHolderRenderers.Length);

        AllRendererDisplayStatus(true);
        TargetReady = true;
        //Debug.Log("Targetmanager got target" + targets.Count);
    }
    public void AllRendererDisplayStatus(bool state)
    {
        if (!DummyTargets)
        {
            for (int i = 0; i < targetRenderers.Length; i++)
            {
                if (targetRenderers[i] != null)
                {
                    targetRenderers[i].enabled = state;
                }
            }
        } else
        {
            for (int i = 0; i < placeHolderRenderers.Length; i++)
            {
                if (placeHolderRenderers[i] != null)
                {
                    placeHolderRenderers[i].enabled = state;
                }
            }
        }
    }
    public void AllTargetsActiveStatus(bool state)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].SetActive(state);
        }
    }
    public void SetRendererDisplayStatus(int index, bool state)
    {
        if (!DummyTargets)
        {
            if (index < targetRenderers.Length)
            {
                if (targetRenderers[index] != null)
                {
                    if (targetRenderers[index].enabled != state)
                    {
                        targetRenderers[index].enabled = state;
                    }
                }
            }
        } else
        {
            if (index < placeHolderRenderers.Length)
            {
                if (placeHolderRenderers[index] != null)
                {
                    if (placeHolderRenderers[index].enabled != state)
                    {
                        placeHolderRenderers[index].enabled = state;
                    }
                }
            }
        }
    }
    public void SwitchDummyMode()
    {
        AllRendererDisplayStatus(false);
        DummyTargets = !DummyTargets;
        AllRendererDisplayStatus(true);
    }
    private void DestroyPlaceHolders()
    {
        //TO DO : make sure it is gone
        foreach(var o in placeHolders)
        {
            Destroy(o);
        }
    }
    private void FindOriginalTargets()
    {
        //find targets
        var objs = GameObject.FindGameObjectsWithTag(TargetTag);
        foreach (var o in objs)
        {
            targets.Add(o);
        }
        //set renderers
        targetRenderers = new Renderer[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            targetRenderers[i] = targets[i].GetComponent<Renderer>();
            targetRenderers[i].enabled = false;
        }
    }
    private void InstantiateDummyTargets()
    {
        //create dummies
        foreach (var o in targets)
        {
            var ph = Instantiate(targetPlaceHolder, o.transform.position, Quaternion.identity);
            ph.transform.name = o.transform.name + "Dummy";
            //ph.transform.parent = placeHolderContainer.transform;\
            ph.transform.parent = o.transform;
            placeHolders.Add(ph);
        }
    }

}
