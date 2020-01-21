using UnityEngine;
using System.Collections;

public class TrapMgr2 : MonoBehaviour {
    private Transform trap;
    private bool col_check = true;
    //이동 속도 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 20.0f;
    // Use this for initialization
    void Start()
    {
        trap = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (col_check)
        {
            trap.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.Self);
        }
    }
    public void isDeath()
    {
        col_check = false;
    }
}
