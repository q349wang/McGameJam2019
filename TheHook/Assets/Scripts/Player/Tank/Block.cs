using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Ability
{
    // private BasePlayer bPlayer;
    private SpriteRenderer sr;
    private Collider2D c;
    private bool isBlocking = false;
    public void Start()
    {
        base.Start();
        // bPlayer = transform.GetComponentInParent<BasePlayer>();
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = false;
        c = GetComponent<Collider2D>();
        c.enabled = false;
        transform.localEulerAngles = new Vector3(0, 0, -90);
        transform.localPosition = new Vector3(0.36f, 0, 0);
    }

    float timeSinceManaUse = 0f;

    private void Update()
    {
        if (isBlocking)
        {
            timeSinceManaUse += Time.deltaTime;
            if (timeSinceManaUse >= 0.3f)
            {
                bPlayer.ServerUseMana(abCost);
                timeSinceManaUse = 0f;
            }
        }
        else
        {
            timeSinceManaUse = 0f;
        }
        //if (Input.GetButton(abilityButton) && bPlayer.CurrentMana >= abCost)
        //{
        //    //Debug.Log("Blocking");
        //    //Debug.Log("Mana " + bPlayer.CurrentMana);
        //    
        //}
        //else
        //{
        //    Debug.Log("Not Blocking");
        //    
        //}
    }

    public override void Fire()
    {
        bPlayer.Block();
        bPlayer.ServerUseMana(abCost);
        sr.enabled = true;
        c.enabled = true;
        isBlocking = true;
        //if (isBlocking == false)
        //{
        //    bPlayer.Block();
        //    bPlayer.UseMana(abCost);
        //    sr.enabled = true;
        //    isBlocking = true;
        //}
        //else
        //{
        //    bPlayer.Unblock();
        //    sr.enabled = false;
        //    isBlocking = false;
        //}
    }

    public override void OnButtonRelease()
    {
        bPlayer.Unblock();
        sr.enabled = false;
        isBlocking = false;
        c.enabled = false;
    }

}
