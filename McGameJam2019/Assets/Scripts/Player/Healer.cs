using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : BasePlayer
{
    public Sprite healerSprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        movementSpeed = 6;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
