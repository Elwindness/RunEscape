using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {
    public Text txtScore; //Text UI 항목 연결
    private int totScore = 0; //누적점수
    private int totalScore = 0; //스테이지 실제 출력 점수
    private int HighScore1 = 0; //1스테이지 최고 기록 점수
    private int HighScore1_2 = 0;
    private int HighScore1_3 = 0;
    private int HighScore2 = 0; //2스테이지 최고 기록 점수
    private int HighScore2_2 = 0;
    private int HighScore2_3 = 0;
    private int HighScore3 = 0; //3스테이지 최고 기록 점수
    private int HighScore3_2 = 0;
    private int HighScore3_3 = 0;
    public GameObject Mainmenu;
    public GameObject Retry;
    public RawImage Gameover_image;
    public RawImage Victory_image;
    public RawImage Pause_image;
    public Text pause_txt;
    public Text gameover_txt;
    public Text victory_txt;
    public Text yourscore;//최종 출력 점수
    public Text highscore;//최고 점수
    public Text highscore2;//2등 점수
    public Text highscore3;//3등 점수

    // Use this for initialization
    void Start ()
    {
        Mainmenu.SetActive(false);
        Retry.SetActive(false);
        Gameover_image.enabled = false;
        Victory_image.enabled = false;
        gameover_txt.enabled = false;
        victory_txt.enabled = false;
        yourscore.enabled = false;
        highscore.enabled = false;
        highscore2.enabled = false;
        highscore3.enabled = false;
        Pause_image.enabled = false;
        pause_txt.enabled = false;
        DispScore(0);
        HighScore1 = PlayerPrefs.GetInt("HighScore1");
        HighScore1_2 = PlayerPrefs.GetInt("HighScore1_2");
        HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
        Debug.Log(HighScore1+" "+HighScore1_2+" "+HighScore1_3);
        HighScore2 = PlayerPrefs.GetInt("HighScore2");
        HighScore2_2 = PlayerPrefs.GetInt("HighScore2_2");
        HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
        Debug.Log(HighScore2 + " " + HighScore2_2 + " " + HighScore2_3);
        HighScore3 = PlayerPrefs.GetInt("HighScore3");
        HighScore3_2 = PlayerPrefs.GetInt("HighScore3_2");
        HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
        Debug.Log(HighScore3 + " " + HighScore3_2 + " " + HighScore3_3);
    }
	
    //점수 누적 함수
    public void DispScore(int score)
    {
        totScore += score;
        totalScore = totScore / 10;
        txtScore.text = "SCORE <color=#ff0000>" + totalScore.ToString() + "</color>";
    }
    public int GetScore()
    {
        return totalScore;
    }
    public void Pause_on()
    {
        Pause_image.enabled = true;
        pause_txt.enabled = true;
        Retry.SetActive(true);
        Mainmenu.SetActive(true);
    }
    public void Pause_off()
    {
        Pause_image.enabled = false;
        pause_txt.enabled = false;
        Retry.SetActive(false);
        Mainmenu.SetActive(false);
    }
    //죽을때 점수 출력 스테이지1
    public void isDeath()
    {
        Debug.Log("highscore = " + HighScore1);
        if (totalScore >= HighScore1)
        {
            PlayerPrefs.SetInt("HighScore1_3", HighScore1_2);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
            PlayerPrefs.SetInt("HighScore1_2", HighScore1);
            HighScore1_2 = PlayerPrefs.GetInt("HighScore1_2");
            PlayerPrefs.SetInt("HighScore1", totalScore);
            HighScore1 = PlayerPrefs.GetInt("HighScore1");
        }
        else if (totalScore >= HighScore1_2)
        {
            PlayerPrefs.SetInt("HighScore1_3", HighScore1_2);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
            PlayerPrefs.SetInt("HighScore1_2", totalScore);
            HighScore1_2 = PlayerPrefs.GetInt("HighScore1_2");
        }
        else if (totalScore >= HighScore1_3)
        {
            PlayerPrefs.SetInt("HighScore1_3", totalScore);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Gameover_image.enabled = true;
        gameover_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore1.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore1_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore1_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //죽을때 점수 출력 스테이지2
    public void isDeath2()
    {
        Debug.Log("highscore = " + HighScore2);
        if (totalScore >= HighScore2)
        {
            PlayerPrefs.SetInt("HighScore2_3", HighScore2_2);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
            PlayerPrefs.SetInt("HighScore2_2", HighScore2);
            HighScore2_2 = PlayerPrefs.GetInt("HighScore2_2");
            PlayerPrefs.SetInt("HighScore2", totalScore);
            HighScore2 = PlayerPrefs.GetInt("HighScore2");
        }
        else if (totalScore >= HighScore2_2)
        {
            PlayerPrefs.SetInt("HighScore2_3", HighScore2_2);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
            PlayerPrefs.SetInt("HighScore2_2", totalScore);
            HighScore2_2 = PlayerPrefs.GetInt("HighScore2_2");
        }
        else if (totalScore >= HighScore2_3)
        {
            PlayerPrefs.SetInt("HighScore2_3", totalScore);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Gameover_image.enabled = true;
        gameover_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore2.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore2_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore2_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //죽을때 점수 출력 스테이지3
    public void isDeath3()
    {
        Debug.Log("highscore = " + HighScore3);
        if (totalScore >= HighScore3)
        {
            PlayerPrefs.SetInt("HighScore3_3", HighScore3_2);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
            PlayerPrefs.SetInt("HighScore3_2", HighScore3);
            HighScore3_2 = PlayerPrefs.GetInt("HighScore3_2");
            PlayerPrefs.SetInt("HighScore3", totalScore);
            HighScore3 = PlayerPrefs.GetInt("HighScore3");
        }
        else if (totalScore >= HighScore3_2)
        {
            PlayerPrefs.SetInt("HighScore3_3", HighScore3_2);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
            PlayerPrefs.SetInt("HighScore3_2", totalScore);
            HighScore3_2 = PlayerPrefs.GetInt("HighScore3_2");
        }
        else if (totalScore >= HighScore3_3)
        {
            PlayerPrefs.SetInt("HighScore3_3", totalScore);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Gameover_image.enabled = true;
        gameover_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore3.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore3_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore3_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //탈출할시 점수 출력 스테이지1
    public void isVictory()
    {
        Debug.Log("highscore = "+ HighScore1);
        if (totalScore >= HighScore1)
        {
            PlayerPrefs.SetInt("HighScore1_3", HighScore1_2);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
            PlayerPrefs.SetInt("HighScore1_2", HighScore1);
            HighScore1_2 = PlayerPrefs.GetInt("HighScore1_2");
            PlayerPrefs.SetInt("HighScore1", totalScore);
            HighScore1 = PlayerPrefs.GetInt("HighScore1");
        }
        else if (totalScore >= HighScore1_2)
        {
            PlayerPrefs.SetInt("HighScore1_3", HighScore1_2);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
            PlayerPrefs.SetInt("HighScore1_2", totalScore);
            HighScore1_2 = PlayerPrefs.GetInt("HighScore1_2");
        }
        else if (totalScore >= HighScore1_3)
        {
            PlayerPrefs.SetInt("HighScore1_3", totalScore);
            HighScore1_3 = PlayerPrefs.GetInt("HighScore1_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Victory_image.enabled = true;
        victory_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>" + "!";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore1.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore1_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore1_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //탈출할시 점수 출력 스테이지2
    public void isVictory2()
    {
        Debug.Log("highscore = " + HighScore2);
        if (totalScore >= HighScore2)
        {
            PlayerPrefs.SetInt("HighScore2_3", HighScore2_2);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
            PlayerPrefs.SetInt("HighScore2_2", HighScore2);
            HighScore2_2 = PlayerPrefs.GetInt("HighScore2_2");
            PlayerPrefs.SetInt("HighScore2", totalScore);
            HighScore2 = PlayerPrefs.GetInt("HighScore2");
        }
        else if (totalScore >= HighScore2_2)
        {
            PlayerPrefs.SetInt("HighScore2_3", HighScore2_2);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
            PlayerPrefs.SetInt("HighScore2_2", totalScore);
            HighScore2_2 = PlayerPrefs.GetInt("HighScore2_2");
        }
        else if (totalScore >= HighScore2_3)
        {
            PlayerPrefs.SetInt("HighScore2_3", totalScore);
            HighScore2_3 = PlayerPrefs.GetInt("HighScore2_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Victory_image.enabled = true;
        victory_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>" + "!";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore2.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore2_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore2_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //탈출할시 점수 출력 스테이지3
    public void isVictory3()
    {
        Debug.Log("highscore = " + HighScore3);
        if (totalScore >= HighScore3)
        {
            PlayerPrefs.SetInt("HighScore3_3", HighScore3_2);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
            PlayerPrefs.SetInt("HighScore3_2", HighScore3);
            HighScore3_2 = PlayerPrefs.GetInt("HighScore3_2");
            PlayerPrefs.SetInt("HighScore3", totalScore);
            HighScore3 = PlayerPrefs.GetInt("HighScore3");
        }
        else if (totalScore >= HighScore3_2)
        {
            PlayerPrefs.SetInt("HighScore3_3", HighScore3_2);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
            PlayerPrefs.SetInt("HighScore3_2", totalScore);
            HighScore3_2 = PlayerPrefs.GetInt("HighScore3_2");
        }
        else if (totalScore >= HighScore3_3)
        {
            PlayerPrefs.SetInt("HighScore3_3", totalScore);
            HighScore3_3 = PlayerPrefs.GetInt("HighScore3_3");
        }
        Mainmenu.SetActive(true);
        Retry.SetActive(true);
        Victory_image.enabled = true;
        victory_txt.enabled = true;
        yourscore.text = "Your Score = <color=#ffffff>" + totalScore.ToString() + "</color>" + "!";
        yourscore.enabled = true;
        highscore.text = "1th Score = <color=#ffffff>" + HighScore3.ToString() + "</color>";
        highscore.enabled = true;
        highscore2.text = "2nd Score = <color=#ffffff>" + HighScore3_2.ToString() + "</color>";
        highscore2.enabled = true;
        highscore3.text = "3rd Score = <color=#ffffff>" + HighScore3_3.ToString() + "</color>";
        highscore3.enabled = true;
    }
    //버튼 이벤트
    public void OnClickMainmenu()
    {
        Application.LoadLevel("Main");
    }
    public void OnClickRetry()
    {
        Application.LoadLevel("RunGame1");
    }
    public void OnClickRetry2()
    {
        Application.LoadLevel("RunGame2");
    }
    public void OnClickRetry3()
    {
        Application.LoadLevel("RunGame3");
    }
}
