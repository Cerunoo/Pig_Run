using System.Collections;
using UnityEngine;

public class PointView : MonoBehaviour
{
    public float minTime;
    public float maxTime;

    Animator anim;
    int clipsLength;

    string onlyClip;
    bool isStopWork;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        clipsLength = anim.runtimeAnimatorController.animationClips.Length;
        StartCoroutine(pointClipUpdate());
    }

    IEnumerator pointClipUpdate()
    {
        yield return new WaitForSeconds(Random.Range(minTime, maxTime));
        if (!isStopWork) 
        {
            onlyClip = "view" + Random.Range(1, clipsLength + 1);
            anim.SetBool(onlyClip, true);
        }
    }

    public void OffClip() {
        anim.SetBool(onlyClip, false);
        StartCoroutine(pointClipUpdate());
    }

    public void StopWork () {
        for (int i = 1; i <= clipsLength; i++) 
        {
            string clip = "view" + i;
            anim.SetBool(clip, false);
        }
        gameObject.GetComponent<PointView>().enabled = false;
        isStopWork = true;
    }
}
