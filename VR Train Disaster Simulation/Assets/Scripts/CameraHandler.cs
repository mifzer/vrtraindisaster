﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    
    [SerializeField] private Transform _Player;
    private Transform _CameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        _CameraTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _Player.eulerAngles = new Vector3(0, _CameraTransform.eulerAngles.y, 0);
    }

}
