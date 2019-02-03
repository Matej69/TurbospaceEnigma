using UnityEngine;
using System.Collections;


public class PlayerBehaviourController : BehaviourController
{

    void Awake()
    {
      //Random.InitState(123);
    
    }

    void Start()
    {
        base.Start();
        SetPossibleVelocity(new Vector2(0f, 0));
    }

    void FixedUpdate()
    {
        base.FixedUpdate();
    }






    public override void HandleReactionAfterStimulus()
    {
        ApplySpriteDirection();
    }
    public override void OnGroundTouched()
    {
        /*
        if (GetComponent<PhysicsCollisionController>().IsGrounded())
        {
            velocity.y = 0;
            curGravitationalForce = 0;
        }
        */
    }
    public override void HandleAnimation()
    {
        if (velocity.x > 0 || velocity.x < 0)
            GetComponent<Entity>().animationManager.SetAnimation("Walk");
        else
            GetComponent<Entity>().animationManager.SetAnimation("Idle");
    }




}