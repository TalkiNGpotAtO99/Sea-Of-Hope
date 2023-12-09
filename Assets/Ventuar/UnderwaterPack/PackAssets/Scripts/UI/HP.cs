using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    //hp
    [SerializeField] private int hp;
    [SerializeField] private int currentHp;
    [SerializeField] private int HurtbyO2; //산소고갈로 hp가 닳는 정도

    //hp 재회복 딜레이
    private int hpRechargeTime;
    private int currentHpRechargeTime;

    //hp가 가득 찼는지 아닌지 (감소 여부)
    private bool isHpUsed;


    //필요한 이미지
    [SerializeField]
    private Image[] images_hp; //왼쪽 + 오른쪽 hp
    private const int HP_left = 0, HP_right = 1;

    private FishDamage thePlayerStatus;
    private playerMovement player;
    private O2 o2;
    private float temp;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = hp;
        thePlayerStatus = FindObjectOfType<FishDamage>();
        player = FindObjectOfType<playerMovement>();
        o2 = FindObjectOfType<O2>();

    }

    // Update is called once per frame
    void Update()
    {

        //산소가 고갈되면 1초당 2씩 hp 감소
        if (o2.currentOxygen <= 0 && player.isInWater)
        {
            temp += Time.deltaTime;
            if (temp >= 1)
            {
                DecreaseHP(HurtbyO2);
                temp = 0;
            }
        }
        //게이지 나타내기
        GaugeUpdate();
    }

    public void GaugeUpdate()
    {
        images_hp[HP_left].fillAmount = (float)currentHp / hp;
        images_hp[HP_right].fillAmount = (float)currentHp / hp;
    }

    public void DecreaseHP(int _count)
    {
        currentHp -= _count;
        if (currentHp <= 0)
            Debug.Log("Game Over 게임 오버");

    }
}
