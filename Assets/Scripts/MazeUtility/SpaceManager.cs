using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceManager : MonoBehaviour
{
    public static SpaceManager instance;
    [SerializeField] string[] SpaceObjectTags = new string[3]{"Wall", "Boundary", "Floor"};
    GameObject[] UniversalGroundPlane;
    [HideInInspector] public GameObject[][] SpatialObjects;
    [HideInInspector] public bool SpatialObjectReady = false;
    private Renderer[][] SpatialObjectRenderers;
    private Renderer[] GroundPlaneRenderers;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        UniversalGroundPlane = GameObject.FindGameObjectsWithTag("GroundPlane");
        GroundPlaneRenderers = new Renderer[UniversalGroundPlane.Length];
        for (int i = 0; i < GroundPlaneRenderers.Length; i++)
        {
            GroundPlaneRenderers[i] = UniversalGroundPlane[i].GetComponent<Renderer>();
            GroundPlaneRenderers[i].enabled = false;
        }
    }
    private void Update()
    {
        //debug purpose only
        if (Input.GetKeyDown(KeyCode.O))
        {
            AllRendererDisplayStatus(false);
            SetGroundPlaneDisplayStatus(true);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            AllRendererDisplayStatus(true);
            SetGroundPlaneDisplayStatus(false);
        }
    }
    public void SetGroundPlaneDisplayStatus(bool state)
    {
        for(int i = 0; i < GroundPlaneRenderers.Length; i++)
        {
            GroundPlaneRenderers[i].enabled = state;
        }
    }
    public void LoadSpatialObjects()
    {
        FindSpatialObjects();
        SetSpatialObjectRenderers();
        SpatialObjectReady = true;
        SetGroundPlaneDisplayStatus(false);
    }
    public void AllRendererDisplayStatus(bool state)
    {
        for (int i = 0; i < SpaceObjectTags.Length; i++)
        {
            for (int j = 0; j < SpatialObjects[i].Length; j++)
            {
                if (SpatialObjectRenderers[i][j] != null)
                {
                    SpatialObjectRenderers[i][j].enabled = state;
                }
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
