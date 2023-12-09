using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RestartBtnController : MonoBehaviour
{
    // 다시하기 버튼 클릭 시 호출될 함수
    public void Restart()
    {
        SceneManager.LoadScene("StartScene");
    }
}
