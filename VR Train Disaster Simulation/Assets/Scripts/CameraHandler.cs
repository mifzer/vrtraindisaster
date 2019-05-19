﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    
    [SerializeField] private Transform _Player;
    [SerializeField] private Transform _CameraTransform;


    // Update is called once per frame
    void Update()
    {
        _Player.eulerAngles = new Vector3(0, _CameraTransform.eulerAngles.y, 0);
        _CameraTransform.position = NewCameraPosition();
    }

    Vector3 NewCameraPosition(){
        Vector3 cameraPosition = new Vector3(_Player.position.x, _CameraTransform.position.y, _Player.position.z);
        return cameraPosition;
    }

}
