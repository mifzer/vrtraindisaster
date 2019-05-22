using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PopUpController : MonoBehaviour
{
    
    [SerializeField] private Transform _Panel;
    [SerializeField] private Vector3 _FirstPosition;
    [SerializeField] private CanvasGroup _CanvasGroup;
    [SerializeField] private Text _TextMessage;

    // Start is called before the first frame update
    void Start(){
        _Panel = this.transform;
    }

    public void OnShow(string message){
        _TextMessage.text = message;
        // _Panel.gameObject.SetActive(true);
        _CanvasGroup.DOFade(1, 0.2f);
        _Panel.DOLocalMove(Vector3.zero, 0.4f);
    }

    public void OnHide(){
        _Panel.DOLocalMove(_FirstPosition, 0.4f).OnComplete(() => _CanvasGroup.DOFade(0, 0.2f));
    }

    
}
