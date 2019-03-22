using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tank : BasePlayer
{
    // Start is called before the first frame update
    protected override void Start()
    {
        movementSpeed = 3;
        normalSpeed = movementSpeed;
        blockingSpeed = 0.5f * movementSpeed;
        dashSpeed = 2 * movementSpeed;
        base.Start();
    }

}
