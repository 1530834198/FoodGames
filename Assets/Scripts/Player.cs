using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator Anim;
    private int speed = 3;
    Camera mainCamera;
    public float turnSpeed=5;
    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
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
    private void FixedUpdate()
    {
        //获取终点方向Y轴数据
        float playerCamera = mainCamera.transform.rotation.eulerAngles.y;
        //球形差值（起始方向,终点方向,速度）
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, playerCamera, 0), turnSpeed *Time.fixedDeltaTime);
    }
}
