using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPanelController : MonoBehaviour
{
    
    private GameObject descriptionPanel;
    void Awake()
    {
        if (descriptionPanel == null)
        {
            descriptionPanel = GameObject.Find("DescriptionPanel");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        descriptionPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && descriptionPanel.activeSelf){
            descriptionPanel.SetActive(false);
        }
    }

    public void ToggleDescriptionPanel()
    {
        descriptionPanel.SetActive(true);
    }
}
