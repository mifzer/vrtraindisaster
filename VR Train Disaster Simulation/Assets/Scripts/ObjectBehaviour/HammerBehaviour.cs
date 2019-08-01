using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : ObjectBehaviour
{

    [ContextMenu("ambil palu")]
    public override void OnPick(){
        base.OnPick();

        UIManager.Instance.ShowPopUp("Palu telah diambil");
        // ScenarioManager.Instance.SaveFirstTimeReaction();
        
    }

}
