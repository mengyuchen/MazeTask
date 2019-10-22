using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager instance;
    [SerializeField] string[] SpaceObjectTags = new string[2]{"Wall", "Boundary"};
   
    [HideInInspector] public GameObject[][] SpatialObjects;
    [HideInInspector] public bool SpatialObjectReady = false;
    private Renderer[][] SpatialObjectRenderers;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        
    }
    private void Update()
    {
        //debug purpose only
        if (Input.GetKeyDown(KeyCode.O))
        {
            AllRendererDisplayStatus(false);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AllRendererDisplayStatus(true);
        }
    }
    public void LoadSpatialObjects()
    {
        FindSpatialObjects();
        SetSpatialObjectRenderers();
        SpatialObjectReady = true;
    }
    public void AllRendererDisplayStatus(bool state)
    {
        for (int i = 0; i < SpaceObjectTags.Length; i++)
        {
            for (int j = 0; j < SpatialObjects[i].Length; j++)
            {
                SpatialObjectRenderers[i][j].enabled = state;
            }
        }
    }
    private void FindSpatialObjects()
    {
        //find targets
        SpatialObjects = new GameObject[SpaceObjectTags.Length][];
        for (int i = 0; i < SpaceObjectTags.Length; i++)
        {
            var objs = GameObject.FindGameObjectsWithTag(SpaceObjectTags[i]);
            SpatialObjects[i] = new GameObject[objs.Length];
            for (int j = 0; j < objs.Length; j ++)
            {
                SpatialObjects[i][j] = objs[j];
            }
            
        }
    }
    private void SetSpatialObjectRenderers()
    {
        SpatialObjectRenderers = new Renderer[SpaceObjectTags.Length][];
        for (int i = 0; i < SpaceObjectTags.Length; i++)
        {
            SpatialObjectRenderers[i] = new Renderer[SpatialObjects[i].Length];
            for (int j = 0; j < SpatialObjects[i].Length; j++)
            {
                SpatialObjectRenderers[i][j] = SpatialObjects[i][j].GetComponent<Renderer>();
            }
        }
        
    }
}
