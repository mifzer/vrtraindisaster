using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SimulationType
{
    FREE_TRIAL, APAR, PALU, REM 
}

public enum FireSpot
{
    BORDES, CENTER
}

[CreateAssetMenu(fileName = "CurrentScenarioData", menuName = "Scenario Data/CurrentScenarioData")]
public class ScenarioData : ScriptableObject
{
    [Header("Biodata")]
    public string Name;

    [Header("Scenario")]
    public int ChairPosition;
    public SimulationType SimulationTypeOf;
    public FireSpot FirePosition;

}
