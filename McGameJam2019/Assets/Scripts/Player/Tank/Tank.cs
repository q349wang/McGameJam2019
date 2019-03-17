using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tank : BasePlayer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        fixedAbilities = new string[] { "TankAbilities" };
        movementSpeed = 3;
        normalSpeed = movementSpeed;
        blockingSpeed = 0.5f * movementSpeed;
        base.Start();
    }

}
