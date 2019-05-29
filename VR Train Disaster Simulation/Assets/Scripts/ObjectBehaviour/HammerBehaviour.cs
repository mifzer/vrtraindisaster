using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : ObjectBehaviour
{

    public override void OnPick(){
        base.OnPick();

        UIManager.Instance.ShowPopUp("Palu telah diambil");
        ScenarioManager.Instance.SaveFirstTimeReaction();
        
    }

}
