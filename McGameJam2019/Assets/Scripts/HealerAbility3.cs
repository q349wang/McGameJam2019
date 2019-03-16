using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAbility3 : Ability
{

    private float range;

    // Start is called before the first frame update
    void Start()
    {
        this.cost = 65;
        this.coolDown = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Run()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, range);
    }
}
