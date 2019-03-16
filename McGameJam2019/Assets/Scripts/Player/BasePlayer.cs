using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    protected int MaxHealth = 100;
    int health = 0;
    public int CurrentHealth
    {
        get { return health; }
    }

    public const int MaxMana = 100;
    int mana = 0;
    public int CurrentMana
    {
        get { return mana; }
    }

    [SerializeField]
    float movementSpeed = 0;

    // Start is called before the first frame update
    protected virtual void Reset()
    {
        GetComponent<PlatformerCharacter2D>().m_MaxSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
