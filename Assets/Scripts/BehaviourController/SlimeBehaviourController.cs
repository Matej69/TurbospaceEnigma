﻿using UnityEngine;
using System.Collections;


public class SlimeBehaviourController : BehaviourController
{

	void Awake()
	{
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


    public override void HandleVelocityStimulus()
    {
        SetPossibleVelocity(new Vector2(walkSpeed * Mathf.Sign(velocity.x) , 0));
    }
    public override void HandleReactionAfterStimulus()
    {
        ApplyGravity();
        ApplyDeltaTime();
        ApplyLimitsToVelocity();

        PhysicsCollisionController.RayHitSide rayHitSide = CastRaysAndMaybeAlterkVelocity();
        if (rayHitSide.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.LEFT)
            velocity.x = Mathf.Abs(velocity.x);
        else if (rayHitSide.hor == PhysicsCollisionController.E_RAY_HIT_SIDE.RIGHT)
            velocity.x = -Mathf.Abs(velocity.x);

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
        //else
        //    GetComponent<Entity>().animationManager.SetAnimation("Idle");
    }


}