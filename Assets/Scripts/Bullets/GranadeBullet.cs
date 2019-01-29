using UnityEngine;
using System.Collections;


public class GranadeBullet : MonoBehaviour, IBullet
{

    public GameObject pref_explosion;
    private PhysicsCollisionController collisionControler;

    [HideInInspector]
    public Vector2 velocity = new Vector2(1,1);
    public float gravity;

    private Timer timer_explodeTime;
    public float explodeTime;

	void Awake()
	{
    collisionControler = GetComponent<PhysicsCollisionController>();
    collisionControler.SetSpriteRenderer(GetComponent<SpriteRenderer>());
  }
	
	void Start () 
	{
    timer_explodeTime = new Timer(explodeTime);
	}

    
	void Update () 
	{
    HandleMovement();
    HandleLifetime();
  }


  public void SetBulletDirection(int horDirX)
  {
    velocity.x = (horDirX == 1) ? -Mathf.Abs(velocity.x) : Mathf.Abs(velocity.x);
  }

  public void OnEnemyTouch()
  {
    Instantiate(pref_explosion, transform.position, Quaternion.identity);
    Destroy(gameObject);
  }

  public void HandleMovement()
  {
    velocity.y -= gravity * Time.deltaTime;
    //velocity only used in collision function
    Vector2 velocityWithDelta = velocity * Time.deltaTime;
    PhysicsCollisionController.RayHitSide rayHitSideInfo = collisionControler.CastRays(ref velocityWithDelta);
    if (rayHitSideInfo.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.LEFT || rayHitSideInfo.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.RIGHT)
      velocity.x = (velocity.x / 2f) * -1;
    if (rayHitSideInfo.ver == PhysicsCollisionController.E_RAY_HIT_SIDE.BOTTOM || rayHitSideInfo.ver == PhysicsCollisionController.E_RAY_HIT_SIDE.BOTTOM) {
      velocity.x = velocity.x / 1.2f;
      velocity.y = (velocity.y / 2f) * -1;
    }
    transform.Translate(velocity * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other)
  {
    if ((GlobalInformation.instance.mask_enemy & 1 << other.gameObject.layer) == (1 << other.gameObject.layer)) {
      timer_explodeTime.SetCurrentTime(0f);
    }
  }

  private void HandleLifetime()
  {
    timer_explodeTime.Tick(Time.deltaTime);
    if (timer_explodeTime.IsFinished())
      OnEnemyTouch();
  }


}
