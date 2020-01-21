using UnityEngine;
using System.Collections;

public class UIMgr_stage : MonoBehaviour {

    public void OnClickBackBtn()
    {
        Application.LoadLevel("Main");
    }
    public void OnClickStage1()
    {
        Application.LoadLevel("RunGame1");
    }
    public void OnClickStage2()
    {
        Application.LoadLevel("RunGame2");
    }
    public void OnClickStage3()
    {
        Application.LoadLevel("RunGame3");
    }
}
