using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAccelerometer : MonoBehaviour
{

    private float speed;
    [SerializeField] private UnityEngine.UI.Text _DebuggingText;
    [SerializeField] private float _ShakeThreshold = 1;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Vector3.zero;

        // // we assume that device is held parallel to the ground
        // // and Home button is in the right hand

        // // remap device acceleration axis to game coordinates:
        // //  1) XY plane of the device is mapped onto XZ plane
        // //  2) rotated 90 degrees around Y axis
        // dir.x = -Input.acceleration.y;
        // dir.z = Input.acceleration.x;

        // // dir = Input.acceleration;

        // // clamp acceleration vector to unit sphere
        // if (dir.sqrMagnitude > 1)
        //     dir.Normalize();

        // // Make it move 10 meters per second instead of 10 meters per frame...
        // dir *= Time.deltaTime;

        // // Move object
        // transform.Translate(dir * speed);
        // _DebuggingText.text = dir.x + " , " + dir.z + " , " + transform.position + " , " + (dir * speed).ToString();

        dir = Input.acceleration;

        if(dir.sqrMagnitude > _ShakeThreshold){
            transform.position += transform.forward;
        }

        _DebuggingText.text = dir.sqrMagnitude.ToString();
    }


}
