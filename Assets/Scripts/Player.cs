using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator Anim;
    private int speed = 3;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Charachter();
    }
    public void PlayerMove() {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime *3;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * 3;
        Vector3 move = new Vector3(h, 0, v);    //获取坐标系
        Vector3 to = transform.position + move;    //要看向的目标点
        transform.LookAt(to);   //player转动方向
        transform.position += move * speed * Time.deltaTime;    //player移动
    }
    public void Charachter() {
        if (Input.GetKeyDown(KeyCode.W)) //向前
        {
            Anim.SetBool("walk", true);
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            Anim.SetBool("walk", false);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Anim.SetBool("left", true);
        }
        if (Input.GetKeyUp(KeyCode.A))//向左走
        {
            Anim.SetBool("left", false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Anim.SetBool("right", true);
        }
        if (Input.GetKeyUp(KeyCode.D))//向右走
        {
            Anim.SetBool("right", false);
        }
    }
}
