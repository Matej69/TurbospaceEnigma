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
        SetPossibleVelocity(new Vector2(1f, 0));
    }

	void FixedUpdate() 
	{
        OnGroundTouched();
        HandleVelocityStimulus();
        HandleReactionAfterStimulus();
    }






    public void HandleVelocityStimulus()
    {
        if (Input.GetKey(KeyCode.W) && GetComponent<PhysicsCollisionController>().IsGrounded())
            SetPossibleVelocity(new Vector2(velocity.x, velocity.y + jumpForce));

        float xSpeed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        if (Input.GetKey(KeyCode.A))
            SetPossibleVelocity(new Vector2(-xSpeed, velocity.y));
        else if (Input.GetKey(KeyCode.D))
            SetPossibleVelocity(new Vector2(xSpeed, velocity.y));
        else
            SetPossibleVelocity(new Vector2(0, velocity.y));
    }
    public void HandleReactionAfterStimulus()
    {
        ApplyGravity();
        ApplyDeltaTime();
        ApplyLimitsToVelocity();
        CastRaysAndMaybeAlterkVelocity();
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }




}
