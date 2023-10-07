using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChickenTest : MonoBehaviour
{
    private bool isFlag;
    private void Update()
    {
        if (isFlag && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(2);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isFlag = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        isFlag = false;
    }
}
