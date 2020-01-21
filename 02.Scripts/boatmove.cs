using UnityEngine;
using System.Collections;

public class boatmove : MonoBehaviour {
    private float h = 0.0f;//좌우
    private int LRcnt = 0;//좌우 이동판단변수
    private bool col_check = true;//움직임 판단 변수
    private Transform tr;
    //이동 속도 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 10.0f;
    // Use this for initialization
    void Start()
    {
        //스크립트 처음에 Transform 컴포넌트 할당
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        //좌우 이동 방향 벡터 계산
        if (Input.GetButtonDown("Horizontal"))
        {
            if (h > 0) LRcnt++;
            if (h < 0) LRcnt--;
            if (LRcnt <= 1 && LRcnt >= -1)
            {
                Vector3 moveDir = (Vector3.right * h);
                tr.Translate(moveDir * 3, Space.Self);
            }
            if (LRcnt > 1) LRcnt = 1;
            if (LRcnt < -1) LRcnt = -1;
        }
        //자동전진
        if (col_check)
        {
            tr.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        }
    }
    public void isDeath()
    {
        col_check = false;
    }
    //함정 충돌 이벤트
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Trap")
        {
            col_check = false;
        }
    }
}
