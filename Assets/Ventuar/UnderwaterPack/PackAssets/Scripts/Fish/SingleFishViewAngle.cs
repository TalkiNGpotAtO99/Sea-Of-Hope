using LowPolyUnderwaterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleFishViewAngle : MonoBehaviour
{
    [SerializeField] private float viewAngle; //viewAngle of singlefish
    [SerializeField] private float viewDistance; //distance of view
    [SerializeField] private LayerMask targetMask; // target mask (=player)

    //Get Player Objects
   // private PlayerController thePlayer;

    private void Start()
    {
        //Get Player Objects and execute this
        //thePlayer = FindObjectOfType<PlayerController>();
    }

    //public Vector3 GetTargetPos()
    //{
    //    return thePlayer.transform.position;
    //}

    private Vector3 BoundaryAngle(float _angle)
    {
        _angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(_angle * Mathf.Deg2Rad), 0f, Mathf.Cos(_angle * Mathf.Deg2Rad));
    }

    public bool View()
    {
        Vector3 _leftBoundary = BoundaryAngle(-viewAngle * 0.5f);
        Vector3 _rightBoundary = BoundaryAngle(viewAngle * 0.5f);

        Collider[] _target = Physics.OverlapSphere(transform.position, viewDistance, targetMask);

        for (int i = 0; i < _target.Length; i++)
        {
            Transform _targetTf = _target[i].transform;
            if(_targetTf.name == "Player")
            {

                Vector3 _direction = (_targetTf.position - transform.position).normalized;
                float _angle = Vector3.Angle(_direction, transform.forward);
           
                if(_angle < viewAngle * 0.5f)
                {
                    RaycastHit _hit;
                    if (Physics.Raycast(transform.position + transform.up, _direction, out _hit, viewDistance))
                    {
                        if(_hit.transform.name == "Player")
                        {
                            Debug.Log("플레이어가 싱글피쉬 시야 내에 있습니다.");
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
