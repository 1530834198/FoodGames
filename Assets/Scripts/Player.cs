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
    /**
     * 角色的移动动画
     */
    public void Charachter() {
        //判断我keyDown的按键
        bool isWalking = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        //触发动画
        Anim.SetBool("walk", isWalking);
    }
}
