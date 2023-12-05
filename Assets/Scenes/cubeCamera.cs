using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeCamera : MonoBehaviour
{
    public Transform target;  // 따라갈 대상인 큐브의 Transform
    public float distance = 5f;  // 카메라와 대상 사이의 거리
    public float height = 2f;    // 카메라의 높이 조절
    public float rotationSpeed = 2f;  // 카메라 회전 속도

    void Update()
    {
        // 카메라의 회전 각도 계산
        float desiredRotationAngle = target.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredRotationAngle, 0);
        
        // 카메라의 위치 계산
        Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);
        desiredPosition.y = target.position.y + height;

        // 부드러운 이동 및 회전 적용
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * rotationSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
    }
}
