using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopUpController : MonoBehaviour
{
    
    [SerializeField] private Transform _Panel;
    [SerializeField] private Vector3 _FirstPosition;

    // Start is called before the first frame update
    void Start(){
        _Panel = this.transform;
    }

    public void OnShow(string message){
        _Panel.gameObject.SetActive(true);
        _Panel.DOLocalMove(Vector3.zero, 0.4f);
    }

    public void OnHide(){
        _Panel.DOLocalMove(_FirstPosition, 0.4f).OnComplete(() => _Panel.gameObject.SetActive(false));
    }

    
}
