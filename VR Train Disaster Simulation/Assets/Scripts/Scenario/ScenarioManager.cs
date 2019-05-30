using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    
    public static ScenarioManager Instance;
    [SerializeField] private ScenarioData _ScenarioData;
    [SerializeField] private OVRScreenFade _ScreeFade;
    
    private float _CurrentTime;
    private IEnumerator _TimerHandler;

    [Header("Collider Simulation")]
    [SerializeField] private Collider[] _AllColliderPaluSimulation;
    [SerializeField] private Collider _ColliderAparSimulation;
    [SerializeField] private Collider[] _AllColliderRemSimulation;

    [Header("Fire Spot")]
    [SerializeField] private GameObject _Center;
    [SerializeField] private GameObject _Bordes;

    public int CounterStep = 0;
    public bool IsFinish = false;

    [SerializeField] private float _TimeLimit;

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        Initialize();
    }

    void Update(){
        if (OVRInput.Get(OVRInput.RawButton.Back)){
            StartCoroutine(DelayLoadScene());
        }
    }

    void Initialize(){
        
        // fire position
        EnableFireSpot();

        // setup collider simulation
        SetupColliderSimulation();

    }

    void StartTimer(){
        
        if(_TimerHandler != null)
            StopCoroutine(_TimerHandler);
        
        _TimerHandler = MyTimer();
        StartCoroutine(_TimerHandler);

    }

    void StopTimer(){
        if(_TimerHandler != null)
            StopCoroutine(_TimerHandler);
    }

    IEnumerator MyTimer(){
        
        while(true){

            if(_CurrentTime >= _TimeLimit){
                FinishScenario();
                break;
            }

            _CurrentTime += Time.deltaTime;
            yield return null;
        }

    }

    public void SaveFirstTimeReaction(){

        int minutes = Mathf.FloorToInt(_CurrentTime / 60F);
        int seconds = Mathf.FloorToInt(_CurrentTime - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        _ScenarioData.FirstTimeReaction = niceTime;
    }

    void SaveCompletitionTime(){
        int minutes = Mathf.FloorToInt(_CurrentTime / 60F);
        int seconds = Mathf.FloorToInt(_CurrentTime - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        _ScenarioData.CompletationTimeReaction = niceTime;
    }

    void FindErrorRate(){

        string key = _ScenarioData.ChairPosition + _ScenarioData.SimulationTypeOf.ToString() + _ScenarioData.FirePosition;
    
        int errorRate = ((CounterStep - CorrectStep(key)) / 2) / CorrectStep(key);

        _ScenarioData.ErrorRate = errorRate.ToString();
    }

    int CorrectStep(string key){

        int length = _ScenarioData.AllListSimulationCorrectStep.Length;
        int correctStep = 0;

        for (int i = 0; i < length; i++){
            if(key == _ScenarioData.AllListSimulationCorrectStep[i].Key){
                correctStep = _ScenarioData.AllListSimulationCorrectStep[i].CorrectStep;
            }
        }

        return correctStep;
    }

    public void FinishScenario(){

        // save complete time
        SaveCompletitionTime();

        // error rate
        FindErrorRate();

        // send data
        StartCoroutine(PostToForm());

        // load scene
        StartCoroutine(DelayLoadScene());
    }

#region SEND DATA

    IEnumerator PostToForm(){
        WWWForm myForm = new WWWForm();
        
        myForm.AddField("","");
        myForm.AddField("","");
        myForm.AddField("","");
        myForm.AddField("","");
        myForm.AddField("","");
        myForm.AddField("","");
        myForm.AddField("","");

        byte[] rawData = myForm.data;

        WWW request = new WWW("", rawData);
        yield return request;

    }

#endregion

    IEnumerator DelayLoadScene(){
        _ScreeFade.FadeOut();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void EnableFireSpot(){

        if(_ScenarioData.FirePosition == FireSpot.A){
            _Bordes.SetActive(true);
            _Center.SetActive(false);
        }else{
            _Bordes.SetActive(false);
            _Center.SetActive(true);
        }

    }

    void SetupColliderSimulation(){

        switch(_ScenarioData.SimulationTypeOf){
            
            case SimulationType.A:
                break;
            
            case SimulationType.B:

                // disable collider palu
                for(int i=0; i<_AllColliderPaluSimulation.Length; i++){
                    _AllColliderPaluSimulation[i].enabled = false;
                }

                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                break;

            case SimulationType.C:
                
                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                // disable collider apar
                _ColliderAparSimulation.enabled = false;

                break;

            case SimulationType.D:

                // disable collider palu
                for(int i=0; i<_AllColliderPaluSimulation.Length; i++){
                    _AllColliderPaluSimulation[i].enabled = false;
                }

                // disable collider apar
                _ColliderAparSimulation.enabled = false;

                break;
        }

    }
   

}
