using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparBehaviour : ObjectBehaviour
{
    
    [SerializeField] private ScenarioData _ScenarioData;
    private bool _IsActive = false;
    [SerializeField] private float _Threshold = 0;
    private float _Power = 0;

    // gameobject semprotan

    public override void OnPick(){
        base.OnPick();
        ScenarioManager.Instance.SaveFirstTimeReaction();
    }

    public void FireHandler(){

        if(_IsActive == false)
            return;
        
        ScenarioManager.Instance.FinishScenario();
    }

    // // Update is called once per frame
    // void Update(){
        
    //     if(_IsActive == false)
    //         return;

    //     // if(_Power > _Threshold){
    //     //     ScenarioManager.Instance.FinishScenario();
    //     //     return;
    //     // }

    //     if(_ScenarioData.SimulationTypeOf == SimulationType.APAR && OVRInput.Get(OVRInput.RawButton.Any)){
    //         // show gameobject

    //         // _Power += Time.deltaTime;
    //         ScenarioManager.Instance.FinishScenario();
    //         _IsActive = false;
    //     }

    // }

    void OnTriggerEnter(Collider other){
        
        if(other.CompareTag("fire")){
            _IsActive = true;
        }

    }

    void OnTriggerExit(Collider other){
        // _IsActive = false;
        // _Power = 0;
        if(other.CompareTag("fire")){
            _IsActive = false;
        }
    }

}
