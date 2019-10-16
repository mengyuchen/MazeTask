using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
	MazeManager mazeManager;
	TrackPlayer logManager;
    IntermissionManager intermissionManager;
	public float timeLimit = 45.0f;
	public Text timeDisplay;
	[System.NonSerialized]public float timeCount;
	[System.NonSerialized]public bool completed = false;
	[System.NonSerialized]public bool shouldCount = false;
	bool learningPhasePause = false;

	void Start () {
		timeCount = timeLimit;
		mazeManager = MazeManager.instance;
		logManager = TrackPlayer.instance;
        intermissionManager = IntermissionManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldCount){
			CountDown();
		}
	}
	private void CountDown(){
		timeDisplay.text = "Current Level = " + (mazeManager.currentLevel - 2) + " | Time left: " + timeCount.ToString();
		if(timeCount > 0)
		{
			timeCount -= Time.deltaTime;

			if(completed)
			{
				Debug.Log("Trial successful");
				Debug.Log("Time Remaining: " + timeCount);
				logManager.WriteLevelFinishInfo(completed, timeLimit - timeCount);
                if (mazeManager.currentLevel == 2)
                {
                    Reset();
                    mazeManager.ChooseLevel();
                    mazeManager.UnloadLevel();
                    learningPhasePause = true;
                    Debug.Log("pause");
                }
                else if (mazeManager.currentLevel < 2) {
                    Reset();
                    mazeManager.ChooseLevel();
                    mazeManager.UnloadLevel();

                    StartCoroutine(NextLevel(3.0f));
                }
                else { 
					Reset();
					mazeManager.ChooseLevel();
                    mazeManager.UnloadLevel();

                    //StartCoroutine(NextLevel(3.0f));
                    intermissionManager.ActivatePoints();
				}
			}
		}
		else
		{
			Debug.Log("Trial unsuccessful");
			logManager.WriteLevelFinishInfo(completed, timeLimit);
			Reset();
			mazeManager.ChooseLevel();
			mazeManager.UnloadLevel();

            //mazeManager.PrepareLevel();
            intermissionManager.ActivatePoints();
        }
		
	}
	public void Reset(){
		shouldCount = false;
		completed = false;
		timeCount = timeLimit;
		DeactivateDisplay();
	}
	public void Run(){
		// timeCount = timeLimit;
		shouldCount = true;
		ActivateDisplay();
	}
	public void WriteTimeDisplay(string message){
		timeDisplay.text = message;
	}
	private void ActivateDisplay(){
		timeDisplay.text = timeCount.ToString();
	}
	private void DeactivateDisplay(){
		timeDisplay.text = "";
	}
	private IEnumerator NextLevel(float waitTime)
    {

        yield return new WaitForSecondsRealtime(waitTime);
		mazeManager.PrepareLevel();
    }

	void OnGUI(){
		if (learningPhasePause){
			if (GUI.Button(new Rect(60, 60, 250, 60), "Instruction time. Click here to continue")){
				
				StartCoroutine(NextLevel(3.0f));
				learningPhasePause = false;
			}
		}
	}
}
