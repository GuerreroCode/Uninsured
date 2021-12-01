using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  [SerializeField] protected Vector2 direction = Vector2.zero;
  [SerializeField] protected float speed = 0;
  [SerializeField] protected Effect effect;


  // Update is called once per frame
  void Update()
  {
      this.transform.Translate(speed * direction * Time.deltaTime);
  }

  public void SetDirection(Vector2 lookDir)
  {
      this.direction = lookDir;
      direction.Normalize();
  }

  public void SetSpeed(float _speed)
  {
      speed = _speed;
  }
  private void OnTriggerEnter2D(Collider2D coll)
  {

      if (this.gameObject.tag != "clear")
      {
          Entity entity = coll.gameObject.GetComponent<Entity>();

            if (entity != null)
              entity.AddEffect(effect);
      }
      if (coll.gameObject.tag != "door")
        Destroy(this.gameObject);
  }
}
