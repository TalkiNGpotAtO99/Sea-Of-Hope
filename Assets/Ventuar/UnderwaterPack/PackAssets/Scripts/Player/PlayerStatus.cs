using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //필요한 컴포넌트
    private O2 o2;
    private HP hp;
    //물고기가 주는 데미지를 가져올 컴포넌트 (가칭)
    private SingleFishAttack singlefishattack;

    // Start is called before the first frame update
    void Start()
    {
        o2 = FindObjectOfType<O2>();
        hp = FindObjectOfType<HP>();
    }

    // Update is called once per frame
    void Update()
    {
        // 데미지를 입으면
        //if (isHurt)
        //{
        //    Damage();
        //}
    }
    private void Damage()
    {
        // 데미지 입으면 hp감소
        //hp.DecreaseHP(singlefishattack.attackDamage);

    }
}
