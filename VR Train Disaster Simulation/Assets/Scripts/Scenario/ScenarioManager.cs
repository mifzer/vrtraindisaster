using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    
    public static ScenarioManager Instance;
    [SerializeField] private ScenarioData _ScenarioData;

    // public delegate void Scenario();
    // public event Scenario OnStartScenario;
    // public event Scenario OnFinishScenario;

    [Header("Collider Simulation")]
    [SerializeField] private Collider[] _AllColliderPaluSimulation;
    [SerializeField] private Collider _ColliderAparSimulation;
    [SerializeField] private Collider[] _AllColliderRemSimulation;

    [Header("Fire Spot")]
    [SerializeField] private GameObject _Center;
    [SerializeField] private GameObject _Bordes;

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    void Initialize(){
        
        // fire position
        EnableFireSpot();

        // setup collider simulation
        SetupColliderSimulation();

    }

    public void FinishScenario(){
        
    }

    GameObject EnableFireSpot(){

        if(_ScenarioData.FirePosition == FireSpot.BORDES){
            _Bordes.SetActive(true);
            _Center.SetActive(false);
        }else{
            _Bordes.SetActive(false);
            _Center.SetActive(true);
        }

        return null;
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
