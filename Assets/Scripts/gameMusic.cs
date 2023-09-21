using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameMusic : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject video;
    public GameObject bgm;
    public GameObject bgmprefab;
    void Start()
    {
        bgm = GameObject.FindGameObjectWithTag("menuBgm");//先去找有没有bgm
        //没有实例化一个
        if (bgm == null)
        {
            bgm = Instantiate(bgmprefab);
        }
        bgm.GetComponent<AudioSource>().volume = 0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        OpenVideo();
        CloseVideo();
    }
    void OpenVideo()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            video.SetActive(true);
            bgm.GetComponent<AudioSource>().Pause();
        }

    }
    void CloseVideo()
    {
        var videoplayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
        if (videoplayer.frame == (long)(videoplayer.frameCount - 1))
        {
            video.SetActive(false);
            bgm.GetComponent<AudioSource>().Play();
        }
    }
}
