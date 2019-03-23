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
    protected int MaxHealth = 100;
    [SyncVar(hook = "OnHealthChanged")]
    public int health = 100;
    public int CurrentHealth
    {
        get { return health; }
    }

    protected int MaxMana = 100;
    [SyncVar]
    public int mana = 100;
    public int CurrentMana
    {
        get { return mana; }
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

        if (isServer && this is Hooker)
        {
            EndGame.AddLegend();
        }
        else if (isServer)
        {
            EndGame.AddSurvivor();
        }
    }

    public GameObject AddAbility(GameObject abilityPrefab)
    {
        GameObject instance = Instantiate(abilityPrefab.gameObject, transform);
        abilities.Add(instance.GetComponent<Ability>());
        return instance;
    }
    

    // Update is called once per frame
    void Update()
    {

    }

    //[Command] // this will always run on server, so have to do isserver checks on bullet/gun - disbale collision when firing? noo then won't dissapear
    public void ServerTakeDamage(float amount)
    {
        // blocking should be done differently
        if (!isServer || player.IsDead || IsBlocking) return;
        // setting it twice in the same frame triggers hook twice?
        this.health = (int)Mathf.Clamp(this.health - (int)amount, 0f, this.MaxHealth);
    }

    // should be called on the listen server too?
    protected void OnHealthChanged(int newHealth)
    {
        Debug.Log("health changed to " + newHealth);
        this.health = newHealth;
        if (!player.IsDead && this.health == 0)
        {
            player.Kill(true);
        }
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
        if (player.IsDead)
        {
            this.health = this.MaxHealth;
            player.Kill(false);
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
