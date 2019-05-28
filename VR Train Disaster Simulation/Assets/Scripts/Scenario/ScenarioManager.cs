using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    
    public static ScenarioManager Instance;
    [SerializeField] private ScenarioData _ScenarioData;
    [SerializeField] private OVRScreenFade _ScreeFade;

    [Header("Collider Simulation")]
    [SerializeField] private Collider[] _AllColliderPaluSimulation;
    [SerializeField] private Collider _ColliderAparSimulation;
    [SerializeField] private Collider[] _AllColliderRemSimulation;

    [Header("Fire Spot")]
    [SerializeField] private GameObject _Center;
    [SerializeField] private GameObject _Bordes;

    [Header("Start Position")]
    [SerializeField] private Transform _ChairTwo;
    [SerializeField] private Transform _ChairSeven;
    [SerializeField] private Transform _ChairTwelve;
    [SerializeField] private Transform _Player;

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        
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

        // setup position
        SetupPosition();

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

    void SetupPosition(){
        
        switch(_ScenarioData.ChairPosition){
            
            case 2:
                _Player.position = _ChairTwo.position;
                break;

            case 7:
                _Player.position = _ChairSeven.position;
                break;

            case 12:
                _Player.position = _ChairTwelve.position;
                break;

        }

    }

    

}
