using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerAbility1 : Ability
{

    [SerializeField] private float range = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        this.cost = 1;
        this.coolDown = 0;
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
