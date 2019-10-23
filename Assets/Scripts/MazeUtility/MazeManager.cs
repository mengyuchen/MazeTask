using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public enum MazeMode : int{tutorial, learning, testing, full};
public class MazeManager : MonoBehaviour
{
    [Header("Managers")]
    static public MazeManager instance;
    [SerializeField] LevelLauncher levelManager;
    [SerializeField] ArrowManager arrowManager;
    [SerializeField] TimeManager timeManager;
    [SerializeField] TrackPlayer logManager;
    [SerializeField] FadeManager fadeManager;
    [SerializeField] TargetManager targetManager;
    [SerializeField] SpaceManager spaceManager;
    [Header("Level Mode")]
    public MazeMode currentMode;
    public int currentLevel;
    [Header("Random Level Loader")]
    private int levelcount = 22;
    private int testPhaseStartingIndex = 2;
    private int nextLevel;
    private bool missionComplete = false;
    // private int maxCount = 5;
    private HashSet<int> candidates = new HashSet<int>();
    System.Random random = new System.Random();
    [Header("Visibility Constant")]
    public float VisibilityDistance = 1.0f;
    void Awake()
    {
        if (instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    void Start(){
        // logManager = TrackPlayer.instance;
        if (fadeManager == null) fadeManager = FadeManager.instance;
        if (levelManager == null) levelManager = LevelLauncher.instance;
        if (arrowManager == null) arrowManager = ArrowManager.instance;
        if (timeManager == null) timeManager = TimeManager.instance;
        if (logManager == null) logManager = TrackPlayer.instance;
        if (targetManager == null) targetManager = TargetManager.instance;
        if (spaceManager == null) spaceManager = SpaceManager.instance;

        levelcount = arrowManager.startingPoints.Length;
    }

    // Update is called once per frame
    void Update()
    {       
        // //keyboard number based maze selection.
        // for (int i = 1; i <= levelManager.levelCount; i++) {
		// 	if (Input.GetKeyDown(KeyCode.Alpha0 + i)) { // maze selection by keyboard number shortcut
        //         levelManager.LevelCheck(); //check level and unload existing level
        //         currentLevel = i; // 
        //         Debug.Log(currentLevel - 1);
        //         arrowManager.Reset();
        //         arrowManager.activateArrow(currentLevel - 1);
		// 		return;
		// 	}
		// }

        // avoid selecting the same number that has been chosen.
        // if (Input.GetKeyDown(KeyCode.R)){ //press R to random generate a number, or whatever condition we define later
        //     currentLevel = Random.Range(0, levelManager.levelCount); // <- random number should write value into "currentLevel"
        //     Debug.Log(currentLevel);
        //     arrowManager.Reset();
        //     arrowManager.activateArrow(currentLevel - 1);
        // }
    }
    public void ResetManagers(){
        levelManager.LevelCheck();
        timeManager.Reset();
        logManager.Reset();
        arrowManager.Reset();
        
    }
    //steps: choose level -> prepare level -> loadlevel
    public void ChooseLevel(){
        GetRandomLevel(testPhaseStartingIndex);
        logManager.Reset();
    }
    public void UnloadLevel(){
        levelManager.LevelCheck();
    }
    public void PrepareLevel(){
        levelManager.LevelCheck(); //check level and unload existing level
        currentLevel = nextLevel; // 1 -> training scene
        arrowManager.Reset();
        targetManager.Reset();
        fadeManager.ResetFadeOut();
        if (missionComplete == false){
            if (currentLevel == 1){
                Debug.Log("preparing tutorial level");
            } else if (currentLevel == 2){
                Debug.Log("preparing learning level");
            } else {
                Debug.Log("preparing testing level:" + (currentLevel - 2));
            }
            arrowManager.Activate(currentLevel - 1);
        } else {
            timeManager.WriteTimeDisplay("Mission Complete");
            arrowManager.SetTextContent("Mission Complete. Thank you!");
            arrowManager.instructionText.transform.position = new Vector3(0,0.5f,0);
            arrowManager.instructionText.SetActive(true);
        }
    }
    public void LoadLevel(){
        levelManager.SelectLevel(currentLevel);
        StartCoroutine(WaitInit());
    }
    IEnumerator WaitInit()
    {
        while (levelManager.loading)
        {
            yield return null;
        }
        timeManager.Run();
        logManager.WriteLevelInfo();
        logManager.Run();
        targetManager.TargetSearch();
        spaceManager.LoadSpatialObjects();
    }
    public void CompleteMaze(){
        timeManager.completed = true;
    }
    public void MazeModeSelect(int mode){
        currentMode = (MazeMode)mode;
        Debug.Log("Selected Current Mode = " + currentMode);

        //if tutorial
        if (currentMode == MazeMode.tutorial || currentMode == MazeMode.full){
            nextLevel = 1; // be default learning level = 1
            timeManager.timeCount = int.MaxValue; // for learning purpose, timeCount goes infinite.
            PrepareLevel();
        }
        //if learning
        if (currentMode == MazeMode.learning){
            nextLevel = 2;
            timeManager.timeCount = int.MaxValue; // for learning purpose, timeCount goes infinite.
            PrepareLevel();
        }
        //if training
        if (currentMode == MazeMode.testing){
            ChooseLevel();
            PrepareLevel(); //to do: randomize prepare level
        }
    }
    private void GetRandomLevel(int min){
        //check if it's done
        if (candidates.Count == levelcount - min || currentMode == MazeMode.learning || currentMode == MazeMode.tutorial){
            missionComplete = true;
            ResetManagers(); // finishes
            Debug.Log("Mission Complete");
            return;
        }
        //check if its moving to learning phase
        if (currentMode == MazeMode.full && currentLevel == 1){
            nextLevel = 2;
            timeManager.timeCount = int.MaxValue;
            Debug.Log("Tutorial done. Move to next Learning phase");
            return;
        }
        while (candidates.Count != levelcount - min){
            int randNum = UnityEngine.Random.Range(min, levelcount);
            //Debug.Log(randNum + " = generated");
            if (candidates.Add(randNum))
            {
                nextLevel = randNum + 1;
                Debug.Log("level: " + (nextLevel - 2) + " picked");
                break;
            }
        }
    }

    public void SkipLevels(string levels)
    {
        int[] skipLevels = Array.ConvertAll(levels.Split(','), int.Parse);
        for (int i = 0; i < skipLevels.Length; i++)
        {
            candidates.Add(skipLevels[i] + 1);
            Debug.Log("skipped " + (skipLevels[i] + 1));
        }
        Debug.Log("remaining " + (levelcount - testPhaseStartingIndex - candidates.Count));
    }
}
