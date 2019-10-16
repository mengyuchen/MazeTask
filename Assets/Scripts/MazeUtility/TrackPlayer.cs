using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;
using System.IO;

public class TrackPlayer : MonoBehaviour
{
    static public TrackPlayer instance;
    MazeManager mazeManager;
    public Transform playerTransform;
    public uint recordInterval; // every N frames
    public bool shouldRecord = false; // called by other manager
    [System.NonSerialized]public string FILE_NAME = "default.txt";
    private int counter;
    public string Username{get;set;}
    public string Gender{get;set;}
    public string Date{get;set;}
    public string ParticipantID{get;set;}

    [Header("CSV")]
    StreamWriter sw;
    StreamWriter csvw;
    private List<string[]> rowData = new List<string[]>();
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
        mazeManager = MazeManager.instance;
        // playerTransform = GameObject.Find("Player").transform;
        recordInterval = (recordInterval < 1) ? 1 : recordInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldRecord){
            RecordPose();
        }
    }
    private void RecordPose(){
        counter ++;
        if (counter > int.MaxValue / 2) {counter = 1;}
        
        if (counter % recordInterval == 0){
            // code for data collection
            Vector3 playerPos = playerTransform.position;
            Quaternion playerRot = playerTransform.rotation;
            sw.WriteLine(Time.time + ": " + playerPos.x + "," + playerPos.z + "," + playerPos.y + "," + playerRot.eulerAngles.y);

            //record in CSV format
            string[] rowDataPose = new string[8];
            rowDataPose[0] = ParticipantID;
            rowDataPose[1] = Gender;
            rowDataPose[2] = (mazeManager.currentLevel - 2).ToString();
            rowDataPose[3] = Time.time.ToString();
            rowDataPose[4] = playerPos.x.ToString();
            rowDataPose[5] = playerPos.y.ToString();
            rowDataPose[6] = playerPos.z.ToString();
            rowDataPose[7] = playerRot.eulerAngles.y.ToString();
            rowData.Add(rowDataPose);

            // Debug.Log("sss");
        }
    }
    //public methods, will be called by other managers
    public void SetFileName(){
        //initialize streamwriter
        sw = File.AppendText(FILE_NAME);
        sw.AutoFlush = true;
        //write some meta info
        sw.WriteLine("Participant Name: " + Username);
        sw.WriteLine("Participant Gender: " + Gender);
        sw.WriteLine("Participant ID: " + ParticipantID);
        sw.WriteLine("Test Date: " + Date);

        //create CSV file
        csvw = File.AppendText(FILE_NAME + ".csv");
        csvw.AutoFlush = true;
        string[] rowDataPose = new string[8];
        rowDataPose[0] = "Participant ID";
        rowDataPose[1] = "Gender";
        rowDataPose[2] = "Level";
        rowDataPose[3] = "Time";
        rowDataPose[4] = "PosX";
        rowDataPose[5] = "PosY";
        rowDataPose[6] = "PosZ";
        rowDataPose[7] = "Facing Angle";
        rowData.Add(rowDataPose);
        WriteCSVData();
    }
    public void WriteModeInfo(){
        if (sw == null){
            Debug.Break();
        }
        sw.WriteLine("Maze Mode: " + mazeManager.currentMode);
    }
    public void WriteLevelInfo(){
        if (sw == null){
            Debug.Break();
        }
        sw.WriteLine("Trail No.:" + (mazeManager.currentLevel - 2) + " Begins");
    }
    public void WriteLevelFinishInfo(bool status, float timeSpent){
        if (sw == null){
            Debug.Break();
        }
        string message;
        if (status) {
            message = "success";
        } else {
            message = "fail";
        }
        sw.WriteLine("Trail Result: " + message + " | time spent: " + timeSpent);
        
        sw.WriteLine("Trail No.:" + (mazeManager.currentLevel - 2) + " Ends");

        //write a csv file at the end of each maze and reset row data
        WriteCSVData();
    }
    private void WriteCSVData()
    {
        string[][] output = new string[rowData.Count][];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }
        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }
        csvw.WriteLine(sb);
        rowData.Clear();
    }
    public void WriteCustomInfo(string info){
        if (sw == null){
            Debug.Break();
        }
        sw.WriteLine(info);
    }
    public void WritePlayerInfo(){
        if (sw == null){
            Debug.Break();
        }
        Vector3 playerPos = playerTransform.position;
        Quaternion playerRot = playerTransform.rotation;
        sw.WriteLine(playerPos.x + "," + playerPos.z + "," + playerPos.y + "," + playerRot.eulerAngles.y);
    }
    public void Reset(){
        shouldRecord = false;
        counter = 0;
    }
    public void Run(){
        shouldRecord = true;
        counter = 0;
    }

    public void CreateFile(string idInput, string nameInput, string dateInput, string genderInput)
    {
        FILE_NAME = idInput + "_" + nameInput + "_" + dateInput + ".txt";
        Username = nameInput;
        Gender = genderInput;
        Date = dateInput;
        ParticipantID = idInput;
        SetFileName();
    }

   
}
