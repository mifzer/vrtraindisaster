using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RemBehaviour : ObjectBehaviour
{

    [Header("rotation")]
    [SerializeField] private Vector3 _FirstRotation;
    [SerializeField] private Vector3 _TargetRotation;

    [Header("audio rem")]
    [SerializeField] private AudioSource _RemAudio;

    public override void OnPick(){
        ScenarioManager.Instance.SaveFirstTimeReaction();
        PickObject.DOLocalRotate(_TargetRotation, 0.5f, RotateMode.Fast).OnComplete(() => FinishAction());
    }

    void FinishAction(){
        // play audio
        _RemAudio.Play();
        ScenarioManager.Instance.FinishScenario();
    }

}
