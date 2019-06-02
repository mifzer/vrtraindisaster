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
    [SerializeField] private Collider[] _AllColliderAparSimulation;
    [SerializeField] private Collider[] _AllColliderRemSimulation;
    [SerializeField] private Collider[] _AllColliderKeluarBordes;

    [Header("Fire Spot")]
    [SerializeField] private GameObject _Center;
    [SerializeField] private GameObject _Bordes;

    public float CounterStep = 0;
    public FinishSimulation FinishState;
    [SerializeField] private string _StartingTime;
    [SerializeField] private string _FinishTime;

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

        if(Input.GetKeyDown(KeyCode.Space)){
            // StartCoroutine(PostToForm());
            FinishScenario();
        }
    }

    int _PlayerNumber = 0;

    void Initialize(){
        _StartingTime = System.DateTime.Now.ToString();

        // fire position
        EnableFireSpot();

        // setup collider simulation
        SetupColliderSimulation();

        StartTimer();

        if(PlayerPrefs.HasKey("Player")){
            PlayerPrefs.SetInt("Player",1);
        }else{
            _PlayerNumber = PlayerPrefs.GetInt("Player");
            PlayerPrefs.SetInt("Player", _PlayerNumber++);
        }
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
        
        // _StartingTime = System.DateTime.Now.ToString();

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
    
        float errorRate = ((CounterStep - CorrectStep(key)) / 2);
        Debug.Log(errorRate);
        
        _ScenarioData.ErrorRate = errorRate.ToString() + " / " + CorrectStep(key).ToString(); 
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

        // save to time stamp finish
        _FinishTime = System.DateTime.Now.ToString();

        if(_CurrentTime >= _TimeLimit){
            FinishState = FinishSimulation.FAIL;
        }else{
            FinishState = FinishSimulation.FINISH;
        }

        // save complete time
        SaveCompletitionTime();

        // error rate
        FindErrorRate();

        // send data
        // StartCoroutine(PostToForm());

        // load scene
        // StartCoroutine(DelayLoadScene());

        // saved data
        SaveData();
    }

#region SAVE DATA

    void SaveData(){
        SavedDataScenario saveData = new SavedDataScenario();

        saveData.Name = PlayerPrefs.GetInt("Player").ToString();
        saveData.StartPos = _ScenarioData.ChairPosition.ToString();
        saveData.Simulation = _ScenarioData.SimulationTypeOf.ToString();
        saveData.FirePos = _ScenarioData.FirePosition.ToString();
        saveData.UrutanScenario = _ScenarioData.ChairPosition + _ScenarioData.SimulationTypeOf.ToString() + _ScenarioData.FirePosition.ToString();
        saveData.FinishStatus = FinishState.ToString();
        saveData.ReactionTime = _ScenarioData.FirstTimeReaction;
        saveData.CompleteTime = _ScenarioData.CompletationTimeReaction;
        saveData.ErrorRate = _ScenarioData.ErrorRate;
        saveData.CounterStep = CounterStep.ToString();
        saveData.CorrectStep = CorrectStep(_ScenarioData.ChairPosition + _ScenarioData.SimulationTypeOf.ToString() + _ScenarioData.FirePosition.ToString()).ToString();
        saveData.StartingTime = _StartingTime;
        saveData.FinishTime = _FinishTime;

        string temp = JsonUtility.ToJson(saveData);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/" + System.DateTime.Now.ToString() + ".json", temp);

        UIManager.Instance.ShowPopUp("Data Saved!");
        StartCoroutine(DelayLoadScene());
    }

#endregion

#region SEND DATA

    IEnumerator PostToForm(){
        WWWForm myForm = new WWWForm();
        
        // myForm.AddField("entry.40503718", _ScenarioData.Name); // name
        myForm.AddField("entry.40503718", "" + PlayerPrefs.GetInt("Player")); // name
        myForm.AddField("entry.1044271779", _ScenarioData.ChairPosition.ToString()); // chair start position
        myForm.AddField("entry.1821314532", _ScenarioData.SimulationTypeOf.ToString()); // kind of simulation
        myForm.AddField("entry.2058439020", _ScenarioData.FirePosition.ToString()); // fire spot
        myForm.AddField("entry.476343051", _ScenarioData.ChairPosition + _ScenarioData.SimulationTypeOf.ToString() + _ScenarioData.FirePosition.ToString()); // urutan skenario
        myForm.AddField("entry.709853587", FinishState.ToString()); // gagal atau finish
        myForm.AddField("entry.1688379912", _ScenarioData.FirstTimeReaction); // reaction time
        myForm.AddField("entry.1033728782", _ScenarioData.CompletationTimeReaction); // completetion time
        myForm.AddField("entry.317004812", _ScenarioData.ErrorRate); // error rate
        myForm.AddField("entry.1694726491", CounterStep.ToString()); // counter step
        myForm.AddField("entry.375297286", CorrectStep(_ScenarioData.ChairPosition + _ScenarioData.SimulationTypeOf.ToString() + _ScenarioData.FirePosition.ToString())); // correct step
        myForm.AddField("entry.807008128", _StartingTime); // Timestamp Start
        myForm.AddField("entry.1947457399", _FinishTime); // Timestamp Finish

        byte[] rawData = myForm.data;

        WWW request = new WWW("https://docs.google.com/forms/d/e/1FAIpQLScU8067VaIz8GKw7JE5mWEMA8mOsDidHtr8lIrGYqlw0Ki9FQ/formResponse", rawData);
        // UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Post("https://docs.google.com/forms/d/e/1FAIpQLScU8067VaIz8GKw7JE5mWEMA8mOsDidHtr8lIrGYqlw0Ki9FQ/formResponse", myForm);
        // yield return request.SendWebRequest();
        yield return request;

        if (request.error != null)
        {
            // Debug.Log(request.error);
            UIManager.Instance.ShowPopUp(request.error);
        }
        else
        {
            // Debug.Log("Form upload complete!");
            UIManager.Instance.ShowPopUp("Data upload complete!");
            StartCoroutine(DelayLoadScene());
        }

        request.Dispose();
        

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
            
            // case SimulationType.A:
            //     break;
            
            case SimulationType.B:

                // disable collider palu
                for(int i=0; i<_AllColliderPaluSimulation.Length; i++){
                    _AllColliderPaluSimulation[i].enabled = false;
                }

                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                // disable collider keluar bordes
                for(int i=0; i<_AllColliderKeluarBordes.Length; i++){
                    _AllColliderKeluarBordes[i].enabled = false;
                }

                break;

            case SimulationType.C:

                // disable collider rem
                for(int i=0; i<_AllColliderRemSimulation.Length; i++){
                    _AllColliderRemSimulation[i].enabled = false;
                }

                // disable collider apar
                for(int i=0; i<_AllColliderAparSimulation.Length; i++){
                    _AllColliderAparSimulation[i].enabled = false;
                }
                
                // disable collider keluar bordes
                for(int i=0; i<_AllColliderKeluarBordes.Length; i++){
                    _AllColliderKeluarBordes[i].enabled = false;
                }

                break;

            case SimulationType.D:

                // disable collider palu
                for(int i=0; i<_AllColliderPaluSimulation.Length; i++){
                    _AllColliderPaluSimulation[i].enabled = false;
                }

                // disable collider apar
                for(int i=0; i<_AllColliderAparSimulation.Length; i++){
                    _AllColliderAparSimulation[i].enabled = false;
                }

                // disable collider keluar bordes
                for(int i=0; i<_AllColliderKeluarBordes.Length; i++){
                    _AllColliderKeluarBordes[i].enabled = false;
                }

                break;
        }

    }
   

}
