using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Equip
{
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float curFireTime = 0;
    [SerializeField] private Item item;



    // Update is called once per frame
    void Update()
    {
        curFireTime += Time.deltaTime;
    }

    public override bool Shoot(Vector2 lookDir)
    {
        if ((curFireTime > fireRate) && ammo > 0)
        {
            //for (int i = 0; i < numSpread; i++)
            //{

                Vector2 normalDir = lookDir;
                normalDir.Normalize();
                Item newProjectile = Instantiate(item.gameObject, ((Vector2)this.transform.position + normalDir), this.transform.rotation).GetComponent<Item>();
                newProjectile.gameObject.layer = this.gameObject.layer;
                //newProjectile.SetDirection(lookDir);
                newProjectile.SetDirection(Vector2.right);
                newProjectile.SetSpeed(5f);
            //}
            FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/shoot");
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            instance.setVolume(2f);
            instance.start();
            instance.release();
            ammo--;

            curFireTime = 0;
        }
        return false;
    }


}
