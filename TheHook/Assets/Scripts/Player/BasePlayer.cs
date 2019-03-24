using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class BasePlayer : NetworkBehaviour
{

    public Rigidbody2D rigidBody;
    [SerializeField]
    private PlatformerCharacter2D player;

    // Player stats //
    protected int MaxHealth = 100;
    [SyncVar(hook = "OnHealthChanged")]
    public int health = 100;
    public int CurrentHealth
    {
        get { return health; }
    }

    protected int MaxMana = 100;
    [SyncVar(hook = "OnManaChanged")]
    public int mana = 100;
    public int CurrentMana
    {
        get { return mana; }
    }

    // Player state //
    private bool isDead;
    public bool IsDead
    {
        get { return isDead; }
    }
    
    private bool isBlocking = false;
    public bool IsBlocking
    {
        get { return isBlocking; }
    }

    [SerializeField]
    protected float movementSpeed = 0;
    protected float blockingSpeed = 0;
    protected float normalSpeed = 0;
    protected float dashSpeed = 0;

    [HideInInspector]
    public List<Ability> abilities;

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

        // replace this with abilities in prefab
        abilities = new List<Ability>(GetComponentsInChildren<Ability>());
        rigidBody = GetComponent<PlatformerCharacter2D>().m_Rigidbody2D;

        ResetPlayer();
    }


    // Abilities //

    public GameObject AddAbility(GameObject abilityPrefab)
    {
        GameObject instance = Instantiate(abilityPrefab.gameObject, transform);
        abilities.Add(instance.GetComponent<Ability>());
        return instance;
    }

    public void UseMana(float amount)
    {
        float sub = amount;
        float result = this.mana - sub;
        this.mana = (int)result;
    }

    public void castAbility(int cost)
    {
        this.mana = Mathf.Max(0, this.mana - cost);
    }


    ///// Taking Damage /////

    public void ServerTakeDamage(float amount)
    {
        // blocking should be done differently
        if (!isServer || IsDead || IsBlocking) return;
        // setting it twice in the same frame triggers hook twice?
        this.health = (int)Mathf.Clamp(this.health - (int)amount, 0f, this.MaxHealth);
    }

    // called on each client (including listen server)
    protected void OnHealthChanged(int newHealth)
    {
        if (health != newHealth)
        {
            Debug.Log("health changed to " + newHealth);
            this.health = newHealth;
            if (!IsDead && this.health == 0)
            {
                KillPlayer();
            }
        }
    }

    public void ServerUseMana(float amount)
    {
        if (!isServer || IsDead) return;
        this.mana = (int)Mathf.Clamp(this.mana - (int)amount, 0f, this.MaxMana);
    }

    protected void OnManaChanged(int newMana)
    {
        Debug.Log("health changed to " + newMana);
        this.mana = newMana;
    }
    public void ResetPlayer()
    {
        this.isDead = false;
        this.health = MaxHealth;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);

        // update endgame variables
        if (isServer)
        {
            if (this is Hooker)
            {
                EndGame.AddLegend();
            }
            else
            {
                EndGame.AddSurvivor();
            }
        }
    }

    public void KillPlayer()
    {
        this.isDead = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.5f);

        if (isServer)
        {
            if (this is Hooker)
            {
                EndGame.LegendDied();
            }
            else
            {
                EndGame.SurvivorDied();
            }
        }
    }
    public void ServerUseMana(float amount)
    {
        if (!isServer) return;
        this.mana = (int)Mathf.Clamp(this.mana - (int)amount, 0f, this.MaxMana);
    }

    protected void OnManaChanged(int newMana)
    {
        Debug.Log("Mana changed to " + newMana);
        this.mana = newMana;
    }


    //      //

    public void Stun()
    {
        if (!isBlocking)
        {
            StartCoroutine(player.StunPlayer());
        }
    }

    public void Cure()
    {
        player.Unstun();
    }

    public void Resurrect()
    {
        if (IsDead)
        {
            ResetPlayer();
        }
    }

    public void Block()
    {
        this.isBlocking = true;
        GetComponent<PlatformerCharacter2D>().m_MaxSpeed = blockingSpeed;
    }

    public void Unblock()
    {
        this.isBlocking = false;
        GetComponent<PlatformerCharacter2D>().m_MaxSpeed = normalSpeed;
    }

    public void Dash()
    {
        player.Dash();
    }

    public void addMana(int amount)
    {
        this.mana = Mathf.Min(MaxMana, this.mana + amount);
    }

    public void SetControl(bool enabled)
    {
        GetComponent<PlatformerCharacter2D>().isEnabled = enabled;
    }
}
