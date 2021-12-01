using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public List<Pacer> npcs = new List<Pacer>();
    public Collider2D col;
    public Animator anim;
    public GameManager gm;
    public bool isOpen = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D _col)
    {
      switch (_col.gameObject.tag)
      {
          case "Player":

              anim.SetTrigger("Open");
              for (int i = 0; i < npcs.Count; i++)
              {
                npcs[i].StartFight();
              }
              if (npcs.Count > 0 && !isOpen)
              {
                  isOpen = true;
                  FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/alarm");
                  instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
                  instance.setVolume(0.2f);
                  instance.start();
                  instance.release();
                  gm.StartFight();
              }
              break;

              default:
                  break;
      }
    }
}
