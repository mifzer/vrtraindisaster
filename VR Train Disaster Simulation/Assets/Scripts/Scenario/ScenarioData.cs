using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimulationType
{
    // FREE_TRIAL, APAR, PALU, REM
    A,B,C,D 
}

public enum FireSpot
{
    // BORDES, CENTER
    A,B
}

[System.Serializable]
public class ErrorEquation{
    public string Key;
    public int SimulationNumber;
    public int CorrectStep; 
}

[CreateAssetMenu(fileName = "CurrentScenarioData", menuName = "Scenario Data/CurrentScenarioData")]
public class ScenarioData : ScriptableObject
{
    [Header("Biodata")]
    public string Name;

    [Header("Scenario")]
    public string ChairPosition;
    public SimulationType SimulationTypeOf;
    public FireSpot FirePosition;

    [Header("Time")]
    public string FirstTimeReaction;
    public string CompletationTimeReaction;
    public string TimeDate;

    [Header("Error Equation")]
    public string ErrorRate;
    public ErrorEquation[] AllListSimulationCorrectStep;

    
}
