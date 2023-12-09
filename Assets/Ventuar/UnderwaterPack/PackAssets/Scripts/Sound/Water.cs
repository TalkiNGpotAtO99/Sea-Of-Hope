using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public static bool isWater = false;

    [SerializeField] private string sound_DiveIn;
    [SerializeField] private string sound_DiveOut;

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
        SoundManager.instance.PlaySE(sound_DiveIn);
        isWater = true;
    }

    private void GetOutWater(Collider _player)
    {
        if (isWater)
        {
            SoundManager.instance.PlaySE(sound_DiveOut);
            isWater = false;
        }
    }

}
