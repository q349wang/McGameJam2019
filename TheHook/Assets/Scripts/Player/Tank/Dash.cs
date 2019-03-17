using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{

    public override void Fire()
    {
        Debug.Log("Dash");
        bPlayer.Dash();
        bPlayer.UseMana(abCost);
    }

    public override void Release()
    {

    }
}
