using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SingleFishAttack : LowPolyUnderwaterPack.SingleFishAI
{
    [SerializeField] protected string singlefishName; //Name of Singlefish
    [SerializeField] protected int hp; //hp of Singlefish

    [SerializeField] protected float attackSpeed; //attack speed of Singlefish
    [SerializeField] protected float rushSpeed;

    protected float currentTime; // Variable to be used in attack time limits
    protected Vector3 destination; //destination (associated with Rush)

    [SerializeField] protected float attackTime; // total attack time
    [SerializeField] protected float rushTime;
    protected float currentAttackTime; //calculate
    [SerializeField] protected float AttackDelayTime; //attack delay

    // State Varable
    protected bool isAttacking; //Determining whether Singlefish are attacking or not
    protected bool isRushing; //Determining whether Singlefish are rushing
    protected bool isDead; //Determining whether Singlefish are dead

    //Required Compnents
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody rigid;
    [SerializeField] protected BoxCollider boxCol;
    protected NavMeshAgent nav;
    protected SingleFishViewAngle theSingleFishViewAngle;

    // Start is called before the first frame update
    override protected void Start()
    {
        theSingleFishViewAngle = GetComponent<SingleFishViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        isAttacking = false;
    }

    // Update is called once per frame
     protected override void Update()
    {
        base.Update();
        if (theSingleFishViewAngle.View() && !isDead)
        {
            //StopAllCoroutines();
            //StartCoroutine(ChaseTargetCoroutine());
            ElapseTime();
        }

        //IEnumerator ChaseTargetCoroutine()
        //{
        //    currentAttackTime = 0;
        //    while (currentAttackTime < attackTime)
        //    {
        //        Attack(theSingleFishViewAngle.GetTargetPos());
        //        yield return new WaitForSeconds(AttackDelayTime);
        //        currentAttackTime += AttackDelayTime;
        //    }
        //    isAttacking = false;
        //    isRushing = false;
        //    anim.SetBool("Rushing", isRushing);
        //    nav.ResetPath();
        //}
    }
    
    public void Attack(Vector3 _targetPos)
    {
        if (isAttacking || isRushing)
        {  
            isAttacking = true;
            
            //rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            nav.SetDestination(transform.position + destination * 5f);
            isRushing = true;

            destination = _targetPos;
            anim.SetBool("Attacking", isAttacking);
            nav.speed = attackSpeed;

            
        }
    }

    public void Rush(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x + _targetPos.x, transform.position.y + _targetPos.y, transform.position.z + _targetPos.z).normalized;
        isAttacking = true;
        isRushing = true;
        nav.speed = rushSpeed;
        anim.SetBool("Rushing", isRushing);
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if(!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            anim.SetTrigger("Hurt");
            Rush(_targetPos);
        }
    }

    private void ElapseTime()
    {
        if (isAttacking)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isAttacking )
                ReSet();
        }
    }

    protected virtual void ReSet()
    {
        isAttacking = false; 
        isRushing = false;
        nav.speed = maxVelocity;
        nav.ResetPath();
        anim.SetBool("Attaking", isAttacking);
        anim.SetBool("Rushing", isRushing);
        //Hardcoded. Be able to modify this value from Inspector
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
    }

    private void Dead()
    {
        isAttacking = false;
        isRushing = false;
        isDead = true;
        swimType = SwimType.Dead;
        anim.SetTrigger("Dead");
    }
}