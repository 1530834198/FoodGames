using UnityEngine;
using System.Collections;

public class plane : MonoBehaviour
{

    Transform mTransform;

    Vector3 offsetPos;                    //鼠标点与所选位置的偏移
    Vector3 finishPos = Vector3.zero;    //当前碎片的正确位置

    Vector2 range;                        //碎片正确位置的范围, 由SetRange函数设置

    float z = 0;

    bool isPlay = false;                //是否进行游戏？
    bool isMove = false;                //当前碎片是否跟随鼠标移动

    void Start()
    {
        mTransform = transform;
        finishPos = mTransform.localPosition;
    }

    void Update()
    {
        if (!isPlay)
            return;

        //当鼠标进入碎片中按下时, 记录与碎片中心位置的偏差; 并使碎片跟随鼠标移动(多张碎片叠在一起时，只选其中一张跟随)
        Vector3 tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && tempMousePos.x > GetComponent<Collider>().bounds.min.x && tempMousePos.x < GetComponent<Collider>().bounds.max.x
            && tempMousePos.y > GetComponent<Collider>().bounds.min.y && tempMousePos.y < GetComponent<Collider>().bounds.max.y)
        {
            mTransform.parent.SendMessage("SetIsMoveFale");
            offsetPos = mTransform.position - tempMousePos;
            z = mTransform.position.z;
            isMove = true;
        }

        //跟随鼠标移动
        if (isMove && Input.GetMouseButton(0))
        {
            tempMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mTransform.position = new Vector3(tempMousePos.x + offsetPos.x, tempMousePos.y + offsetPos.y, z - 0.1f);
        }

        //鼠标放开后停止跟随
        if (Input.GetMouseButtonUp(0))
        {
            mTransform.position = new Vector3(mTransform.position.x, mTransform.position.y, z);
            isMove = false;
        }

        //判断是否到达正确位置(如进入正确位置范围, 碎片自动设置在正确位置, 并不可被再移动)
        IsFinish();
    }

    void IsFinish()
    {
        if (mTransform.localPosition.x > finishPos.x - range.x && mTransform.localPosition.x < finishPos.x + range.x
            && mTransform.localPosition.y > finishPos.y - range.y && mTransform.localPosition.y < finishPos.y + range.y)
        {
            isPlay = false;
            mTransform.localPosition = finishPos;
            mTransform.parent.SendMessage("IsFinish");
        }
    }

    //开始游戏
    void Play()
    {
        isPlay = true;
    }

    void IsMoveFalse()
    {
        isMove = false;
    }

    void SetRange(Vector2 _range)
    {
        range = _range;
    }

    //更换纹理
    void SetTexture(Texture2D _tex)
    {
        mTransform.GetComponent<Renderer>().material.mainTexture = _tex;
    }
}