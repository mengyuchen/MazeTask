using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightAdjuster : MonoBehaviour {

    [Header("Manual Adjustment")]
    [SerializeField] GameObject VRPlayer;
    [SerializeField] float YoffsetStep = 0.05f;
    [SerializeField] KeyCode UpKey = KeyCode.UpArrow;
    [SerializeField] KeyCode DownKey = KeyCode.DownArrow;
    [Header("Auto Adjustment")]
    [SerializeField] bool AutoAdjust = true;
    [SerializeField] float autoHeightOffset = 0.9f;
    void Start()
    {
        if (AutoAdjust) VRPlayer.transform.Translate(new Vector3(0, -autoHeightOffset, 0));
    }
    void Update()
    {
        if (Input.GetKeyDown(UpKey))
        {
            if (VRPlayer.activeSelf)
            {
                VRPlayer.transform.Translate(new Vector3(0, YoffsetStep, 0));
            }
            
        }
        if (Input.GetKeyDown(DownKey))
        {
            if (VRPlayer.activeSelf)
            {
                VRPlayer.transform.Translate(new Vector3(0, -YoffsetStep, 0));
            }
        }
        
    }
}
