using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInteraction : MonoBehaviour
{
    // 플레이어 인벤토리 스크립트에 접근하기 위한 참조
    public PlayerInventory playerInventory;
    private Dictionary<string, int> boatFishCount = new Dictionary<string, int>();
    // 다금바리, 혹돔, 참돔, 농어, 병어, 메기, 고등어, 꽁치, 전갱이, 잉어, 미역
    private string[] fishTypes = { "Atlantic Cod", "Flowerhorn", "Red Snapper", "Salmon", "Piranha", "Catfish", "Sea Bass", "Herring", "Common Carp", "Koi", "Kelp01" };
    private Dictionary<string, int> fishPrices = new Dictionary<string, int>()
    {
        {"Atlantic Cod", 100000},
        {"Flowerhorn", 70000},
        {"Red Snapper", 50000},
        {"Salmon", 30000},
        {"Piranha", 20000},
        {"Catfish", 10000},
        {"Sea Bass", 5000},
        {"Herring", 3000},
        {"Common Carp", 2000},
        {"Koi", 1000},
        {"Kelp01", 500},
        // 물고기 종류와 가격을 필요에 따라 추가
    };

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
    }
    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 Boat과 접촉한 경우
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어가 Boat에 접촉했습니다. 스페이스바를 눌러 Fish를 옮깁니다.");
        }
    }

    void OnTriggerStay(Collider other)
    {
        // 플레이어가 Boat과 접촉 중이고 스페이스바를 누른 경우
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            TransferFishToBoat();
        }
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.X))
        {
            SellAllFish();
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
    }
    // Boat의 모든 Fish를 판매하여 총액을 반환하는 함수
    public int SellAllFish()
    {
        int totalAmount = 0;

        foreach (KeyValuePair<string, int> fishEntry in boatFishCount)
        {
            string fishType = fishEntry.Key;
            int fishCount = fishEntry.Value;

            // 물고기 가격이 설정되어 있는지 확인하고 가격을 가져옴
            if (fishPrices.ContainsKey(fishType))
            {
                int fishPrice = fishPrices[fishType];
                int fishTotalAmount = fishCount * fishPrice;

                // 물고기를 판매하여 총액에 더함
                totalAmount += fishTotalAmount;

                Debug.Log(fishCount + "개의 " + fishType + "를 " + fishTotalAmount + "원에 판매했습니다.");
            }
            else
            {
                Debug.LogWarning("물고기 가격이 설정되지 않았습니다: " + fishType);
            }
        }
        InitializeBoat();
        Debug.Log("배를 모두 비웠습니다. 총 금액: "+totalAmount+"원");

        // 총액 반환
        return totalAmount;
    }
}
