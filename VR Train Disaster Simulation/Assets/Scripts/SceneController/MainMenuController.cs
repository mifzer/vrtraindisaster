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

    [Header("Menu")]
    [SerializeField] private TMPro.TMP_InputField _InputFieldName;
   
    [Header("Scenario Status")]
    [SerializeField] private TMPro.TextMeshProUGUI _ScenarioText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start(){
        ResetData();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.A)){
    //         SelectFireSpot("A");
    //         StartScenario();
    //     }    

    //     if(Input.GetKeyDown(KeyCode.B)){
    //         SelectFireSpot("B");
    //         StartScenario();
    //     }
    // }

    void ResetData(){

        _ScenarioData.Name = "";
        SelectPosition("A");
        SelectSimulation("A");
        SelectFireSpot("A");

        _ScenarioData.FirstTimeReaction = "";
        _ScenarioData.CompletationTimeReaction = "";
        _ScenarioData.ErrorRate = "";

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
        
        mymenu.transform.DOMoveY(firstPosition.y - 5, 0.4f).OnComplete(() => mymenu.SetActive(false));
        mymenu.GetComponent<CanvasGroup>().DOFade(0,0.3f);

    }

    // public void InputName(TMPro.TMP_InputField inputfield){
    //     _ScenarioData.Name = inputfield.text;
    // }

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

    public void ShowStatusScenario(){

        switch(_ScenarioData.SimulationTypeOf){
            
            // free trial
            case SimulationType.A:
                 _ScenarioText.text = "----------";
                break;

            // apar
            case SimulationType.B:
                _ScenarioText.text = "PEMADAM KEBAKARAN";
                break;

            // palu
            case SimulationType.C:
                _ScenarioText.text = "PALU DARURAT";
                break;

            // rem
            case SimulationType.D:
                _ScenarioText.text = "REM TANGAN";
                break;

        }

        // scenario
        // _ScenarioText.text = _ScenarioData.SimulationTypeOf.ToString();
    }

#endregion

    public void StartScenario(){

        // add name data
        _ScenarioData.Name = _InputFieldName.text;

        StartCoroutine(DelayScene());
    }

    IEnumerator DelayScene(){
        // ScreenFade.Instance.FadeOut();
        _ScreenFade.FadeOut();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void AddChar(string character){
        _InputFieldName.text = _InputFieldName.text + character;
    }

    public void AddSpace(){
        _InputFieldName.text = _InputFieldName.text + " ";
    }

    public void RemoveCharacter(){
        _InputFieldName.text = _InputFieldName.text.Remove(_InputFieldName.text.Length - 1);
    }

}
