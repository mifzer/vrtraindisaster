using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;

    [SerializeField] private PopUpController _PopUp;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        
    }

#region pop up

    private IEnumerator _DelayHidePopUp;

    public void ShowPopUp(string message){
        _PopUp.OnShow(message);

        if(_DelayHidePopUp != null)
            StopCoroutine(_DelayHidePopUp);

        _DelayHidePopUp = DelayHidePopUp(2);
        StartCoroutine(_DelayHidePopUp);
    }

    IEnumerator DelayHidePopUp(float delaytime){
        yield return new WaitForSeconds(delaytime);
        _PopUp.OnHide();
    }

#endregion


}
