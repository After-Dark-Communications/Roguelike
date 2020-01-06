using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SmoothCamera : MonoBehaviour
{
    [SerializeField] private Transform _ObjectToFollow;
    [SerializeField] private float _CamSpeed;
    //private Vector2 _LastPos;
    private Vector3 _Camdepth = new Vector3(0, 0, -10);
    //private void Start()
    //{
    //    //_LastPos = gameObject.transform.position;
    //}
    private void FixedUpdate()
    {
        gameObject.transform.DOMove(_ObjectToFollow.transform.position + _Camdepth, _CamSpeed);

        
    }
}