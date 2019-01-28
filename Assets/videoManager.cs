using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoManager : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoClip;

    void Start(){
        videoClip = gameObject.GetComponent<UnityEngine.Video.VideoPlayer>();
        StartCoroutine(playVideo());
    }

    IEnumerator playVideo(){
        videoClip.Play();
        yield return new WaitForSeconds(29f);
        LevelChangerScript.Instance.FadeToNextLevel("World1Level1");
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }
}
