using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube : obstacle
{
    public override float effect(float damage)
    {
        return damage;
    }
}
