using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowObstacle : MonoBehaviour {

    public GameObject obstacle;
    float timer = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if(timer > 1)
        {
            Instantiate(obstacle, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), 50), Quaternion.identity);
            timer = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
