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
    [SerializeField] private float _FireExtinguisherPower = 0;
    [SerializeField] private GameObject[] _Fire;
    // [Header("Fire Material")]
    // [SerializeField] private Material[] _MyFire;

    // [SerializeField] private float _Threshold = 0;
    // private float _Power = 0;

    // gameobject semprotan

    [ContextMenu("ambil apar")]
    public override void OnPick(){
        base.OnPick();
        
        PickObject.eulerAngles = new Vector3(0,90,0);
        UIManager.Instance.ShowPopUp("Apar telah diambil");
        // _IsActive = true;
    }

    void Update()
    {


        if(_IsActive){
            
            if(_FireExtinguisherPower >= _FireLimitTime){
                
                for(int i = 0; i<_Fire.Length; i++){
                    _Fire[i].SetActive(false);
                }
                    
                UIManager.Instance.ShowPopUp("Api telah padam");
                ScenarioManager.Instance.FinishScenario();

                _IsActive = false;
            }
            
            if(OVRInput.Get(OVRInput.RawButton.RIndexTrigger)){
                _AparSound.Play();
                _Smoke.SetActive(true);

                _FireExtinguisherPower += Time.deltaTime;
            }

            if(OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger)){
                _AparSound.Stop();
                _Smoke.SetActive(false);
            }

            if(Input.GetKey(KeyCode.Space)){
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

        Debug.Log("pointer enter");

        if(_IsActive == false)
            return;

        if(_FireExtinguisherPower >= _FireLimitTime){
            
            if(fire != null)
                fire.SetActive(false);
                
            UIManager.Instance.ShowPopUp("Api telah padam");
            ScenarioManager.Instance.FinishScenario();

            _IsActive = false;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("fire")){
            _IsActive = true;
        }
    }

    void OnTriggerExit(Collider other){
        
        if(other.CompareTag("fire")){
            
            _IsActive = false;

            if(_AparSound.isPlaying)
                _AparSound.Stop();

            // if(_Smoke.activeInHierarchy)
                _Smoke.SetActive(false);

        }

    }

}
