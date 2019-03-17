using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : RaycastAbility
{


    LineRenderer laserLine;

    public void Start()
    {
        base.Start();
        abCoolDown = 5;
        abCost = 20;
        gunDamage = -20;
        weaponRange = 5;

        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    public override void Fire()
    {
        Vector2 rayOrigin = transform.position;

        Vector2 direction = transform.right.normalized;

        //Declare a raycast hit to store information about what our raycast has hit.
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, weaponRange, LayerMask.GetMask("Player"));
        
        //Set the start position for our visual effect for our laser to the position of gunEnd
        laserLine.SetPosition(0, new Vector3(rayOrigin.x, rayOrigin.y, 10));

        //Set the end position for our laser line 
        Vector2 endPos = rayOrigin + (direction * weaponRange);
        laserLine.SetPosition(1, new Vector3(endPos.x, endPos.y, 10));
        StartCoroutine(ShotEffect());

        //Check if our raycast has hit anything
        if (hit.collider != null)
        {
            Healer self = transform.GetComponentInParent<Healer>();

            //Get a reference to a health script attached to the collider we hit
            BasePlayer target = hit.collider.GetComponent<BasePlayer>();

            //If there was a player script attached
            if (target != null && self != null)
            {
                //Call the damage function of that script, passing in our gunDamage variable
                target.Damage(gunDamage);
                self.UseMana(abCost);
            }
            Debug.Log("Healing " + hit.collider.gameObject.name);
            Debug.Log("Mana: " + self.CurrentMana);
            self.UseMana(abCost);
            Debug.Log("Mana: " + self.CurrentMana);

            //Check if the object we hit has a rigidbody attached
            if (hit.rigidbody != null)
            {
                //Add force to the rigidbody we hit, in the direction it was hit from
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }
        }
        else
        {
            //if we did not hit anything, set the end of the line to a position directly away from
            //laserLine.SetPosition(1, transform.right * weaponRange);
            Debug.Log("No allies");
        }
    }

    private IEnumerator ShotEffect()
    {
        //Turn on our line renderer
        laserLine.enabled = true;
        //Wait for .07 seconds
        yield return .1;

        //Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
}
