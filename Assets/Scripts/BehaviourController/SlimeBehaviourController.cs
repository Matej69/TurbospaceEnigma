using UnityEngine;
using System.Collections;


public class SlimeBehaviourController : BehaviourController
{

  PhysicsCollisionController physicsCollisionController;

  void Awake()
	{
    physicsCollisionController = GetComponent<PhysicsCollisionController>();
    physicsCollisionController.SetSpriteRenderer(transform.Find("Sprite").GetComponent<SpriteRenderer>());
  }
	
	void Start () 
	{
    base.Start();
    SetPossibleVelocity(new Vector2(walkSpeed, 0));
   }

	void Update () 
	{
    base.FixedUpdate();
  }


  public override void HandleAnimation()
  {
    if (velocity.x > 0 || velocity.x < 0)
      GetComponent<Entity>().animationManager.SetAnimation("Walk");
    //else
    //    GetComponent<Entity>().animationManager.SetAnimation("Idle");
  }
  
  public override void HandleReactionAfterStimulus()
  {
    ApplyGravity();
    ApplyDeltaTime();
    ApplyLimitsToVelocity();
    
    PhysicsCollisionController.RayHitSide rayHitSide = GetComponent<PhysicsCollisionController>().CastRays(ref velocity);
    if (rayHitSide.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.LEFT) {
      velocity.x = Mathf.Abs(velocity.x);
      xFacingDir = Vector2.right;
    }
    else if (rayHitSide.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.RIGHT) {
      velocity.x = -Mathf.Abs(velocity.x);
      xFacingDir = Vector2.left;
    }
    else {
      xFacingDir = Vector2.zero;
    }
    
    ApplyVelocityToPosition();
    ApplySpriteDirection();
  }
  
  public override void OnGroundTouched() {
    if (GetComponent<PhysicsCollisionController>().IsGrounded()) {
      velocity.y = 0;
      curGravitationalForce = 0;
    }
  }
  
  public override void HandleVelocityStimulus() {
    SetPossibleVelocity(new Vector2(walkSpeed * Mathf.Sign(velocity.x), 0));
  }
  
  public override void OnDeath() {
    Destroy(gameObject);
  }



}
