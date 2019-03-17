using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Ability
{
    private BasePlayer bPlayer;
    private SpriteRenderer sr;
    //private bool isBlocking;
    public void Start()
    {
        base.Start();
        bPlayer = transform.GetComponentInParent<BasePlayer>();
        sr = GetComponent<SpriteRenderer>();
        transform.localEulerAngles = new Vector3(0, 0, -90);
        transform.localPosition = new Vector3(0.36f, 0, 0);
    }

    private void Update()
    {
        if (Input.GetButton(abilityButton) && bPlayer.CurrentMana >= abCost)
        {
            Debug.Log("Blocking");
            Debug.Log("Mana " + bPlayer.CurrentMana);
            bPlayer.Block();
            bPlayer.UseMana(abCost);
            sr.enabled = true;
        }
        else
        {
            Debug.Log("Not Blocking");
            bPlayer.Unblock();
            sr.enabled = false;
        }
    }

    public override void Fire()
    {
        
    }

}
