using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestTextDisplay : MonoBehaviour
{
    public QuestControl questControl;
    private Text questText;
    private string[] questFishes = {
        "다금바리",
        "혹돔",
        "참돔",
        "농어",
        "병어",
        "메기",
        "고등어",
        "꽁치",
        "전갱이",
        "잉어"
    };
    void Start()
    {
        questText = GetComponent<Text>();
        if (questText == null)
        {
            Debug.LogError("QuestText를 찾을 수 없습니다. QuestText 스크립트를 올바르게 연결했는지 확인하세요.");
        }
        questControl = FindObjectOfType<QuestControl>();
        if (questControl == null)
        {
            Debug.LogError("QuestControl 스크립트를 찾을 수 없습니다.");
        }
    }
    void Update()
    {
        questText.text = questFishes[questControl.GetQFishIndex()]+" "+questControl.GetQFishNum()+"마리";
    }
}
