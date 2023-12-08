using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PanelControl : MonoBehaviour
{
    private GameObject inventoryPanel;
    public PlayerInventory playerInventory;
    private Dictionary<string, int> collectedFishCount = new Dictionary<string, int>();
    public Text fishInfoText; // Text 컴포넌트를 참조하기 위한 변수
    void Awake()
    {
        // infoPanel이 null이면 Find 메서드로 찾아서 할당
        if (inventoryPanel == null)
        {
            inventoryPanel = GameObject.Find("InventoryPanel");
        }
        fishInfoText = inventoryPanel.GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ToggleInventoryPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleInventoryPanel();
        }
    }
    void ToggleInventoryPanel()
    {
        // 패널이 활성화되어 있으면 비활성화하고, 비활성화되어 있으면 활성화
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateFishInfoText();
        }
    }
    void UpdateFishInfoText()
    {
        
        // fishInfoText가 null이면 리턴
        if (fishInfoText == null)
        {
            Debug.LogError("fishInfoText가 할당되지 않았습니다.");
            return;
        }

        playerInventory = FindObjectOfType<PlayerInventory>();

        // playerInventory 스크립트를 찾았는지 확인
        if (playerInventory != null)
        {
            //텍스트 초기화
            fishInfoText.text = "";

            // Boat에서 채집한 물고기 정보를 가져옴
            Dictionary<string, int> fishInfo = playerInventory.GetFishCount();

            foreach (KeyValuePair<string, int> fishEntry in fishInfo)
            {
                fishInfoText.text += "- "+fishEntry.Key + ": " + fishEntry.Value + "마리 \n";
            }
        }
        else
        {
            Debug.LogError("playerInventory 스크립트를 찾을 수 없습니다.");
        }
    }
}
