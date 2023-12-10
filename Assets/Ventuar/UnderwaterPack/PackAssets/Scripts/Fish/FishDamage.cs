using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishDamage : MonoBehaviour
{
    [SerializeField] public Collider fishCollider;
    [SerializeField] public int fishDamage;
    [SerializeField] private float damageTime; //데미지가 들어갈 딜레이
    private float currentDamageTime;

    [SerializeField] private float durationTime; //데미지 입는 지속시간
    private float currentDurationTime;
    
    //필요한 오디오
    [SerializeField] private string sound_Ouch;


    private const int Orca = 0, GreatWhiteShark = 1, HammarheadShark = 2, SwordFish = 3;
    


    //상태변수
    private bool isHurt = true;

    //필요한 컴포넌트
    private O2 o2;
    private HP hp;

    // Start is called before the first frame update
    void Start()
    {
        o2 = FindObjectOfType<O2>();
        hp = FindObjectOfType<HP>();

        currentDamageTime = durationTime;
    }

    // Update is called once per frame
    void Update()
    {
        // 데미지를 입으면
        if (isHurt)
        {
            ElapseTime();
            OnTriggerEnter(fishCollider);
            isHurt = false;
        }
    }

    private void ElapseTime()
    {
        currentDurationTime -= Time.deltaTime;

        if(currentDamageTime > 0)
        {
            currentDamageTime -= Time.deltaTime;
        }
        if(currentDurationTime <= 0)
        {
            Off();
        }
    }

    private void Off()
    {
        isHurt = false;
    }

    void OnTriggerEnter(Collider singlefish)
    {
        // Check if the object the player touched is tagged as "singleFish"
        if (isHurt && (singlefish.transform.tag == "Player"))
        {
            if (currentDamageTime <= 0)
            {
                Damage();
                currentDamageTime = damageTime;
            }
        }
    }

    private void Damage()
    {
        // 데미지 입으면 hp감소
        hp.DecreaseHP(fishDamage);
        SoundManager.instance.PlaySE(sound_Ouch);
    }
}
