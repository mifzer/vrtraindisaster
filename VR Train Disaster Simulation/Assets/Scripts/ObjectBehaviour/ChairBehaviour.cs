using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairBehaviour : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other){
        ScenarioManager.Instance.CounterStep += 1;     
    }

}
