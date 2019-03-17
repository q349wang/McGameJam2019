using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Healer : BasePlayer
{
    public Sprite healerSprite;
    private GameObject healObject;


    // Start is called before the first frame update
    protected override void Start()
    {
        fixedAbilities = new string[] { "HealerAbilities" };
        movementSpeed = 6;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
