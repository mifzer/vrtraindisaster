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

        // apar
        // if(OVRInput.Get(OVRInput.RawTouch.Any) && _ScenarioData.SimulationTypeOf == SimulationType.APAR){
        //     // keluarin semprotan

        // }

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

    public void FinishScenario(){
        

        
        // save data
        
        // load scene
        StartCoroutine(DelayLoadScene());
    }

    IEnumerator DelayLoadScene(){
        _ScreeFade.FadeOut();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }

    void EnableFireSpot(){

        if(_ScenarioData.FirePosition == FireSpot.BORDES){
            _Bordes.SetActive(true);
            _Center.SetActive(false);
        }else{
            _Bordes.SetActive(false);
            _Center.SetActive(true);
        }

    }

    void SetupColliderSimulation(){

        switch(_ScenarioData.SimulationTypeOf){
            
            case SimulationType.FREE_TRIAL:
                break;
            
            case SimulationType.APAR:

                // disable collider palu
                for(int i=0; i<_AllColliderPaluSimulation.Length; i++){
                    _AllColliderPaluSimulation[i].enabled = false;
                }

                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                break;

            case SimulationType.PALU:
                
                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                // disable collider apar
                _ColliderAparSimulation.enabled = false;

                break;

            case SimulationType.REM:

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
