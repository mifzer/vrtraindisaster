using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowBehaviour : MonoBehaviour
{
    
    private bool _IsTriggered = false;

    [SerializeField] private AudioSource _CrackAudio;

    void OnTriggerEnter(Collider other){
        
        if(other.CompareTag("Hammer")){
            
            // if(_IsTriggered == true)
            //     return;

            Debug.Log("crack");
            _CrackAudio.Play();

            // show pop up
            UIManager.Instance.ShowPopUp("kaca nya pecah");
            
        }
    }

}
