using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInteraction : MonoBehaviour
{
    // 플레이어 인벤토리 스크립트에 접근하기 위한 참조
    public PlayerInventory playerInventory;
    public Dictionary<string, int> boatFishCount = new Dictionary<string, int>();
    // 다금바리, 혹돔, 참돔, 농어, 병어, 메기, 고등어, 꽁치, 전갱이, 잉어, 미역
    private string[] fishTypes = { "Atlantic Cod", "Flowerhorn", "Red Snapper", "Salmon", "Piranha", "Catfish", "Sea Bass", "Herring", "Common Carp", "Koi", "Kelp01" };
    private bool isNear = false;
    public Dictionary<string, int> GetBoatInventory(){
        return boatFishCount;
    }

    void Start()
    {   
        playerInventory = FindObjectOfType<PlayerInventory>();
        if (playerInventory == null)
        {
            Debug.LogError("PlayerInventory 스크립트를 찾을 수 없습니다.");
        }
        InitializeBoat();
    }
    void InitializeBoat(){
        foreach (string fishType in fishTypes)
        {
            boatFishCount[fishType] = 0;
        }
        SaveData(boatFishCount);
    }
    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 Boat과 접촉한 경우
        if (other.CompareTag("Player"))
        {
            isNear = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 플레이어가 Boat과 접촉 중이고 스페이스바를 누른 경우
        if (isNear  && Input.GetKeyDown(KeyCode.Space))
        {
            TransferFishToBoat();
        }
    }
    void OnTriggerExit(Collider other){
        if (other.CompareTag("Player"))
        {
            isNear = false;
        }
    }

    // Fish를 Boat로 옮기는 함수
    public void TransferFishToBoat()
    {
        // 플레이어 인벤토리에서 Fish를 Boat으로 이동
        foreach (KeyValuePair<string, int> fishEntry in playerInventory.GetFishCount())
        {
            string fishType = fishEntry.Key;
            int fishCount = fishEntry.Value;

            // boatFishCount 딕셔너리에 Fish 추가 또는 갱신
            if (boatFishCount.ContainsKey(fishType))
            {
                boatFishCount[fishType] += fishCount;
                Debug.Log(fishCount + "개의 " + fishType + "를 Boat으로 옮겼습니다.");
            }
            else
            {
                Debug.LogWarning($"Unknown fish type: {fishType}");
            }

        }
        playerInventory.InitializeInventory();
        Debug.Log("어망을 모두 비웠습니다.");
        SaveData(boatFishCount);
    }
    // Boat의 모든 Fish를 판매하여 총액을 반환하는 함수
    public void SaveData(Dictionary<string,int> data){
        foreach (KeyValuePair<string, int> fishEntry in data)
        {
            PlayerPrefs.DeleteKey(fishEntry.Key);
            PlayerPrefs.SetInt(fishEntry.Key, fishEntry.Value);
        }
    }
    
}
