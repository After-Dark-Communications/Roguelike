using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEMO : MonoBehaviour
{
    [SerializeField] private Transform _Plane;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        _Plane.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
