using UnityEngine;
using System.Collections;


public class LinearBullet : MonoBehaviour, IBullet
{

  [HideInInspector]
  public Vector2 velocity;
  public float gravity = 0f;
  public int dmg;


  void Start()
  {
  }

  public void Update()
  {
    HandleMovement();
  }




  public void SetBulletDirection(int horDirX)
  {
    velocity.x = (horDirX == 1) ? -Mathf.Abs(velocity.x) : Mathf.Abs(velocity.x);
  }

  public void OnEnemyTouch()
  {
    Destroy(gameObject);
  }

  public void HandleMovement()
  {
    velocity.y -= gravity * Time.deltaTime;
    transform.Translate(velocity * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if ((GlobalInformation.instance.mask_enemy & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
    {
      other.transform.parent.GetComponent<Enemy>().OnBulletHit(dmg);
      OnEnemyTouch();
    }
  }





}
