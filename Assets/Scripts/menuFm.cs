﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuFm : MonoBehaviour
{
    public GameObject bgm;
    public GameObject bgmprefab;

    public GameObject bgmPanel;
    //public GameObject video;

    // Start is called before the first frame update
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
        //OpenVideo();
        //CloseVideo();
    }
    /**
     * 开始游戏
     */
    public void OnSatrtGame()
    {
        SceneManager.LoadScene(1);
    }

    /**
     * bgm静音
     */
    public void OnToggle(bool isOn)
    {
        if (!isOn)//没选择
        {
            bgm.GetComponent<AudioSource>().Play();
        }
        else
        {
            //选中了
            bgm.GetComponent<AudioSource>().Pause();
        }
    }

    /**
     * 关闭音量设置panel
     */
    public void OnCloseBgmBtnClick()
    {
        bgmPanel.SetActive(false);
    }

    /**
     * 开启音量设置panel
     */
    public void OnstartBgmBtnClick()
    {
        bgmPanel.SetActive(true);
    }

    /**
     * 音量控制
     */
    public void OnSliderChange(float value)
    {
        bgm.GetComponent<AudioSource>().volume = value;
    }
    /**
     * 退出游戏
     */
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unity调试时使用
#else
            Application.Quit();//打包后使用
#endif
    }
    //void OpenVideo()
    //{
    //    if (Input.GetKeyDown(KeyCode.P))
    //    {
    //        video.SetActive(true);
    //        bgm.GetComponent<AudioSource>().Pause();
    //    }

    //}
    //void CloseVideo()
    //{
    //    var videoplayer = video.GetComponent<UnityEngine.Video.VideoPlayer>();
    //    if (videoplayer.frame == (long)(videoplayer.frameCount - 1))
    //    {
    //        video.SetActive(false);
    //        bgm.GetComponent<AudioSource>().Play();
    //    }
    //}

}
