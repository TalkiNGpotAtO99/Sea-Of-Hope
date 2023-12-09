using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class O2 : MonoBehaviour
{
    
    [SerializeField] private float totalOxygen;
    public float currentOxygen;

    [SerializeField] private Image image_gauge;
    [SerializeField] private string sound_LackOfOxygen;
    [SerializeField] private float breatheTime;
    private float currentLackTime;

    private playerMovement player;
    private HP hp;
    

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<playerMovement>();
        hp = FindObjectOfType<HP>();

        currentOxygen = totalOxygen;
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseOxygen();
        GetOutWater();
        soundplay();
    }
    private void GetOutWater()
    {
        if(!player.isInWater) //플레이어가 물 밖에 있으면
        {
            currentOxygen = totalOxygen;
            image_gauge.fillAmount = currentOxygen; //산소게이지 풀로 채우기
        }
    }
    
    //물 안에서 산소 감소
    private void DecreaseOxygen()
    {
        //플레이어가 물 안에 있다면
        if(player.isInWater)
        {
            currentOxygen -= Time.deltaTime; //산소 감소
            image_gauge.fillAmount = currentOxygen / totalOxygen; //산소게이지 감소
        }
    }

    private void soundplay()
    {
        if(player.isInWater)
        {
            if (currentOxygen <= 0)
            {
                currentLackTime += Time.deltaTime;
                if(currentLackTime >= breatheTime)
                {
                    SoundManager.instance.PlaySE(sound_LackOfOxygen);
                    currentLackTime = 0;
                }
            }         
        }
    }
}
