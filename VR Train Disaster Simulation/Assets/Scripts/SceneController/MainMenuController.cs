using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    
    [Header("Sceneario Data")]
    [SerializeField] private ScenarioData _ScenarioData;

    [Header("Screen Fade")]
    // [SerializeField] private ScreenFade _ScreenFade;
    [SerializeField] private OVRScreenFade _ScreenFade;

   
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
        
        mymenu.transform.DOMoveY(firstPosition.y - 5, 0.4f).OnComplete(() => mymenu.SetActive(false));
        mymenu.GetComponent<CanvasGroup>().DOFade(0,0.3f);

    }

    public void InputName(TMPro.TMP_InputField inputfield){
        _ScenarioData.Name = inputfield.text;
    }

#region Scenario

    // start position scneario
    public void SelectPosition(string key){
        Startpos myKey = (Startpos) System.Enum.Parse(typeof(Startpos), key);
        _ScenarioData.ChairPosition = myKey;
    }

    public void SelectSimulation(string key){
        SimulationType myKey = (SimulationType) System.Enum.Parse(typeof(SimulationType), key);
        _ScenarioData.SimulationTypeOf = myKey;
    }

    public void SelectFireSpot(string key){
        FireSpot mykey = (FireSpot) System.Enum.Parse(typeof(FireSpot), key);
        _ScenarioData.FirePosition = mykey;
    }

#endregion

    public void StartScenario(){
        StartCoroutine(DelayScene());
    }

    IEnumerator DelayScene(){
        // ScreenFade.Instance.FadeOut();
        _ScreenFade.FadeOut();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

}
