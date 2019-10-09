using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionSwitch : MonoBehaviour {
     public string scenename;
	[System.NonSerialized]public FadeManager fadeManager;
	[System.NonSerialized]public bool switching = false;
	[System.NonSerialized]public int i = 0;
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
    

    void OnTriggerEnter(Collider collision)
    {
        if (i == 0)
        {
            switching = true;
            fadeManager.switching = switching;
            StartCoroutine(Loading_scene());
            i++;
        }
    }

    IEnumerator Loading_scene()
    {

        yield return new WaitForSecondsRealtime(whiteOutTime);
        SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Single);
    }
    void OnDestroy()
    {
        switching = false;
        fadeManager.switching = switching;
    }
}
