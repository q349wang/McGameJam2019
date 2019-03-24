using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{

    public override void Fire()
    {
        Debug.Log("Dash");
        bPlayer.Dash();
    }

    public override void Release()
    {

    }
}
