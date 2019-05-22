using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectState
{
    IDLE, PICKED
}

public class ObjectBehaviour : MonoBehaviour
{
    
    public string ObjectName;
    public ObjectState State;
    public Transform PickObject;

    protected virtual void OnPick(){

    }

    protected virtual void OnIdel(){

    }



}
