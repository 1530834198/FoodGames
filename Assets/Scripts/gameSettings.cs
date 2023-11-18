using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameSettings : MonoBehaviour
{
    public GameObject Setting;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Setting.SetActive(true);
            //鼠标隐藏
            Cursor.visible = true;
            //鼠标锁定
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
    public void OnResumeGameClick()
    {
        if (Setting.activeSelf)
        {
            Setting.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    public void OnExitGameClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//unity调试时使用
#else
            Application.Quit();//打包后使用
#endif
    }
}
