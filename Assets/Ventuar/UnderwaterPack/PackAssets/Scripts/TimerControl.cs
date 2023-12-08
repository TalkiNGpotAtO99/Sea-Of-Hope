using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimerControl : MonoBehaviour
{
    
    public float timerDuration = 300f; // 5분 (단위: 초)
    private Text timerText;
    private float currentTime;

    void Start()
    {
        currentTime = timerDuration;
        timerText = GetComponent<Text>();
        if (timerText == null)
        {
            Debug.LogError("TimerText를 찾을 수 없습니다. Timer 스크립트를 올바르게 연결했는지 확인하세요.");
        }
    }

    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            // 타이머를 분:초 형식의 문자열로 변환하여 UI에 표시
            string minutes = Mathf.Floor(currentTime / 60).ToString("00");
            string seconds = (currentTime % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            // 타이머가 0에 도달하면 원하는 동작 수행
            timerText.text = "00:00";
            Debug.Log("타이머 종료!");
        }
    }
}
