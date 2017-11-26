using UnityEngine;
using System.Collections;


public class GranadeBullet : Bullet {


    public GameObject pref_explosion;
    private PhysicsCollisionController collisionControler;

    private Timer timer_explodeTime;
    public float explodeTime;

	void Awake()
	{
        base.Awake();
        collisionControler = GetComponent<PhysicsCollisionController>();

    }
	
	void Start () 
	{
        timer_explodeTime = new Timer(explodeTime);
	}

    /*
	void Update () 
	{
        base.Update();
	}
    */
    
    public override void OnEnemyTouch()
    {
        Instantiate(pref_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);        
    }

    public override void HandleBehaviour()
    {
        HandleMovement();
        HandleLifetime();
    }

    private void HandleMovement()
    {
        velocity.y -= gravity * Time.deltaTime;
        //velocity only used in collision function
        Vector2 velocityWithDelta = velocity * Time.deltaTime;
        PhysicsCollisionController.RayHitSide rayHitSideInfo = collisionControler.CastRays(ref velocityWithDelta);
        if (rayHitSideInfo.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.LEFT || rayHitSideInfo.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.RIGHT)
            velocity.x = (velocity.x / 2f) * -1;
        if (rayHitSideInfo.ver == PhysicsCollisionController.E_RAY_HIT_SIDE.BOTTOM || rayHitSideInfo.ver == PhysicsCollisionController.E_RAY_HIT_SIDE.BOTTOM)
        {
            velocity.x = velocity.x / 1.2f;
            velocity.y = (velocity.y / 2f) * -1;
        }
        transform.Translate(velocity * Time.deltaTime);
    }

    private void HandleLifetime()
    {
        timer_explodeTime.Tick(Time.deltaTime);
        if (timer_explodeTime.IsFinished())
            OnEnemyTouch();
    }






}
