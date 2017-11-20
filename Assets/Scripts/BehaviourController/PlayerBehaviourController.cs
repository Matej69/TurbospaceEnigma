using UnityEngine;
using System.Collections;


public class PlayerBehaviourController : BehaviourController
{

	void Awake()
	{
    }
	
	void Start () 
	{
        base.Start();
        SetPossibleVelocity(new Vector2(0f, 0));
    }

	void FixedUpdate() 
	{
        base.FixedUpdate();
    }






    public override void HandleVelocityStimulus()
    {
        if (Input.GetKey(KeyCode.UpArrow) && GetComponent<PhysicsCollisionController>().IsGrounded())
            SetPossibleVelocity(new Vector2(velocity.x, velocity.y + jumpForce));

        float xSpeed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        if (Input.GetKey(KeyCode.LeftArrow))
            SetPossibleVelocity(new Vector2(-xSpeed, velocity.y));
        else if (Input.GetKey(KeyCode.RightArrow))
            SetPossibleVelocity(new Vector2(xSpeed, velocity.y));
        else
            SetPossibleVelocity(new Vector2(0, velocity.y));
    }
    public override void HandleReactionAfterStimulus()
    {
        ApplyGravity();
        ApplyDeltaTime();
        ApplyLimitsToVelocity();
        CastRaysAndMaybeAlterkVelocity();
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }
    public override void OnGroundTouched()
    {
        if (GetComponent<PhysicsCollisionController>().IsGrounded())
        {
            velocity.y = 0;
            curGravitationalForce = 0;
        }
    }
    public override void HandleAnimation()
    {
        if (velocity.x > 0 || velocity.x < 0)
            GetComponent<Entity>().animationManager.SetAnimation("Walk");
        else
            GetComponent<Entity>().animationManager.SetAnimation("Idle");
    }




}
