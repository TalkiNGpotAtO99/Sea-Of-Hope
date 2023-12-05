using System.Security.Cryptography;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
   public float acceleration = 2f;   // 가속도
    public float deceleration = 10f;  // 감속도
    public float maxSpeed = 10f;      // 최대 속도
    public float rotateSpeed = 100f;

    private float currentSpeed = 0f;

    private Rigidbody rb;

    Animator animator;
    
    public Collider waterCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        // 중력 비활성화
        rb.useGravity = false;

        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        FreezeMoveAndRotate();
    }
    void FreezeMoveAndRotate(){
        rb.angularVelocity=Vector3.zero;
        rb.velocity =Vector3.zero;
    }

    void Update()
    {
        MoveAndRotate();
        CheckInsideWater();
    }
    

    void MoveAndRotate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetKey(KeyCode.W) ? 1f : Input.GetKey(KeyCode.S) ? -1f : 0f;
        float upDownRotation = Input.GetKey(KeyCode.UpArrow) ? 1f : Input.GetKey(KeyCode.DownArrow) ? -1f : 0f;
        // 전진 및 후진 가속도 및 감속도 적용
        if (vertical != 0)
        {
            // 가속
            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed * vertical, acceleration * Time.deltaTime);
        }
        else
        {
            // 감속
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.deltaTime);
        }

        // 전진 및 후진
        Vector3 moveDirection = transform.forward * currentSpeed;
        transform.Translate(moveDirection * Time.deltaTime, Space.World);

        // 위아래 회전
        Vector3 rotateUpDownDirection = new Vector3(-upDownRotation, 0, 0); // 방향 반전
        transform.Rotate(rotateUpDownDirection * rotateSpeed * Time.deltaTime);


        // 좌우 회전
        Vector3 rotateDirection = new Vector3(0, horizontal, 0);
        transform.Rotate(rotateDirection * rotateSpeed * Time.deltaTime);

        // 공격 및 채집 모션
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
        
    }
    void CheckInsideWater(){
        if (transform.position.y > waterCollider.bounds.min.y+3f)
        {
            // 플레이어가 물 밖으로 나가려고 할 때
            Vector3 newPosition = transform.position;
            newPosition.y = waterCollider.bounds.min.y+3f;
            transform.position = newPosition;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 충돌 시 가속도를 -1으로 만듦
        currentSpeed = -1f;
    }

}
