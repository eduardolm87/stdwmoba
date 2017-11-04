using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : ToolItem
{
    public override void UseOnCreature(Creature owner, Creature target)
    {
        owner.ShootHitbox(target.transform.position);

        owner.CancelInteraction();
    }
}
