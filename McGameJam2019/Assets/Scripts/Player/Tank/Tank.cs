using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : BasePlayer
{
    public Sprite tankSprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        movementSpeed = 3;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
