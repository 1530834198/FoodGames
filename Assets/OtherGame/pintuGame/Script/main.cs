using UnityEngine;
using System.Collections;

public class main : MonoBehaviour
{

    public GameObject _plane;        //用来实例碎片的对象
    public GameObject _planeParent; //碎片对象所要绑定的父节点
    public GameObject _background;    //显示暗色的背景图
    public Texture2D[] _texAll;        //用来更换的纹理
    public Vector3[] _RandomPos;    //开始时, 碎片随机分布的位置
    public int raw = 3;                //图形切分的行数
    public int volumn = 3;            //图形切分的列数
    public float factor = 0.25f;    //一个范围比例因子, 用来判断碎片是否在正确位置范围内

    GameObject[] _tempPlaneAll;

    float sideLength = 0;            //背景图的边长(正方形)

    int finishCount = 0;            //完成的碎片数量
    int _index = 0;

    Vector2 originPoint;            //第一个碎片的位置
    Vector2 space;                    //碎片与碎片之间的间隔(中心距x,y)、
    public GameObject Npcqiuqiu;

    void Start()
    {
        sideLength = _background.transform.localScale.x;
        space.x = sideLength / volumn;
        space.y = sideLength / raw;
        originPoint.x = -((volumn - 1) * space.x) / 2;
        originPoint.y = ((raw - 1) * space.y) / 2;
        Vector2 range;
        range.x = space.x * factor * 10f;
        range.y = space.y * factor * 10f;

        _tempPlaneAll = new GameObject[raw * volumn];
        int k = 0;
        //完成所有碎片的有序排列位置和uv纹理的截取
        for (int i = 0; i != raw; ++i)
        {
            for (int j = 0; j != volumn; ++j)
            {
                GameObject tempObj = (GameObject)Instantiate(_plane);
                tempObj.name = "Item" + k;
                tempObj.transform.parent = _planeParent.transform;
                tempObj.transform.localPosition = new Vector3((originPoint.x + space.x * j) * 10f, (originPoint.y - space.y * i) * 10f, 0);
                tempObj.transform.localScale = new Vector3(space.x, 1f, space.y);
                Vector2 tempPos = new Vector2(originPoint.x + space.x * j, originPoint.y - space.y * i);

                float offset_x = (tempPos.x <= 0 + Mathf.Epsilon) ? (0.5f - Mathf.Abs((tempPos.x - space.x / 2) / sideLength)) : (0.5f + (tempPos.x - space.x / 2) / sideLength);
                float offset_y = (tempPos.y <= 0 + Mathf.Epsilon) ? (0.5f - Mathf.Abs((tempPos.y - space.y / 2) / sideLength)) : (0.5f + (tempPos.y - space.y / 2) / sideLength);

                float scale_x = Mathf.Abs(space.x / sideLength);
                float scale_y = Mathf.Abs(space.y / sideLength);

                tempObj.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offset_x, offset_y);
                tempObj.GetComponent<Renderer>().material.mainTextureScale = new Vector2(scale_x, scale_y);
                tempObj.SendMessage("SetRange", range);

                _tempPlaneAll[k] = tempObj;
                ++k;
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartGame();
        }
    }
    void StartGame()
    {
        //将所有碎片随机分布在左右两边
        for (int i = 0; i != _tempPlaneAll.Length; ++i)
        {
            int tempRank = Random.Range(0, _RandomPos.Length);
            _tempPlaneAll[i].transform.localPosition = new Vector3(_RandomPos[tempRank].x, _RandomPos[tempRank].y, 0f);
        }
        //通知所有子对象, 开始游戏
        gameObject.BroadcastMessage("Play");
        _plane.SetActive(false);
    }

    void SetIsMoveFale()
    {
        gameObject.BroadcastMessage("IsMoveFalse");
    }

    void IsFinish()
    {
        //计算放置正确的碎片数量
        ++finishCount;
        if (finishCount == raw * volumn)
            Npcqiuqiu.GetComponent<Npcqiuqiu>().setFinishCount(finishCount);
    }

}