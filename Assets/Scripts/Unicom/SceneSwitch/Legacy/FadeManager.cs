using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour {
    public static FadeManager Instance { set; get; }
    [System.NonSerialized]public bool switching;
    [SerializeField] Image fadeImage;
    private bool isInTransition;
    private float transition;
    private bool isShowing;
    private float duration;
    private bool fading = false;

    private void Awake()
    {
        Instance = this;
        Fade(false, 2.0f);
    }
    private void Start(){
        // if (sceneSwitcher == null){
        //     Debug.LogError("SceneSwitcher not found!");
        // }
    }
	public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }

    private void Update()
    {
        if(switching)
        {
           
            if (fading == false)
            {
                Debug.Log("fading start");
                Fade(true, 1.75f);
                fading = true;
                //StartCoroutine(reset_fading());
            }
        } else if (switching == false)
        {
            fading = false;
        }

        if (!isInTransition)
        {
            return;
        }
        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);

        if(transition > 1 || transition < 0)
        {
            isInTransition = false;
        }
        
       
    }
    IEnumerator reset_fading()
    {

        yield return new WaitForSecondsRealtime(3);
        fading = false;
    }
}
