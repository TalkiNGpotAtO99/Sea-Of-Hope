using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBtnController : MonoBehaviour
{
    // 게임 종료 버튼 클릭 시 호출될 함수
    public void Exit()
    {
        Debug.Log("게임을 종료합니다."); // 디버그용 로그, 실제 빌드에서는 제거 가능
        Application.Quit(); // 애플리케이션을 종료합니다. (빌드된 런타임에서만 작동)
    }
}
