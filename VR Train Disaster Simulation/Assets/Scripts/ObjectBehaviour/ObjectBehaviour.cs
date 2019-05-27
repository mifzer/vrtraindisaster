using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public enum ObjectState
// {
//     IDLE, PICKED
// }

public class ObjectBehaviour : MonoBehaviour
{
    
    // public ObjectState State;
    public Transform PickObject;
    public Transform ParentObject;

    public virtual void OnPick(){
        PickObject.parent = ParentObject;
        PickObject.localPosition = Vector3.zero;
    }

}
