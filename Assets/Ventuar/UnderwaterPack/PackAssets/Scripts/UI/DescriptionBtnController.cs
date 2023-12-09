using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionBtnController : MonoBehaviour
{
    public DPanelController dPanelController;
    void Start(){
        dPanelController = FindObjectOfType<DPanelController>();
    }
    public void Clicked()
    {
        dPanelController.ToggleDescriptionPanel();
    }
}
