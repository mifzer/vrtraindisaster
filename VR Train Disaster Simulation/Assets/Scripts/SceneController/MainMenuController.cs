using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    
    [Header("Sceneario Data")]
    [SerializeField] private ScenarioData _ScenarioData;

    [Header("Screen Fade")]
    [SerializeField] private ScreenFade _ScreenFade;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ShowMenu(GameObject mymenu){
        
        // setup
        Vector3 firstPosition = mymenu.transform.position;
        mymenu.SetActive(true);
        mymenu.transform.position = new Vector3(mymenu.transform.position.x, mymenu.transform.position.y - 5, mymenu.transform.position.z);

        // fading
        mymenu.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
        
        // transform
        mymenu.transform.DOMoveY(firstPosition.y, 0.4f);
        
    }

    public void HideMenu(GameObject mymenu){
        
        // setup
        Vector3 firstPosition = mymenu.transform.position;
        
        mymenu.transform.DOMoveY(firstPosition.y - 5, 0.4f);
        mymenu.GetComponent<CanvasGroup>().DOFade(0,0.2f).SetDelay(0.2f).OnComplete(() => mymenu.SetActive(false));

    }

    void StartingFade(){
        
    }

}
