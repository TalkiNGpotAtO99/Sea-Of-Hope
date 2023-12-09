using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RPanelController : MonoBehaviour
{
    private GameObject resultPanel;
    public Text resultText;
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
    private Dictionary<string, string> fishToKOR = new Dictionary<string, string>()
    {
        {"Atlantic Cod", "다금바리"},
        {"Flowerhorn", "혹돔"},
        {"Red Snapper", "참돔"},
        {"Salmon", "농어"},
        {"Piranha", "병어"},
        {"Catfish", "메기"},
        {"Sea Bass", "고등어"},
        {"Herring", "꽁치"},
        {"Common Carp", "전갱이"},
        {"Koi", "잉어"},
        {"Kelp01", "미역"},
        // 물고기 종류와 가격을 필요에 따라 추가
    };
    void Awake()
    {
        if (resultPanel == null)
        {
            resultPanel = GameObject.Find("ResultPanel");
        }
        resultText = resultPanel.GetComponentInChildren<Text>();
    }


    // Update is called once per frame
    void Start(){
        UpdateResultText();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && resultPanel.activeSelf){
            ToggleResultPanel();
        }
    }
    void ToggleResultPanel()
    {
        // 패널이 활성화되어 있으면 비활성화하고, 비활성화되어 있으면 활성화
        resultPanel.SetActive(!resultPanel.activeSelf);
    }
    void UpdateResultText()
    {
        
        if (resultText == null)
        {
            Debug.LogError("resultText 할당되지 않았습니다.");
            return;
        }
        if(PlayerPrefs.GetInt("HP")<=0){
            DeadResult();
        }
        else{
            AliveResult();
        }
        
        
    }
    public void AliveResult(){
        //텍스트 초기화
        resultText.text = "[채집결과]\n";

        Dictionary<string, int> fishInfo = LoadData();

        foreach (KeyValuePair<string, int> fishEntry in fishInfo)
        {
            resultText.text += "- "+fishToKOR[fishEntry.Key] + ": " + fishEntry.Value + "마리 \n";
        }
        resultText.text += "+ "+SellAllFish(fishInfo)+"원\n";
        resultText.text += "\n";
        resultText.text += "[퀘스트]\n";
        resultText.text += "- "+fishToKOR[fishTypes[PlayerPrefs.GetInt("fishIndex")]] + ": " + fishInfo[fishTypes[PlayerPrefs.GetInt("fishIndex")]] + "/"+PlayerPrefs.GetInt("fishNum")+"마리 \n";
        int bonus = 0;
        if(fishInfo[fishTypes[PlayerPrefs.GetInt("fishIndex")]]>=PlayerPrefs.GetInt("fishNum")){
            bonus = 500000;
        }else{
            bonus = 0;
        }
        resultText.text += "+ "+bonus+"원\n";
        resultText.text += "\n";
        resultText.text += "[총 수익]\n";
        resultText.text += "+ "+(bonus+SellAllFish(fishInfo))+"원\n";
        resultText.text += "\n";
        resultText.text += "esc 키를 눌러 종료 ..."; 
    }
    public void DeadResult(){
        //텍스트 초기화
        resultText.text = "[게임오버]\n";
        resultText.text += "플레이어가 기절하였습니다! \n";
        resultText.text += "\n";
        resultText.text += "esc 키를 눌러 종료 ..."; 
    }
    public Dictionary<string, int> LoadData()
    {
        Dictionary<string, int> temp = new Dictionary<string, int>();
        foreach(string name in fishTypes)
        {
            temp[name] = PlayerPrefs.GetInt(name);
        }
        return temp;
    }
    public int SellAllFish(Dictionary<string, int> fishes)
    {
        int totalAmount = 0;

        foreach (KeyValuePair<string, int> fishEntry in fishes)
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
        Debug.Log("배를 모두 비웠습니다. 총 금액: "+totalAmount+"원");

        // 총액 반환
        return totalAmount;
    }
}
