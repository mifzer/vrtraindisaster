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
    [SerializeField] private ScreenFade _ScreenFade;

   
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

#region Scenario

    // start position scneario
    public void SelectPosition(Toggle mytoggle){
        string key = mytoggle.GetComponent<OptionProperties>().Key;
        
        mytoggle.isOn = true;
        _ScenarioData.ChairPosition = System.Convert.ToInt32(key);
    }

    public void SelectSimulation(Toggle mytoggle){
        SimulationType key = (SimulationType) System.Enum.Parse(typeof(SimulationType), mytoggle.GetComponent<OptionProperties>().Key);

        mytoggle.isOn = true;
        _ScenarioData.SimulationTypeOf = key;
    }

    public void SelectFireSpot(Toggle mytoggle){
        FireSpot key = (FireSpot) System.Enum.Parse(typeof(FireSpot), mytoggle.GetComponent<OptionProperties>().Key);

        mytoggle.isOn = true;
        _ScenarioData.FirePosition = key;
    }

#endregion

    public void StartScenario(){
        ScreenFade.Instance.FadeOut();
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene");
    }

}
