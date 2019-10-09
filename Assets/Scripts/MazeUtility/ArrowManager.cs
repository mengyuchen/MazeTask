using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowManager : MonoBehaviour
{
    [Header("Preset GameObjects")]
    public GameObject[] startingPoints; // predefined starting point objects, with given position for each
    [SerializeField] GameObject arrowAvatar; // any prefab object that can represent the looking of an arrow
    private ArrowCollisionCheck arrowCollider; // a sub-script in charge of actual trigger check
    [System.NonSerialized]public bool completed = false; // feedback bool to be sent back to notify the LevelManager
    public static ArrowManager instance;
    public GameObject instructionText;
    [SerializeField] bool debug = false;
    private Text mText;
    public GameObject footPrint;
    void Awake(){
        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        arrowCollider = arrowAvatar.GetComponent<ArrowCollisionCheck>();
        arrowAvatar.SetActive(false); //arrowAvatar not visible at initialization
        SetTextStatus(false);
        mText = instructionText.GetComponentInChildren<Text>();
    }

    //level manager call "Activate(int)" function by giving an index value
    //to reference one of the start point objects that arrow avatar can teleport to
    public void Activate(int arrowPosIndex){
        if (startingPoints[arrowPosIndex] != null){
            // Debug.Log(arrowPosIndex + " arrow pos");
            arrowAvatar.transform.position = startingPoints[arrowPosIndex].transform.position; 
            footPrint.transform.position = new Vector3(startingPoints[arrowPosIndex].transform.position.x, 0.1f, startingPoints[arrowPosIndex].transform.position.z);
            footPrint.transform.rotation = startingPoints[arrowPosIndex].transform.rotation;
            
            // Debug.Log(arrowAvatar.transform.position);
            arrowAvatar.SetActive(true);
            SetTextStatus(true);
            mText.text = "Please Stand Here";
        } else {
            Debug.Log("Arrow Activation Failed");
        }
    }
    public void Reset(){
        arrowAvatar.transform.position = new Vector3(0, 100, 0);
        arrowAvatar.SetActive(false); // turn off avatar to be not active in the scene
       
        // Debug.Log("arrow reset");
        SetTextStatus(false);
    }
    public void SetTextContent(string sentence){
        mText.text = sentence;
    }
    private void SetTextStatus(bool status){
        instructionText.transform.position = arrowAvatar.transform.position;
        instructionText.SetActive(status);
    }
    private IEnumerator ResetFootPrint(float waitTime){
        yield return new WaitForSecondsRealtime(waitTime);
        footPrint.transform.position = new Vector3(0, 100, 0);
    }
    public void TriggerFootPrint(){
        StartCoroutine(ResetFootPrint(5));
    }

}
