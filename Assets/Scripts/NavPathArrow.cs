using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 导航箭头
/// </summary>
public class NavPathArrow : MonoBehaviour
{
    public GameObject player;
    private GameObject demo1 = null;
    private GameObject ntm = null;
    public GameObject hideBtn;
    public GameObject taskUI;
    public Text showinfo;//物品介绍
    public MeshRenderer meshRenderer;//箭头3D对象Quad
    //public List<Transform> points = new List<Transform>();//路径点
    private MeshRenderer line ;//显示的路径
    private Vector3 currentPos;//初始位置

    public float xscale = 1f;//缩放比例
    public float yscale = 1f;
    private bool isHide=false;

    void Start()
    {
        //箭头宽度缩放值
        xscale = meshRenderer.transform.localScale.x;
        //箭头长度缩放值
        yscale = meshRenderer.transform.localScale.y;
        currentPos = player.transform.position;
        //print(currentPos);
    }
    void Update()
    {
        if (demo1 != null && isHide==false)
        {
            if (currentPos != player.transform.position)
            {
                currentPos = player.transform.position;
                HidePath();
                DrawPath(demo1);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!taskUI.activeSelf)
            {
                //鼠标隐藏
                Cursor.visible = true;
                //鼠标锁定
                Cursor.lockState = CursorLockMode.Confined;
                taskUI.SetActive(true);
            }
            else
            {
                //鼠标隐藏
                Cursor.visible = false;
                //鼠标锁定
                Cursor.lockState = CursorLockMode.Locked;
                taskUI.SetActive(false);
            }
        }
        if (GameObject.Find("Quad(Clone)"))
        {
            hideBtn.SetActive(true);
        }
        else
        {
            hideBtn.SetActive(false);
        }
    }
    //画路径
    public void DrawPath(GameObject demo1)
    {
        if (GameObject.Find("Quad(Clone)"))
        {
            HidePath();
        }
        this.demo1 = demo1;
        DrawLine(player.transform.position, demo1.transform.position);
        isHide = false;
    }


    //隐藏路径
    public void HidePath()
    {
        if (GameObject.Find("Quad(Clone)")) {
            line.gameObject.SetActive(false);
            isHide = true;
        }
        
    }

    //画路径
    private void DrawLine(Vector3 start, Vector3 end)
    {
        MeshRenderer mr;
        mr = Instantiate(meshRenderer);
        line = mr;

        var tran = mr.transform;
        var length = Vector3.Distance(start, end);
        tran.localScale = new Vector3(xscale, length, 1);
        tran.position = (start + end) / 2;
        //指向end
        tran.LookAt(end);
        //旋转偏移
        tran.Rotate(90, 0, 0);
        mr.material.mainTextureScale = new Vector2(1, length * yscale);
        mr.gameObject.SetActive(true);
    }

    //追踪按钮
    public void OnBtnClick(GameObject ntm) {
        if (this.ntm!=null)
        {
            this.ntm.SetActive(false);
        }
        this.ntm = ntm;
        ntm.SetActive(true);
    }
    //任务介绍
    public void OnBtnClickByItemInfo(Item item){
        showinfo.text = item.itemInfoByGame;
    }
}

