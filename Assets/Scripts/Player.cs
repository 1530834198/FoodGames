﻿using System;
using UnityEngine;

namespace Cinemachine.Examples
{

    [AddComponentMenu("")] // Don't display in add component menu
    public class Player : MonoBehaviour
    {
        public bool useCharacterForward = false;//用于确定角色是否应该根据自己的前方方向移动
        public bool lockToCameraForward = false;//可能用于锁定角色的移动方向到摄像机的前方。
        public float turnSpeed = 10f;//角色的旋转速度

        private float turnSpeedMultiplier;//调整旋转速度
        private float speed = 0f;//角色的移动速度
        private float direction = 0f;//表示角色的方向

        private Animator anim;
        private Vector3 targetDirection;//一个Vector3，表示角色应该面向的方向
        private Vector2 input;//存储水平和垂直输入以控制角色的移动
        private Quaternion freeRotation;//用于控制自由旋转
        private Camera mainCamera;//主摄像机的引用
        private float velocity;//平滑地处理角色的速度变化

        public GameObject myBag;//背包
        public GameObject fk;//第三人称视角
        
        private bool isOpen;//判断背包是否打开
        public Inventory playerInventory;//背包
        

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            mainCamera = Camera.main;
            //鼠标隐藏
            Cursor.visible = false;
            //鼠标锁定
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        /**
         * 角色的移动和旋转，计算速度并更新动画参数
         */
        private void Update()
        {
            OpenMyBag();
        }
        void FixedUpdate()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            // set speed to both vertical and horizontal inputs
            if (useCharacterForward)
                speed = Mathf.Abs(input.x) + input.y;
            else
                speed = Mathf.Abs(input.x) + Mathf.Abs(input.y);

            speed = Mathf.Clamp(speed, 0f, 1f);
            speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
            anim.SetFloat("Speed", speed);

            if (input.y < 0f && useCharacterForward)
                direction = input.y;
            else
                direction = 0f;

            anim.SetFloat("Direction", direction);

           

            // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
            UpdateTargetDirection();
            if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = mainCamera.transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
            }
        }

        /**
         * 更新目标方向，该方向相对于摄像机视图来决定（或者根据 useCharacterForward 标志）。这个方法影响角色的移动方向。
         */
        public virtual void UpdateTargetDirection()
        {
            if (!useCharacterForward)
            {
                turnSpeedMultiplier = 1f;
                var forward = mainCamera.transform.TransformDirection(Vector3.forward);
                forward.y = 0;
        
                //get the right-facing direction of the referenceTransform
                var right = mainCamera.transform.TransformDirection(Vector3.right);
        
                // determine the direction the player will face based on input and the referenceTransform's right and forward directions
                targetDirection = input.x * right + input.y * forward;
            }
            else
            {
                turnSpeedMultiplier = 0.2f;
                var forward = transform.TransformDirection(Vector3.forward);
                forward.y = 0;
        
                //get the right-facing direction of the referenceTransform
                var right = transform.TransformDirection(Vector3.right);
                targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
            }
        }
        void OpenMyBag()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                isOpen = !isOpen;
                myBag.SetActive(isOpen);
                fk.SetActive(!isOpen);
                Cursor.visible = isOpen;
                Cursor.lockState = CursorLockMode.Confined;
            }
        }

        void SaveByPlayerPrefs()
        {
            PlayerPrefs.SetFloat("PlayerPositionX",transform.position.x);
            PlayerPrefs.SetFloat("PlayerPositionY",transform.position.y);
            PlayerPrefs.SetFloat("PlayerPositionZ",transform.position.z);
            PlayerPrefs.Save();
        }

        void LoadByPlayerPrefs()
        {
            transform.position = new Vector3(
                PlayerPrefs.GetFloat("PlayerPositionX",0f),
                PlayerPrefs.GetFloat("PlayerPositionY",0f),
                PlayerPrefs.GetFloat("PlayerPositionZ",0f));
        }
    }

}
