using UnityEngine;

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
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            ChangeColor(false); // red
            HitObject();
        }
        else
        {
            ChangeColor(true); // green
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {

            LaserOff();
        }
        else
        {
            ShootLaser();
        }

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

    void ShootLaser()
    {
        laser.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // if (hit.collider)
            // {
            //     laser.SetPosition(1, hit.point);
            //     if(!isGreen)Destroy(hit.transform.gameObject);
            // }
        }
        else
        {
            laser.SetPosition(1, transform.forward * 5000);
        }
    }

    void HitObject(){
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {

            // if (hit.collider.CompareTag("FireExtinguisher")){
            //     Debug.LogWarning("fire extinguisher");
            // }

            if(hit.collider.CompareTag("Hammer")){
                
                Transform myObject = hit.collider.GetComponent<HammerBehaviour>().PickObject;

                myObject.parent = _PlaceHolder;
                myObject.position = _PlaceHolder.position;

                // show pop up
                UIManager.Instance.ShowPopUp("palu telah diambil");

            }

        }
    }
}


