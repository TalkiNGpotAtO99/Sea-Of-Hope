using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDisplay : MonoBehaviour
{
    public QuestControl questControl;
    public RawImage imagePanel; // UI 패널에 해당하는 Image 컴포넌트를 가진 GameObject를 연결하세요.
    public Texture2D[] imageArray; // 랜덤하게 선택할 이미지를 배열에 저장하세요.

    void Start()
    {
        questControl = FindObjectOfType<QuestControl>();
        if (questControl == null)
        {
            Debug.LogError("QuestControl 스크립트를 찾을 수 없습니다.");
        }

        imageArray = new Texture2D[]
        {
            Resources.Load<Texture2D>("Images/Atlantic_Cod"),
            Resources.Load<Texture2D>("Images/Flowerhorn"),
            Resources.Load<Texture2D>("Images/Red_Snapper"),
            Resources.Load<Texture2D>("Images/Salmon"),
            Resources.Load<Texture2D>("Images/Piranha"),
            Resources.Load<Texture2D>("Images/Catfish"),
            Resources.Load<Texture2D>("Images/Sea_Bass"),
            Resources.Load<Texture2D>("Images/Herring"),
            Resources.Load<Texture2D>("Images/Common_Carp"),
            Resources.Load<Texture2D>("Images/Koi"),
        };
        imagePanel = GetComponent<RawImage>();
        DisplayRandomImage();
    }

    void DisplayRandomImage()
    {
        
        if (imageArray.Length > 0 && imagePanel != null)
        {
            imagePanel.texture = imageArray[questControl.GetQFishIndex()];
        }
        else
        {
            Debug.LogError("이미지 배열 또는 UI 패널이 올바르게 설정되지 않았습니다.");
        }
    }
}
