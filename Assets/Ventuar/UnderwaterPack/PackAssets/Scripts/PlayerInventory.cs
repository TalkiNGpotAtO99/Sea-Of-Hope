using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, int> collectedFishCount = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
        InitializeInventory();
    }
    private void InitializeInventory()
    {
        // 여러 물고기 종류에 대한 초기값 설정
        // 다금바리, 혹돔, 참돔, 농어, 병어, 메기, 고등어, 꽁치, 전갱이, 잉어, 미역
        string[] fishTypes = { "Atlantic Cod", "Flowerhorn", "Red Snapper", "Salmon", "Piranha", "Catfish", "Sea Bass", "Herring", "Common Carp", "Koi", "Kelp01" };

        foreach (string fishType in fishTypes)
        {
            collectedFishCount[fishType] = 0;
        }
    }

    public void CollectFish(string fishType)
    {
        if (collectedFishCount.ContainsKey(fishType))
        {
            // 해당 물고기 종류의 채집 수 증가
            collectedFishCount[fishType]++;
            Debug.Log($"채집된 {fishType} 수: {collectedFishCount[fishType]}");
        }
        else
        {
            Debug.LogWarning($"Unknown fish type: {fishType}");
        }
    }
}
