using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    
    public static ScenarioManager Instance;
    
    // public delegate void Scenario();
    // public event Scenario OnStartScenario;
    // public event Scenario OnFinishScenario;

    void Awake(){
        Instance = this;
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    void Initialize(){

    }

    public void FinishScenario(){
        
    }

    

}
