﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Healer : BasePlayer
{
    public Sprite healerSprite;
    //[SerializeField]
    //private GameObject healObject;

    // Start is called before the first frame update
    protected override void Start()
    {
        //healObject = (GameObject)Resources.Load("HealObject", typeof(GameObject));
        //Heal heal = healObject.GetComponent<Heal>();
        gameObject.AddComponent<Heal>();
        movementSpeed = 6;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}