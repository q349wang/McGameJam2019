using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAbility2 : Ability
{

    private float range;

    // Start is called before the first frame update
    void Start()
    {
        this.cost = 100;
        this.coolDown = 1000;
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
