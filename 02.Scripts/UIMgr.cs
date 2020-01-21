using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour {

    public void OnClickStartBtn()
    {
        Debug.Log("Click Button");
        Application.LoadLevel("Main_Stage");
    }
    public void OnClickRuleBtn()
    {
        Debug.Log("Rule");
        Application.LoadLevel("Main_Rules");
    }
    public void OnClickExitBtn()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}
