﻿using UnityEngine;

public class ObjectController : MonoBehaviour {

    private LineRenderer laser;
    bool isGreen = false;

    [SerializeField] private Transform _PlaceHolder;

    // Use this for initialization
    void Start () {
        laser = GetComponent<LineRenderer>();
        transform.localScale = new Vector3();
    }
	

	// Update is called once per frame
	void Update () {

        HitObject();
        ChangeColor(true); // green
        // ShootLaser();

        // if (OVRInput.Get(OVRInput.RawButton.A))
        // {
        //     ChangeColor(false); // red
        //     // HitObject();
        // }
        // else
        // {
        //     HitObject();
        // }

        // if (OVRInput.Get(OVRInput.Button.One))
        // {

        //     LaserOff();
        // }
        // else
        // {
        // }

	}

    public void ChangeColor(bool isGreen)
    {
        this.isGreen = isGreen;
        Color c = isGreen ? Color.green : Color.white;
        laser.startColor = c;
    }

    void LaserOff()
    {
        laser.SetPosition(0, transform.position);
        laser.SetPosition(1, transform.position);
    }

    // void ShootLaser()
    // {
    //     laser.SetPosition(0, transform.position);
    //     RaycastHit hit;
    //     if (Physics.Raycast(transform.position, transform.forward, out hit))
    //     {
    //         // if (hit.collider)
    //         // {
    //         //     laser.SetPosition(1, hit.point);
    //         //     if(!isGreen)Destroy(hit.transform.gameObject);
    //         // }
    //     }
    //     else
    //     {
    //         laser.SetPosition(1, transform.forward * 5000);
    //     }
    // }

    void HitObject(){
        // Debug.Log("hitter");
        laser.SetPosition(1, transform.forward);

        RaycastHit hit;
        Ray myRay = new Ray(transform.position, transform.forward);
        

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)){
            Debug.DrawRay(transform.position, transform.forward, Color.blue);
  
            if(hit.collider.tag == "Hammer"){
                    
                Transform myObject = hit.collider.GetComponent<HammerBehaviour>().PickObject;

                myObject.parent = _PlaceHolder;
                myObject.position = _PlaceHolder.position;

                // show pop up
                UIManager.Instance.ShowPopUp("palu telah diambil");
                Debug.Log("get hammer");
            }

            // if (OVRInput.Get(OVRInput.RawButton.A)){
                
            //     ChangeColor(false);

            //     if(hit.collider.tag == "Hammer"){
                    
            //         Transform myObject = hit.collider.GetComponent<HammerBehaviour>().PickObject;

            //         myObject.parent = _PlaceHolder;
            //         myObject.position = _PlaceHolder.position;

            //         // show pop up
            //         UIManager.Instance.ShowPopUp("palu telah diambil");

            //     }

            // }


        } else {
            Debug.DrawRay(transform.position, transform.forward, Color.yellow);  
        }
    }
}


