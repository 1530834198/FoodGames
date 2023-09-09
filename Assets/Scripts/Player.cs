using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }
    public void PlayerMove() {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime *3;
        float v = Input.GetAxis("Vertical") * Time.deltaTime * 3;
        transform.Translate(new Vector3(h, 0, v));
    }
}
