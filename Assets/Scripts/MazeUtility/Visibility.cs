using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visibility : MonoBehaviour {

    [Header("Visibility Mode")]
    public bool fogMode = true;
    private float visibilityDistance = 1.0f;
    private float fogFarDist = 2.0f;
    MazeManager mazeManager;
    CameraMarker cameraMarker;
    TargetManager targetManager;
	void Start () {
        if (cameraMarker == null) cameraMarker = CameraMarker.instance;
        if (mazeManager == null) mazeManager = MazeManager.instance;
        if (targetManager == null) targetManager = TargetManager.instance;

        //Debug.Log(targets.Length);
        visibilityDistance = mazeManager.VisibilityDistance;

        if(fogMode){
            RenderSettings.fog = true;
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogColor = RenderSettings.skybox.GetColor("_Tint");
            RenderSettings.fogStartDistance = visibilityDistance;
            RenderSettings.fogEndDistance = fogFarDist;
        }
	}
	// Update is called once per frame
	void Update () {
        
        if (!fogMode){
            
            if (mazeManager.currentLevel >= 3 && targetManager.TargetReady)
            {
                TargetDistanceChecking();
            }		
        } else{

            if (RenderSettings.fogStartDistance != visibilityDistance){
                RenderSettings.fogStartDistance = visibilityDistance;
            }
            if (RenderSettings.fogEndDistance != fogFarDist){
                RenderSettings.fogEndDistance = fogFarDist;
            }
        }
	}
    private void TargetDistanceChecking()
    {
        for (int i = 0; i < targetManager.targets.Count; i++)
        {
            var dist = Vector3.Distance(cameraMarker.transform.position, targetManager.targets[i].transform.position);
            if (dist > visibilityDistance)
            {
                targetManager.SetRendererDisplayStatus(i, false);
            }
            else
            {
                targetManager.SetRendererDisplayStatus(i, true);
            }
        }
    }
}
