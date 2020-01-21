using UnityEngine;
using System.Collections;

//클래스는 System.Serializable이라는 어트리뷰트(Attribute)를 명시해야 
//Inspector 뷰에 노출됨
[System.Serializable]
public class Anim3
{
    public AnimationClip idle;
    public AnimationClip jump;
    public AnimationClip stand;
    public AnimationClip slide;
    public AnimationClip death;
}

public class PlayerCtrl3 : MonoBehaviour {
    private float h = 0.0f;//좌우
    private bool col_check = true;
    private bool jumpcnt = false;
    private bool slidecnt = false;
    private float Slidetime = 0.7f;
    private float TickTime = 0;
    private int Scnt = 0;
    private Rigidbody rigdbody;
    private bool isGround = true;//지면 판단변수
    private bool isDeath = false;//죽음 판단변수
    private bool isVictory = false;//탈출 판단변수
    private int Deathcnt = 0;
    private int LRcnt = 0;//좌우 이동판단변수
    private bool pause_cnt = false;//일시정지 기능 판단변수
    //자주 사용하는 컴포넌트는 반드시 변수에 할당한 후 사용
    private Transform tr;
    //이동 속도 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveSpeed = 10.0f;
    //점프 크기 변수 (public으로 선언되어 Inspector에 노출됨)
    public float moveJump = 10.0f;

    //함정 생성 배열
    private int trapcnt = 0;
    private int trapnum;
    GameObject[] tempGO = new GameObject[10];
    public GameObject Trapobject;
    private int trap_make = 180;
    private int down_num = 180;
    //스타트넘버출력
    private int Start_Timer = 0;
    public GameObject Startnum3;
    public GameObject Startnum2;
    public GameObject Startnum1;
    public GameObject Startnum0;

    //인스펙터뷰에 표시할 애니메이션 클래스 변수
    public Anim3 anim;
    //아래에 있는 3D 모델의 Animation 컴포넌트에 접근하기 위한 변수
    public Animation _animation;

    //GameUI에 접근하기 위한 변수
    private GameUI gameUI; private bool Score_start = false;
    //TrapMgr에 접근하기 위한 변수
    private TrapMgr trap_camera;
    //메인카메라 접근 변수
    private FollowCam gamecamera;
    //기본 효과음
    public AudioClip idle_sound;
    public AudioClip slide_sound;
    public AudioClip jump_sound;
    public AudioClip death_sound;
    private int idle_cnt = 0;
    //소리를 저장할 변수
    public AudioSource source = null;

    void Awake()
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
        trap_camera = GameObject.Find("TrapObject").GetComponent<TrapMgr>();
        gamecamera = GameObject.Find("Main Camera").GetComponent<FollowCam>();
        //Time.timeScale = 1;
        //스크립트 처음에 Transform 컴포넌트 할당
        tr = GetComponent<Transform>();

        rigdbody = GetComponent<Rigidbody>();
        //자신의 하위에 있는 Animation 컴포넌트를 찾아와 변수에 할당
        _animation = GetComponentInChildren<Animation>();

        //Animation 컴포넌트의 애니메이션 클립을 지정하고 실행
        _animation.clip = anim.idle;
        _animation.Play();

        //기본 소리 지정
        //source = GetComponent<AudioSource>();
        this.source = this.gameObject.AddComponent<AudioSource>();
        source.clip = idle_sound;
        source.loop = true;
        source.Play();
        source.volume = 0;
    }
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxisRaw("Horizontal");
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
                source.volume = 0.5f;
            }
        }
        
        //함정 생성 이벤트(점점 빠르게)
        if (trap_make > down_num && !isDeath && !pause_cnt)
        {
            trap_make--;
        }
        if (!isDeath && !pause_cnt) trapcnt++;
        if (trapcnt == trap_make && Trapobject.transform.position.z < 2800 && 
            !isDeath && !isVictory && !pause_cnt)
        {
            trapnum = Random.Range(0, 9);
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
        //점프
        if (tr.position.y < -0.01 && !jumpcnt && !slidecnt)
        {
            isGround = true;
            _animation.CrossFade(anim.idle.name, 0.1f);
            source.volume = 0.5f;
            if (idle_cnt == 1)
            {
                idle_cnt = 0;
                source.Play();
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGround && !slidecnt)
            {
                jumpcnt = true;
            }
        }
        //슬라이드
        if (Input.GetButtonDown("Slide"))
        {
            if (isGround)
            {
                Scnt = 1;
                slidecnt = true;
                _animation.Play("slide");
                source.clip = slide_sound;
                source.volume = 0.2f;
                source.Play();
            }
        }
        if (Scnt == 1) TickTime += Time.deltaTime;
        if (TickTime >= Slidetime && slidecnt && !jumpcnt)
        {
            slidecnt = false;
            Scnt = 0;
            Slidetime = 0.7f;
            TickTime = 0;
            source.clip = idle_sound;
            source.Play();
            source.volume = 0.5f;
        }
        //자동전진
        //Translate(이동 방향 * Time.deltaTime * 변위값 * 속도, 기준좌표)
        if (col_check && Score_start && !pause_cnt)
        {
            gameUI.DispScore(1);
            tr.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.Self);
        }
        //죽는 이벤트
        if (isDeath && Deathcnt == 0)
        {
            rigdbody.AddForce(Vector3.back * moveJump / 50, ForceMode.Impulse);
            _animation.Play("death");
            source.Stop();
            source.PlayOneShot(death_sound, 0.7f);
        }
        if (isDeath) TickTime += Time.deltaTime;
        if (TickTime >= Slidetime && isDeath && Deathcnt == 0)
        {
            Deathcnt = 1;
            Slidetime = 0.7f;
            TickTime = 0;
            trap_camera.isDeath();
            gameUI.isDeath3();
            gamecamera.isDeath();
            Time.timeScale = 0;
        }

        //탈출 성공시
        if (isVictory)
        {
            TickTime = 0;
            trap_camera.isDeath();
            gameUI.isVictory3();
            gamecamera.isVictory();
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
            else if(!pause_cnt)
            {
                pause_cnt = true;
                gameUI.Pause_on();
                Time.timeScale = 0;
            }
        }
    }
    //물리적인 요소 처리
    void FixedUpdate()
    {
        if (!jumpcnt) return;
        if (jumpcnt)
        {
            isGround = false;
            rigdbody.AddForce(Vector3.up * moveJump, ForceMode.Impulse);
            _animation.CrossFade(anim.jump.name, 0.1f);
            source.volume = 0.5f;
            source.Stop();
            idle_cnt = 1;
            source.PlayOneShot(jump_sound, 0.5f);
        }
        jumpcnt = false;

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
        if (col.tag == "MiddleTrap" && slidecnt)
        { }
        else if (col.tag == "MiddleTrap" && !slidecnt)
        {
            col_check = false;
            isDeath = true;
            Debug.Log("death");
        }
        if (col.tag == "LowTrap")
        {
            col_check = false;
            isDeath = true;
            Debug.Log("death");
        }
        if (col.tag == "Trap")
        {
            col_check = false;
            isDeath = true;
            Debug.Log("death");
        }
        if (col.tag == "Water")
        {
            col_check = false;
            isDeath = true;
            Debug.Log("death");
        }
    }
    //탈출 이벤트
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Victory")
        {
            col_check = false;
            isVictory = true;
            Debug.Log("Victory");
        }
    }
}
