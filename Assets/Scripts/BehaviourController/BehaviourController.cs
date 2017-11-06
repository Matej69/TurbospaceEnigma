using UnityEngine;
using System.Collections;


public class BehaviourController : MonoBehaviour {

    
    float curGravitationalForce = 0.1f;
    GameObject obj_sprite;
    public Vector2 velocity;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;
    public float jumpForce;

    public void Awake()
	{        
    }
	
	public void Start () 
	{
        obj_sprite = GetComponent<Entity>().obj_sprite;
    }

	void FixedUpdate () 
	{
    }










    public virtual void OnGroundTouched()
    {
        if (GetComponent<PhysicsCollisionController>().IsGrounded())
        {
            velocity.y = 0;
            curGravitationalForce = 0;
        }
    }
    public virtual void HandleVelocityStimulus() { }
    public virtual void HandleReactionAfterStimulus()
    {
        ApplyGravity();
        ApplyDeltaTime();
        ApplyLimitsToVelocity();
        CastRaysAndMaybeAlterkVelocity();
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }

    public virtual void OnDeath()
    {

    }






    //**********************
    //functions that will not be overriden
    //**********************
    public void SetPossibleVelocity(Vector2 _possibleVel)
    {
        velocity = _possibleVel;        
    }
    public void ApplyDeltaTime()
    {
        velocity.x *= Time.fixedDeltaTime;
    }
    public void ApplyGravity()
    {  
        curGravitationalForce += gravity * Time.fixedDeltaTime;
        velocity.y -= curGravitationalForce;        
    }
    public void CastRaysAndMaybeAlterkVelocity()
    {        
        //Call collision if needed
        GetComponent<PhysicsCollisionController>().CastRays(ref velocity);
    }
    public void ApplyLimitsToVelocity()
    {
        velocity.x = (Mathf.Abs(velocity.x) > 15 * Time.fixedDeltaTime) ? Mathf.Sign(velocity.x) * 15 * Time.fixedDeltaTime : velocity.x;
        velocity.y = (Mathf.Abs(velocity.y) > 10 * Time.fixedDeltaTime) ? Mathf.Sign(velocity.y) * 10f * Time.fixedDeltaTime : velocity.y;
    }
    public void ApplyVelocityToPosition()
    { 
        transform.Translate(velocity.x, velocity.y, 0);
    }

    public void ApplySpriteDirection()
    {        
        if (velocity.x > 0)
            obj_sprite.transform.localScale = new Vector2(-1, 1);
        if (velocity.x < 0)
            obj_sprite.transform.localScale = new Vector2(1, 1);
    }

    




    



}
