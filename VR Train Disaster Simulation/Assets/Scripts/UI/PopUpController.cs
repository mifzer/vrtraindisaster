using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpController : MonoBehaviour
{
    
    public string Message;
    [SerializeField] private Transform _Panel;
    [SerializeField] private Vector3 _FirstPosition;

    // Start is called before the first frame update
    void Start(){
        _Panel = this.transform;
    }

    public void OnShow(){
        _Panel.gameObject.SetActive(true);
        _Panel.DOLocalMoveY
    }

    public void OnHide(){



    }

    
}
