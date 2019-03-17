using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hooker : BasePlayer
{
    public Sprite hookerSprite;

    // Start is called before the first frame update
    protected override void Start()
    {
        fixedAbilities = new string[] { "Hook" };
        movementSpeed = 5;
        gameObject.layer = 9; // Hooker
        base.Start();

        foreach(GameObject ability in abilities)
        {
            ability.SetActive(true);
            ability.transform.parent = null;
            ability.GetComponent<HookUtils.Hook>().SetPlayer(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
