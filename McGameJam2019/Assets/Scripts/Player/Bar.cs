using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    protected Vector2 localScale;
    protected Vector2 startPosition;
    protected Quaternion startRotation;
    private BasePlayer player;
    [SerializeField]
    private string barType = "Health";
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<BasePlayer>();
        startPosition = transform.localPosition;
        startRotation = transform.rotation;
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = player.transform.position;
        transform.position = playerPos + startPosition;
        transform.rotation = startRotation;
        if(barType == "Health")
        {
            float remHealth = player.CurrentHealth;
            localScale.x = remHealth/100;
        } else if(barType == "Mana")
        {
            float remMana = player.CurrentMana;
            localScale.x = remMana/100;
        }

        transform.localScale = localScale;
        
    }
}
