using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerBehaviour : ObjectBehaviour
{
   
   public override void OnPick(){

       PickObject.parent = ParentObject;
       PickObject.localPosition = Vector3.zero;

   }

}
