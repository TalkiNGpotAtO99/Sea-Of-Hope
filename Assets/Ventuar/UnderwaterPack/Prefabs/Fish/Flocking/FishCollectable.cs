using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isCollected = false;

    void Update()
    {
        // 플레이어와 물고기가 접촉하고 스페이스바를 누르면 채집
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space) && !isCollected)
        {
            HideFish();
            Debug.Log("Collected");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("isNear");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void HideFish()
    {
        // 물고기 비활성화 (숨김 처리)
        gameObject.SetActive(false);

        isCollected = true;
    }
}
