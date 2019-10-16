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
        trackManager.CreateFile(idInput.text, nameInput.text, dateInput.text, genderInput.text);
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
	    //Vector3 referencePos = new Vector3(0, 1, 0);
	    //Vector3 currentPos = player.hmdTransform.position;
	    //Vector3 offsetPos = currentPos - referencePos;
	    //player.trackingOriginTransform.position = -offsetPos;
	    //Debug.Log(offsetPos + " offsetPos");
	    //float rotZ = player.hmdTransform.rotation.eulerAngles.z;
	    //float rotX = player.hmdTransform.rotation.eulerAngles.x;
	    //Vector3 referenceRot = new Vector3(rotX, 0, rotZ);
	    //Vector3 currentRot = player.hmdTransform.rotation.eulerAngles;
	    //Vector3 offsetRot = currentRot - referenceRot;
	    //Debug.Log(offsetRot + " offset rot");
	    //player.trackingOriginTransform.rotation = Quaternion.Euler(-offsetRot);
	    
    }

}
