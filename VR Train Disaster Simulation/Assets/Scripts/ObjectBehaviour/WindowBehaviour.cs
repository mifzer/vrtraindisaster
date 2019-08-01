using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour : MonoBehaviour
{
    
    private bool _IsTriggered = false;
    private int _KnockCounter = 0;

    [Header("Cracked Window")]
    [SerializeField] private GameObject _CrackedWindow;

    [Header("Audio")]
    [SerializeField] private AudioSource _CrackAudio;
    [SerializeField] private AudioSource _KnockAudio;


    // void OnTriggerEnter(Collider other){
        
    //     if(other.CompareTag("Hammer")){
            
    //         // if(_IsTriggered == true)
    //         //     return;

    //         Debug.Log("crack");
    //         _CrackAudio.Play();

    //         // show pop up
    //         UIManager.Instance.ShowPopUp("kaca nya pecah");
            
    //         // balik ke main menu scene
    //         ScenarioManager.Instance.FinishScenario();
    //     }
    // }

    void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Hammer")){
            
            // if(_IsTriggered == true)
            //     return;
            _KnockCounter += 1;

            if(_KnockCounter == 5){
                
                // false the collider
                GetComponent<BoxCollider>().enabled = false;

                _CrackedWindow.SetActive(true);

                _CrackAudio.Play();

                // show pop up
                UIManager.Instance.ShowPopUp("kaca nya pecah");
                
                // balik ke main menu scene
                ScenarioManager.Instance.FinishScenario();
                return;
            }

            _KnockAudio.Play();
            // Debug.Log("crack");
            // _CrackAudio.Play();

        }
    }

}
