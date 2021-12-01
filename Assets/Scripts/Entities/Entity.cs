using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    //Effects List
    [SerializeField] protected List<Effect> statusEffects = new List<Effect>();

    //Physical Properties
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Vector2 movement;
    [SerializeField] protected Collider2D col;
    [SerializeField] protected GameObject splat;


    //Entity Stats
    [SerializeField] protected float baseHealth = 10f;
    [SerializeField] protected float maxHealth = 10f;
    [SerializeField] protected float curHealth = 10f;
    [SerializeField] protected float baseStrength = 10;
    [SerializeField] protected float curStrength = 10;
    [SerializeField] protected float baseSpeed = 2f;
    [SerializeField] protected float curSpeed = 2f;

    //Entity activity
    [SerializeField] protected bool isActive = true;
    [SerializeField] protected float inactiveTime = 0f;
    [SerializeField] protected float maxInactiveTime = 1f;
    [SerializeField] protected Vector2 pushDir;


    //player stuff
    [SerializeField] protected Equip equip;
    [SerializeField] protected Equip baseHands;
    [SerializeField] protected bool isHands = false;
    [SerializeField] protected Vector2 lookDir;
    [SerializeField] protected bool isAttacking = false;
    [SerializeField] protected float attackTime = 0;
    [SerializeField] protected float maxAttackTime = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        curSpeed = baseSpeed;
        curStrength = baseStrength;
    }

    // Update is called once per frame
    void Update()
    {
        ActiveEffects();
        AttackTime();
    }

    protected void ActiveEffects()
    {
        if (!isActive)
        {
            inactiveTime += Time.deltaTime;
            rb.velocity = pushDir;

            if (inactiveTime > maxInactiveTime)
            {
                isActive = true;
                rb.velocity = Vector2.zero;
                inactiveTime = 0f;
            }
        }
    }

    protected virtual void GetHit(float hitPoints)
    {
        curHealth -= hitPoints;

        if (curHealth <= 0f)
        {
            Die();
        }
        else
        {
            GetPushed(-lookDir,3f);
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/heavy push");
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            instance.setVolume(.8f);
            instance.start();
            instance.release();
        }
    }

    protected virtual void Die()
    {
        DropEquip(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/splat");
        Instantiate(splat, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }

    //Moves based on curSpeed and WASD
    protected virtual void Movement()
    {

    }

    //Aims based on cur position and mouse position
    protected virtual void Aim()
    {

    }

    //calls equips shoot method
    protected virtual void Shoot()
    {
        if (equip.Shoot(lookDir))
        {
            SetAttacking(true);
        }
    }

    protected virtual void SetAttacking(bool _isAttacking)
    {
        isAttacking = _isAttacking;
        rb.velocity = Vector2.zero;

    }

    protected virtual void AttackTime()
    {
      if (isAttacking)
      {

          attackTime += Time.deltaTime;

          if (attackTime > maxAttackTime)
          {
              attackTime = 0;
              SetAttacking(false);

              equip.Release();
          }


      }
    }


    public virtual void PickupEquipment(Equip newEquip, int ammo)
    {

        DropEquip(true);
        SetEquip(newEquip, ammo);
        isHands = false;

    }
    //Sets equipment from outside object
    public virtual void SetEquip(Equip newEquip, int ammo)
    {

        Destroy(equip.gameObject);
        equip = Instantiate(newEquip, this.transform.position, this.transform.rotation).GetComponent<Equip>();
        equip.SetEntity(this);
        equip.SetAmmo(ammo);
        equip.gameObject.transform.parent = this.transform;
        equip.gameObject.layer = this.gameObject.layer;

    }

    //sets object from hand prefab
    protected virtual void DropEquip(bool isReplaced)
    {
        if (!isHands)
        {
            Vector2 normalDir = lookDir;
            normalDir.Normalize();
            Pickup dropPickup = Instantiate(equip.GetPickup(), ((Vector2)this.transform.position - normalDir), this.transform.rotation);
            dropPickup.SetAmmo(equip.GetAmmo());
        }
        if (!isReplaced)
        {
            SetEmptyHands();
        }

    }

    protected void SetEmptyHands()
    {

      SetEquip(baseHands,0);
      isHands = true;
    }


    public virtual void AddEffect(Effect newEffect)
    {
        if(newEffect.AddEffect(this))
            statusEffects.Add(newEffect);

    }

    public void RemoveEffect(Effect oldEffect)
    {
        statusEffects.Remove(oldEffect);
    }

    public virtual bool SetSpeedPercentage(float speedVar)
    {

        if ((curSpeed + (baseSpeed * speedVar)) > 1f)
        {
            curSpeed += (baseSpeed * speedVar);
            return true;
        }

        return false;

    }

    public virtual bool SetStrengthPercentage(float strengthVar)
    {
        if ((curStrength + (baseStrength * strengthVar)) > .5f)
        {
            curStrength += (baseStrength * strengthVar);
            return true;
        }

        return false;
    }

    public virtual bool SetHealthPercentage(float healthVar)
    {

        if ((maxHealth + (baseHealth * healthVar)) > 1f)
        {
            maxHealth += (baseHealth * healthVar);

              if (maxHealth < curHealth || maxHealth > baseHealth)
              {
                  curHealth = maxHealth;
              }

              return true;
        }

        return false;
    }

    public virtual void GetPushed(Vector2 direction, float playerStrength)
    {
        float pushStrength = (playerStrength - this.curStrength) / 1.5f;
        if (pushStrength < 0.5f)
            pushStrength = 0.5f;
        pushDir = direction *  pushStrength;
        isActive = false;
    }

    protected virtual void ClearEffects()
    {
        foreach (Effect effect in statusEffects)
        {
            effect.RemoveEffect();
        }
        statusEffects.Clear();
    }


    //PUBLIC METHODS
    //return aim
    public Vector2 GetAim()
    {
        return lookDir;
    }

    public float GetStrength()
    {
        return curStrength;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        switch (coll.gameObject.tag)
        {
            case "hand":
              Equip hand = coll.gameObject.GetComponent<Equip>();
              GetPushed(hand.GetAim(), hand.GetStrength());
                FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/light push");
                instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
                instance.setVolume(1.2f);
                instance.start();
                instance.release();
              break;
            case "melee":
              MeleeChild melee = coll.gameObject.GetComponent<MeleeChild>();
              GetHit(melee.GetMeleeDamage());
              break;

            case "clear":
                ClearEffects();
                break;
            default:
              break;
        }
    }



}
