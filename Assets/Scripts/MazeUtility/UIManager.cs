using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class UIManager : MonoBehaviour {
	[SerializeField]TrackPlayer trackManager;
	[SerializeField]private InputField nameInput;
    [SerializeField]private InputField genderInput;
    [SerializeField]private InputField idInput;
    [SerializeField]private InputField dateInput;
	[SerializeField]private GameObject infoRect;
	private bool resetting = false;
    Player player;
	void Start () {
		trackManager = GetComponent<TrackPlayer>();
        if (player == null)
        {
            player = Player.instance;
        }
	}

	void LateUpdate()
	{
		
	}
	
	public void ConfirmFileName(){
        trackManager.FILE_NAME = idInput.text + "_" + nameInput.text + "_" + dateInput.text + ".txt";
        trackManager.Username = nameInput.text;
		trackManager.Gender = genderInput.text;
		trackManager.Date = dateInput.text;
		trackManager.ParticipantID = idInput.text;
		trackManager.SetFileName();
		Debug.Log("information recorded at /" +  idInput.text + "_" + nameInput.text + "_" + dateInput.text + ".txt");
    }
	public void DeactivateInfoRect(){
		infoRect.SetActive(false);
	}
	public void ActivateInfoRect(){
		infoRect.SetActive(true);
	}
    public void Calibrate()
    { 
	    Vector3 referencePos = new Vector3(0, 1, 0);
	    Vector3 currentPos = player.hmdTransform.position;
	    Vector3 offsetPos = currentPos - referencePos;
	    player.trackingOriginTransform.position = -offsetPos;
	    Debug.Log(offsetPos + " offsetPos");
	    float rotZ = player.hmdTransform.rotation.eulerAngles.z;
	    float rotX = player.hmdTransform.rotation.eulerAngles.x;
	    Vector3 referenceRot = new Vector3(rotX, 0, rotZ);
	    Vector3 currentRot = player.hmdTransform.rotation.eulerAngles;
	    Vector3 offsetRot = currentRot - referenceRot;
	    Debug.Log(offsetRot + " offset rot");
	    player.trackingOriginTransform.rotation = Quaternion.Euler(-offsetRot);
	    
    }

    void ResetPose()
    {
	    
//	    
//	    Debug.Log(resetting);
//	    Debug.Log(player.hmdTransform.position);
//	    float y = player.trackingOriginTransform.position.y;
//	    float rotZ = player.hmdTransform.rotation.eulerAngles.z;
//        
//	    Quaternion zeroRot = Quaternion.Euler(new Vector3(0, 0, rotZ));
//	    player.hmdTransform.position = new Vector3(0, y, 0);
//	    player.hmdTransform.rotation = zeroRot;
//	    if (player.leftHand != null)
//	    {
//		    float lfy = player.leftHand.transform.position.y;
//		    player.leftHand.transform.position = new Vector3(-0.25f, lfy, 0);
//	    }
//	    if (player.rightHand != null)
//	    {
//		    float rfy = player.rightHand.transform.position.y;
//		    player.rightHand.transform.position = new Vector3(0.25f, rfy, 0);
//	    }
	    Debug.Log(player.hmdTransform.position);
    }
}
