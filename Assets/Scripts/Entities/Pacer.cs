using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer : Entity
{
    public int direction = 1;
    public float curTime = 0f;
    public float maxTime = 5f;
    public bool isPatrolling = true;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ActiveEffects();
        AttackTime();
        if(!isAttacking)
            Movement();

    }

    protected override void Movement()
    {
        if (isActive)
        {
            if (isPatrolling)
            {
                this.transform.Translate(curSpeed * Vector2.up * direction * Time.deltaTime);

                if (curTime > maxTime)
                {
                    direction *= -1;
                    curTime = 0;
                    //FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/static bug");
                    //instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
                    //instance.setVolume(0.3f);
                    //instance.start();
                    //instance.release();
                    //Shoot();
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, curSpeed * Time.deltaTime);
                Vector3 vectorToTarget = player.transform.position - this.transform.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;


                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100f * Time.deltaTime);

                if (curTime > maxTime)
                {
                    curTime = 0;

                    Shoot();
                    FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/angry bug");
                    instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
                    instance.setVolume(0.3f);
                    instance.start();
                    instance.release();
                }


            }
            curTime += Time.deltaTime;
        }

        lookDir = Vector2.right;

    }

    public void StartFight()
    {
        isPatrolling = false;
    }
}
