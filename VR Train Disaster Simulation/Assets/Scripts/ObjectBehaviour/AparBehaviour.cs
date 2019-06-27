using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AparBehaviour : ObjectBehaviour
{
    
    // [SerializeField] private ScenarioData _ScenarioData;
    [SerializeField] private bool _IsActive = false;
    [SerializeField] private GameObject _Smoke;
    [SerializeField] private AudioSource _AparSound;

    [SerializeField] private float _FireLimitTime;
    private float _FireExtinguisherPower = 0;

    // [Header("Fire Material")]
    // [SerializeField] private Material[] _MyFire;

    // [SerializeField] private float _Threshold = 0;
    // private float _Power = 0;

    // gameobject semprotan

    public override void OnPick(){
        base.OnPick();
        // ScenarioManager.Instance.SaveFirstTimeReaction();
        PickObject.eulerAngles = new Vector3(0,90,0);
        
        UIManager.Instance.ShowPopUp("Apar telah diambil");
        _IsActive = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.Space)){
        //     // StartCoroutine(PostToForm());
        //     FireHandler(null);
        // }

        if(_IsActive){
            
            if(OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger)){
                _AparSound.Play();
                _Smoke.SetActive(true);

                _FireExtinguisherPower += Time.deltaTime;
            }

            if(OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)){
                _AparSound.Stop();
                _Smoke.SetActive(false);
            }

            if(Input.GetKeyDown(KeyCode.Space)){
                _AparSound.Play();
                _Smoke.SetActive(true);

                _FireExtinguisherPower += Time.deltaTime;
            }

            if(Input.GetKeyUp(KeyCode.Space)){
                _AparSound.Stop();
                _Smoke.SetActive(false);
            }

        }
        
    }

    public void FireHandler(GameObject fire){

        if(_IsActive == false)
            return;

        if(_FireExtinguisherPower >= _FireLimitTime){
            
            if(fire != null)
                fire.SetActive(false);
                
            UIManager.Instance.ShowPopUp("Api telah padam");
            ScenarioManager.Instance.FinishScenario();
        }

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

    // void OnTriggerEnter(Collider other){
        
    //     if(other.CompareTag("fire")){
    //         _IsActive = true;
    //     }

    // }

    // void OnTriggerExit(Collider other){
    //     // _IsActive = false;
    //     // _Power = 0;
    //     if(other.CompareTag("fire")){
    //         _IsActive = false;
    //     }
    // }

}
