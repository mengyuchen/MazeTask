using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLauncher : MonoBehaviour
{
    [SerializeField] KeyCode refreshKey = KeyCode.N; //in case something happened
    int loadedLevelBuildIndex;
    FadeManager fadeManager;
    public static LevelLauncher instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        fadeManager = FadeManager.instance;

        //initial check if there is anything already accidentally loaded
        if (Application.isEditor) {
			for (int i = 0; i < SceneManager.sceneCount; i++) {
				Scene loadedScene = SceneManager.GetSceneAt(i);
                //make sure level contains word Maze
				if (loadedScene.name.Contains("Maze")) {
					SceneManager.SetActiveScene(loadedScene);
					loadedLevelBuildIndex = loadedScene.buildIndex;
					return;
				}
			}
        }
    }

    // Update is called once per frame
    void Update()
    {
        //refresh and reload the current scene, in case any bug happens.
        if (loadedLevelBuildIndex > 0){
            if (Input.GetKeyDown(refreshKey)){ 
                StartCoroutine(LoadLevel(loadedLevelBuildIndex));
            } 
        }

    }

    //"selectLevel(int)" function is to be called by MazeManager after checking the arrowmanager
    public void SelectLevel(int levelIndex){
        StartCoroutine(LoadLevel(levelIndex));
        //Debug.Log(levelName);
    }

    //levelCheck() is mostly an unload level function
    //it is to be called before the arrowManager so that the scene goes back to empty, and arrow can appear  
    public void LevelCheck(){
        // Debug.Log("Active? "+gameObject.activeInHierarchy);  
        StartCoroutine(UnloadLevel());
        //Debug.Log(levelName);
    }

    IEnumerator LoadLevel (int levelBuildIndex) {
		enabled = false; // <-- this is the Unity Behavior enable bool
        fadeManager.FadeIn();

        //unload existing scene
		if (loadedLevelBuildIndex > 0) {
			yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
		}

		//async loading for multiple frames, can also show a loading screen at this point
		yield return SceneManager.LoadSceneAsync(
			levelBuildIndex, LoadSceneMode.Additive
		);
		SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(levelBuildIndex));
		loadedLevelBuildIndex = levelBuildIndex;
		enabled = true;
        fadeManager.FadeOut();
	}
    IEnumerator UnloadLevel(){
        enabled = false; // <-- this is the Unity Behavior enable bool
        fadeManager.FadeIn();
        if (loadedLevelBuildIndex > 0) {
			yield return SceneManager.UnloadSceneAsync(loadedLevelBuildIndex);
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
		loadedLevelBuildIndex = 0;
		enabled = true;
        fadeManager.FadeOut();
    }

}
