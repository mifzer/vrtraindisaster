using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

    private LineRenderer _Laser;
    [SerializeField] private bool _IsMainMenu;
    private float _LaserLength;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _Laser = GetComponent<LineRenderer>();    
    }

	// Update is called once per frame
	void Update () {

        if(_IsMainMenu){
            _LaserLength = 5000;
        }else{
            _LaserLength = 1;
        }

		LaserPosition();
        MyRaycast();
	}

    void LaserPosition(){
        _Laser.SetPosition(0, transform.position);
        _Laser.SetPosition(1, transform.position + transform.forward * _LaserLength);
    }

    void MyRaycast(){
        RaycastHit hit;
        Ray myRay = new Ray(transform.position, transform.forward * _LaserLength);

        Debug.DrawRay(transform.position, transform.forward * _LaserLength, Color.yellow);
    }

}
