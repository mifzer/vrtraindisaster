using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoorBehaviour : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other){
        ScenarioManager.Instance.FinishScenario();    
    }

}
