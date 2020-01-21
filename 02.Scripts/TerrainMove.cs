using UnityEngine;
using System.Collections;

public class TerrainMove : MonoBehaviour {
    private Rigidbody rigdbody;
    //자주 사용하는 컴포넌트는 반드시 변수에 할당한 후 사용
    private Transform tr;
    //이동 속도 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 20.0f;
    // Use this for initialization
    void Start () {
        //스크립트 처음에 Transform 컴포넌트 할당
        tr = GetComponent<Transform>();

        rigdbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //자동이동
        //Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표)
        if (tr.position.y>-3800)
        {
            tr.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.Self);
        }
    }
}
