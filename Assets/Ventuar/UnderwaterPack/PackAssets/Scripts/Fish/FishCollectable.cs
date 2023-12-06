using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollectable : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private bool isCollected = false;
    private PlayerInventory playerInventory;

    void Start(){
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory 스크립트를 찾을 수 없습니다.");
        }
    }
    void Update()
    {
        // 플레이어와 물고기가 접촉하고 스페이스바를 누르면 채집
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space) && !isCollected)
        {
            HideFish();
            playerInventory.CollectFish(gameObject.name); // 에셋의 이름으로 채집
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
