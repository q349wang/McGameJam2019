using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class BasePlayer : NetworkBehaviour
{
    [SerializeField]
    private PlatformerCharacter2D player;
    protected int MaxHealth = 100;
    public int health = 100;
    public int CurrentHealth
    {
        get { return health; }
    }

    public const int MaxMana = 100;
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

    protected string[] fixedAbilities;

    public List<GameObject> abilities;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        abilities = new List<GameObject>();
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

        //if (hasAuthority)
        //{
        //    player.CmdSpawnAbilities(fixedAbilities);
        //}
        foreach (string ability in fixedAbilities)
        {
            GameObject abilityObject = (GameObject)Resources.Load(ability, typeof(GameObject));
            GameObject instance = Instantiate(abilityObject, transform);
            abilities.Add(instance);
        }
    }

    public GameObject AddAbility(GameObject abilityPrefab)
    {
        GameObject instance = Instantiate(abilityPrefab.gameObject, transform);
        abilities.Add(instance);
        return instance;
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
            if (!isBlocking)
            {
                this.health = Mathf.Min(0, this.health -= dmg);
            }
        }

        if(this.health == 0)
        {
            player.Kill(true);
        }
        return true;
    }

    public void UseMana(int amount)
    {
        float sub = amount * Time.deltaTime;
        float result = this.mana - sub;
        this.mana = (int)result;
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
}
