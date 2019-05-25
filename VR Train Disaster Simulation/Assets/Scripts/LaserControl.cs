using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour {

    private LineRenderer _Laser;

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
		LaserPosition();
        MyRaycast();
	}

    void LaserPosition(){
        _Laser.SetPosition(0, transform.position);
        _Laser.SetPosition(1, transform.position + transform.forward * 100);
    }

    void MyRaycast(){
        RaycastHit hit;
        Ray myRay = new Ray(transform.position, transform.forward * 5000);

        Debug.DrawRay(transform.position, transform.forward * 5000, Color.yellow);
    }

}
