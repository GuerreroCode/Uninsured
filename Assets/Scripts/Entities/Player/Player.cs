using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    [SerializeField] private GameObject pushCollider;
    [SerializeField] private Animator pushAnim;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float pushTime = 0.4f;
    [SerializeField] private GameManager gm;

    //UI
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject effectBar;
    [SerializeField] private GameObject strengthUp;
    [SerializeField] private GameObject strengthDown;
    [SerializeField] private GameObject speedUp;
    [SerializeField] private GameObject speedDown;
    [SerializeField] private GameObject healthUp;
    [SerializeField] private GameObject healthDown;

    [SerializeField] private GameObject equipBar;
    [SerializeField] private GameObject broom;
    [SerializeField] private GameObject gun;



    void Start()
    {
        gm = GameManager.Instance;
        gm.SetPlayer(this);
        if (gm.IsNextLevel())
        {
            SetPlayerStats();
        }
    }

    void Initialize()
    {
        SetHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        ActiveEffects();

        if (!isAttacking)
        {
            Movement();
            Aim();
            Shoot();
            DropEquip();
            Push();
        }
        else
          AttackTime();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.EndGame();
        }

    }

    protected override void Die()
    {
        gm.ResetLevel();
        base.Die();
    }
    private void SetPlayerStats()
    {
        List<Effect> curStatusEffects = gm.GetStatusEffects();
        foreach(Effect curEffect in curStatusEffects)
        {
            AddEffect(curEffect);
        }

        SetEquip(gm.GetEquip(), 10);
    }

    //Moves based on curSpeed and WASD
    protected override void Movement()
    {
        movement.x =  Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
        rb.velocity = curSpeed * movement;
        mainCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10);
    }

    //Aims based on cur position and mouse position
    protected override void Aim()
    {
        lookDir = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    //calls equips shoot method
    protected override void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            maxAttackTime = equip.GetAttackTime();
            base.Shoot();
        }
    }

    protected override void SetAttacking(bool _isAttacking)
    {
        base.SetAttacking(_isAttacking);
        //cameraRb.velocity = Vector2.zero;
        if (!isAttacking)
        {
            equip.gameObject.SetActive(true);
            pushCollider.SetActive(false);
        }
    }

    protected override void GetHit(float damage)
    {
        base.GetHit(damage);
        SetHealthBar();
    }

    public override void AddEffect(Effect newEffect)
    {
        if(newEffect.AddEffect(this))
        {
            statusEffects.Add(newEffect);

            AddEffectUI(newEffect);
        }

    }

    public override bool SetHealthPercentage(float healthVar)
    {
        if ((maxHealth + (baseHealth * healthVar)) > 0)
        {
            maxHealth += (baseHealth * healthVar);

            if (maxHealth < curHealth || maxHealth > baseHealth)
            {
                curHealth = maxHealth;
            }

            SetHealthBar();
            return true;
        }



        return false;
    }

    //Sets equipment from outside object
    public override void SetEquip(Equip newEquip, int ammo)
    {
        base.SetEquip(newEquip, ammo);
        //AddEquipUI(newEquip);
    }

    //sets object from hand prefab
    protected void DropEquip()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            base.DropEquip(false);
            //Destroy(equipBar.transform.GetChild(0).gameObject);
        }
    }

    private void Push()
    {
        if (Input.GetMouseButton(1) && !isAttacking)
        {
            maxAttackTime = pushTime;
            SetAttacking(true);
            pushCollider.SetActive(true);
            equip.gameObject.SetActive(false);
            pushAnim.Play("Push");
        }
    }

    private void SetHealthBar()
    {
        healthBar.maxValue = this.maxHealth;
        healthBar.value = this.curHealth;
        gm.SetPlayerHealth(curHealth,maxHealth);
    }

    private void AddEquipUI(Equip newEquip)
    {
        string equipName = newEquip.name;
        GameObject newEquipUI = null;

        switch (equipName)
        {
            case "Broom":
                newEquipUI = broom;
                break;
            case "Gun":
                newEquipUI = gun;
                break;
            default:
                break;
        }

        if (newEquipUI != null && equipBar.transform.childCount == 0)
        {
            newEquipUI = Instantiate(newEquipUI, Vector3.zero, Quaternion.identity);
            newEquipUI.transform.parent = equipBar.transform;
            newEquipUI.transform.localPosition = Vector3.zero;
            newEquipUI.transform.localScale = new Vector3(480f, 480f, 0f);
            newEquipUI.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }


    private void AddEffectUI(Effect newEffect)
    {
        string effectName = newEffect.GetEffectName();
        GameObject newEffectUI = null;


        if (newEffect.GetPercentage() > 0)
            effectName += "Up";
        else
            effectName += "Down";


        switch (effectName)
        {
            case "StrengthUp":
                newEffectUI = strengthUp;
                break;
            case "StrengthDown":
                newEffectUI = strengthDown;
                break;
            case "SpeedUp":
                newEffectUI = speedUp;
                break;
            case "SpeedDown":
                newEffectUI = speedDown;
                break;
            case "HealthUp":
                newEffectUI = healthUp;
                break;
            case "HealthDown":
                newEffectUI = healthDown;
                break;
            default:
                break;
        }


        if (newEffectUI != null)
        {
            newEffectUI = Instantiate(newEffectUI, Vector3.zero, Quaternion.identity);
            newEffectUI.transform.parent = effectBar.transform;
            newEffectUI.transform.localPosition = new Vector3((50 * statusEffects.Count) - 90f, 0f ,0f);
            newEffectUI.transform.localScale = new Vector3(12f, 12f, 0f);
        }
    }

    protected override void ClearEffects()
    {
        base.ClearEffects();

        for  (int i = effectBar.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(effectBar.transform.GetChild(i).gameObject);
        }
    }


    public float GetBaseHealth()
    {
      return baseHealth;
    }


    public float GetBaseSpeed()
    {
      return baseSpeed;
    }

    public float GetBaseStrength()
    {
      return baseStrength;
    }

    public Equip GetEquip()
    {
        return equip;
    }

    public List<Effect> GetStatusEffects()
    {
        return statusEffects;
    }


/*
    public void OnTriggerEnter2D(Collider2D _col)
    {
        switch (_col.gameObject.tag)
        {
            case "pickup":
                //SetEquip(_col.gameObject.GetComponent<Pickup>().GetEquip());
                break;
            default:
                break;
        }
    }*/
}
