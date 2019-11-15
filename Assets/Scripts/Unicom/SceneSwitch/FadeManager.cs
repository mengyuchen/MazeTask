using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Author: Mengyu Chen, 2019
//For questions: mengyuchenmat@gmail.com
public class FadeManager : MonoBehaviour
{
    public static FadeManager instance { set; get; }
    [SerializeField] Image fadeImage;
    [SerializeField] Color fadeColor;
    [SerializeField] float fadeDuration = 0.6f;
    [SerializeField] float fadeInDuration = 0.1f;
    [SerializeField] float fadeOutDuration = 0.5f;
    [SerializeField] AnimationCurve animationCurve;
    public bool Fading
    {
        get
        {
            return fading;
        }
    }
    private bool fading = false;

    private Color defaultColor;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start()
    {
        defaultColor = fadeImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fade()
    {
        StartCoroutine(CurveFade(defaultColor, fadeColor, fadeDuration));
    }
    public void FadeIn()
    {
        //Debug.Log(fading);
        StartCoroutine(LinearFade(defaultColor, fadeColor, fadeInDuration));
    }
    public void FadeOut()
    {
        StartCoroutine(LinearFade(fadeColor, defaultColor, fadeOutDuration));
    }
    public void ResetFadeOut()
    {
        StartCoroutine(LinearFade(fadeColor, defaultColor, fadeOutDuration / 4));
    }
    public void QuickFade()
    {
        StartCoroutine(CurveFade(defaultColor, fadeColor, fadeDuration / 2));
    }
    public void ResetFadingStatus()
    {
        fading = false;
    }
    IEnumerator LinearFade(Color origin, Color target, float duration)
    {
        while (fading)
        {
            
            yield return null;
        }
        fading = true;
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            
            fadeImage.color = Color.Lerp(origin, target, percent);
            yield return null;
        }
        fading = false;
    }
    IEnumerator CurveFade(Color origin, Color target, float duration)
    {
        while (fading)
        {
            yield return null;
        }
        fading = true;
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            float curvePercent = animationCurve.Evaluate(percent);

            fadeImage.color = Color.Lerp(origin, target, curvePercent);
            yield return null;
        }
        fading = false;
    }
}
