using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : BasePlayer
{
    public Sprite hookerSprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        movementSpeed = 5;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
