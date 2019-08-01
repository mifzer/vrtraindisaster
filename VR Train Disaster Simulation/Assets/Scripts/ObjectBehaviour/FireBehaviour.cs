using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireBehaviour : MonoBehaviour
{

    [SerializeField] private Material[] _MyFire;

    public void FireFade(){
        for(int i=0; i<_MyFire.Length; i++){
            _MyFire[0].DOFade(0,0.3f);
        }
    }

}
