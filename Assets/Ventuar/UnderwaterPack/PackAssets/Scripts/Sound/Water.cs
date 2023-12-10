using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool isWater = false;

    // 필요한 오디오
    [SerializeField] private string sound_DiveIn;
    [SerializeField] private string sound_DiveOut;
    [SerializeField] private string sound_UnderWater;
    [SerializeField] private string sound_OnWater;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            GetWater(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            GetOutWater(other);
        }
    }

    private void GetWater(Collider _player)
    {
        SoundManager.instance.StopSE(sound_UnderWater);
        SoundManager.instance.PlaySE(sound_DiveOut);
        SoundManager.instance.PlaySE(sound_OnWater);
        isWater = true;
    }

    private void GetOutWater(Collider _player)
    {
        if (isWater)
        {
            SoundManager.instance.PlaySE(sound_DiveIn);
            SoundManager.instance.StopSE(sound_OnWater);
            SoundManager.instance.PlaySE(sound_UnderWater);

            isWater = false;
        }
    }

}
