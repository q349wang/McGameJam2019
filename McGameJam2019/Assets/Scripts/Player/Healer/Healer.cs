using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Healer : BasePlayer
{
    public Sprite healerSprite;
    [SerializeField]
    private GameObject healObject;

    // Start is called before the first frame update
    void Reset()
    {
        base.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
