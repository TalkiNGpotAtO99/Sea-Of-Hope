using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestControl : MonoBehaviour
{
    // 다금바리, 혹돔, 참돔, 농어, 병어, 메기, 고등어, 꽁치, 전갱이, 잉어
    private int fishIndex;
    private int fishNum;
    void Start()
    {
        fishIndex = Random.Range(0, 10);
        fishNum = Random.Range(1, 3);
    }
    public int GetQFishIndex(){
        return fishIndex;
    }
    public int GetQFishNum(){
        return fishNum;
    }
}
