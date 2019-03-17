using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class BasePlayer : UnityEngine.Networking.NetworkBehaviour
{
    [SerializeField]
    private PlatformerCharacter2D player;
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
    protected float movementSpeed = 0;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        GetComponent<PlatformerCharacter2D>().m_MaxSpeed = movementSpeed;
        player = transform.GetComponent<PlatformerCharacter2D>();
        if (isLocalPlayer)
        {
            Camera main = Camera.main;
            if (main != null)
            {
                main.GetComponent<Camera2DFollow>().target = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool Damage(float amount)
    {
        int dmg = (int)amount;
        if(dmg < 0)
        {
            if (this.health == this.MaxHealth)
            {
                return false;
            }
            else
            {
                this.health -= dmg;
            }
        }
        else
        {
            this.health = Mathf.Min(0, this.health -= dmg);
        }

        if(this.health == 0)
        {
            player.Kill(true);
        }
        return true;
    }

    public void UseMana(int amount)
    {
        this.mana -= amount;
    }

    public void Stun()
    {
        StartCoroutine(player.StunPlayer());
    }

    public void Cure()
    {
        player.Unstun();
    }

    public void Resurrect()
    {
        if (player.IsDead)
        {
            this.health = this.MaxHealth;
            player.Kill(false);
        }
    }
}
