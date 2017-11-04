using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolItem : MonoBehaviour
{

    [TextArea]
    public string Description = "???";

    public Sprite Icon;


    public float DistanceToInteract = 5;



    public virtual void UseOnCreature(Creature owner, Creature target)
    {
        Debug.Log(owner.name + " uses " + name + " over " + target.name);
        owner.CancelInteraction();
    }
}
