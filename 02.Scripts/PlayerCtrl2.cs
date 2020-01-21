using UnityEngine;
using System.Collections;

//클래스는 System.Serializable이라는 어트리뷰트(Attribute)를 명시해야 
//Inspector 뷰에 노출됨
[System.Serializable]
public class Anim2
{
    public AnimationClip idle;
    public AnimationClip down;
}

public class PlayerCtrl2 : MonoBehaviour {
    private float h = 0.0f;//좌우
    private float v = 0.0f;//앞뒤
    private bool col_check = true;
    private bool isDeath2 = false;//죽음 판단변수
    private bool isVictory2 = false;//탈출 판단변수
    private int Deathcnt = 0;
    private int LRcnt = 0;//좌우 이동판단변수
    private int UDcnt = 0;//상하 이동판단변수
    private bool pause_cnt = false;//일시정지 기능 판단변수
    //자주 사용하는 컴포넌트는 반드시 변수에 할당한 후 사용
    private Transform tr;
    //이동 속도 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 20.0f;

    //함정 생성 배열
    //public GameObject Trap;
    private int trapcnt = 0;
    private int trapnum;
    GameObject[] tempGO = new GameObject[10];
    public GameObject Trapobject;
    private int trap_make = 140;
    private int down_num = 140;
    //스타트넘버출력
    private int Start_Timer = 0;
    public GameObject Startnum3;
    public GameObject Startnum2;
    public GameObject Startnum1;
    public GameObject Startnum0;

    //인스펙터뷰에 표시할 애니메이션 클래스 변수
    public Anim2 anim;
    //아래에 있는 3D 모델의 Animation 컴포넌트에 접근하기 위한 변수
    public Animation _animation;

    //GameUI에 접근하기 위한 변수
    private GameUI gameUI; private bool Score_start = false;
    //TrapMgr에 접근하기 위한 변수
    private TrapMgr2 trap_camera;

    //기본 효과음
    public AudioClip death_sound;
    public AudioClip bgm_sound;
    public AudioClip victory_sound;
    //소리를 저장할 변수
    public AudioSource source = null;

    void Awake()//트랩 배열 저장
    {
        tempGO[0] = GameObject.Find("Trap1");
        tempGO[1] = GameObject.Find("Trap2");
        tempGO[2] = GameObject.Find("Trap3");
        tempGO[3] = GameObject.Find("Trap4");
        tempGO[4] = GameObject.Find("Trap5");
        tempGO[5] = GameObject.Find("Trap6");
        tempGO[6] = GameObject.Find("Trap7");
        tempGO[7] = GameObject.Find("Trap8");
        tempGO[8] = GameObject.Find("Trap9");
    }

    // Use this for initialization
    void Start () {
        //시작시 로딩하기
        Start_Timer = 0;
        Startnum0.SetActive(false); Startnum1.SetActive(false);
        Startnum2.SetActive(false); Startnum3.SetActive(false);
        //GameUI 스크립트 할당
        gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
        trap_camera = GameObject.Find("TrapObject").GetComponent<TrapMgr2>();
        //스크립트 처음에 Transform 컴포넌트 할당
        tr = GetComponent<Transform>();
        //자신의 하위에 있는 Animation 컴포넌트를 찾아와 변수에 할당
        _animation = GetComponentInChildren<Animation>();

        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        _animation.clip = anim.down;
        _animation.Play();
        //기본 소리 지정
        this.source = this.gameObject.AddComponent<AudioSource>();
        source.clip = bgm_sound;
        source.loop = true;
        source.Play();
        source.volume = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxis("Vertical");
        //s = Input.GetAxis("Slide");
        if (Start_Timer == 0)
        {
            Time.timeScale = 0.0f;
        }

        //Timer 가 90보다 작거나 같을경우 Timer 계속증가
        if (Start_Timer <= 100)
        {
            Start_Timer++;
            // Timer가 30보다 작을경우 3번켜기
            if (Start_Timer < 30)
            {
                Startnum3.SetActive(true);
            }
            // Timer가 30보다 클경우 3번끄고 2번켜기
            if (Start_Timer > 30)
            {
                Startnum3.SetActive(false); Startnum2.SetActive(true);
            }
            // Timer가 60보다 작을경우 2번끄고 1번켜기
            if (Start_Timer > 70)
            {
                Startnum2.SetActive(false); Startnum1.SetActive(true);
            }
            //Timer 가 90보다 크거나 같을경우 1번끄고 GO 켜기 LoadingEnd () 코루틴호출 
            if (Start_Timer >= 100)
            {
                Startnum1.SetActive(false); Startnum0.SetActive(true);
                StartCoroutine(this.LoadingEnd());
                Time.timeScale = 1.0f; //게임시작
            }
        }
        
        //함정 생성 이벤트(점점 빠르게)
        if (trap_make > down_num && !isDeath2 && !pause_cnt)
        {
            trap_make--;
        }
        if(!isDeath2 && !pause_cnt) trapcnt++;
        if (trapcnt == trap_make && Trapobject.transform.position.y > -4000 &&
            !isDeath2 && !isVictory2 && !pause_cnt)
        {
            trapnum = Random.Range(0, 9);
            Debug.Log(trapnum);
            Instantiate(tempGO[trapnum], Trapobject.transform.position, tempGO[trapnum].transform.rotation);
            trapcnt = 0;
            down_num--;
        }

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
        //앞뒤 이동 방향 벡터 계산
        if (Input.GetButtonDown("Vertical"))
        {
            if (v > 0 && UDcnt < 1)
            {
                UDcnt++;
                Vector3 moveDir2 = (Vector3.forward * 1);
                tr.Translate(moveDir2 * 1, Space.Self);
                Debug.Log("up,down = " + v);
            }
            if (v < 0 && UDcnt > -1)
            {
                UDcnt--;
                Vector3 moveDir2 = (Vector3.forward * -1);
                tr.Translate(moveDir2 * 1, Space.Self);
                Debug.Log("up,down = " + v);
            }
        }
        //자동이동
        //Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표)
        if (col_check && Score_start && !pause_cnt)
        {
            gameUI.DispScore(1);
            tr.Translate(Vector3.down * Time.deltaTime * moveSpeed, Space.Self);
        }
        
        //죽는 이벤트
        if (isDeath2 && Deathcnt == 0)
        {
            Deathcnt = 1;
            trap_camera.isDeath();
            gameUI.isDeath2();
            Debug.Log("death!!");
            Time.timeScale = 0;
            source.Stop();
            source.PlayOneShot(death_sound, 0.7f);
        }
        //탈출 성공시
        if (isVictory2)
        {
            trap_camera.isDeath();
            gameUI.isVictory2();
            Time.timeScale = 0;
        }
        //일시정지 이벤트
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause_cnt)
            {
                pause_cnt = false;
                gameUI.Pause_off();
                Time.timeScale = 1;
            }
            else if (!pause_cnt)
            {
                pause_cnt = true;
                gameUI.Pause_on();
                Time.timeScale = 0;
            }
        }
    }

    //로딩 이벤트
    IEnumerator LoadingEnd()
    {
        Score_start = true;
        yield return new WaitForSeconds(1.0f);
        Startnum0.SetActive(false);
        Score_start = true;
    }
    //함정 충돌 이벤트
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Trap")
        {
            col_check = false;
            isDeath2 = true;
            Debug.Log("death!!");
        }
    }
    //탈출 이벤트
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Victory")
        {
            col_check = false;
            source.Stop();
            source.PlayOneShot(victory_sound, 0.5f);
            isVictory2 = true;
            Debug.Log("Victory");
        }
    }
}
