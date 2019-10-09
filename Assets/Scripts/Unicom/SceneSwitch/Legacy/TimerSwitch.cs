using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerSwitch : MonoBehaviour {
    public string scenename;
	[System.NonSerialized]public FadeManager fadeManager;
	[System.NonSerialized]public bool switching = false;
	[System.NonSerialized]public int i = 0;
	
    [SerializeField] int wait_time = 60;
    [SerializeField] int whiteOutTime = 5;
    // Use this for initialization
	void Start () {
		fadeManager = FadeManager.Instance;
		if (fadeManager != null){
			Debug.Log("fademanager found");
		} else {
			Debug.LogError("fademanager not found");
		}
	}
	
    void Update()
    {
        if (Time.timeSinceLevelLoad > wait_time && i == 0)
        {
            switching = true;
            fadeManager.switching = switching;
            StartCoroutine(Switching_auto());
            i = 1;
        }
    }

    IEnumerator Switching_auto()
    {
        Debug.Log("auto switching in " + whiteOutTime + " seconds");
        yield return new WaitForSecondsRealtime(whiteOutTime);
        SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Single);
    }
    void OnDestroy()
    {
        switching = false;
        fadeManager.switching = switching;
        i = 0;
    }

}
