using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBtnController : MonoBehaviour
{
    public void SwitchScene()
    {
        // 버튼이 클릭되었을 때 호출되는 함수
        SceneManager.LoadScene("MainScene");
    }
}
