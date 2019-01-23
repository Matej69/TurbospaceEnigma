using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Entity))]
public class BehaviourController : MonoBehaviour {

    public Vector2 maxVelocity = new Vector2(15, 10);
    public float curGravitationalForce = 0.1f;
    GameObject obj_sprite;
    public Vector2 velocity;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;
    public float jumpForce;

    [HideInInspector]
    public Vector2 xFacingDir = Vector2.left;


    public void Awake()
	{   
    }
	
	public void Start () 
	{
        obj_sprite = GetComponent<Entity>().obj_sprite;
    }

    public void FixedUpdate () 
	{        
        HandleBehaviour();
    }








    public void HandleBehaviour()
    {
        OnGroundTouched();
        HandleVelocityStimulus();
        HandleReactionAfterStimulus();
        HandleAnimation();
    }

    //**********************
    //functions to be overriden
    //**********************    
    public virtual void OnGroundTouched() { }
    public virtual void HandleVelocityStimulus() { }
    public virtual void HandleReactionAfterStimulus() { }
    public virtual void HandleAnimation() { }
    public virtual void OnDeath() { }






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
    public void ApplyLimitsToVelocity()
    {
        Vector2 velSign = new Vector2(Mathf.Sign(velocity.x), Mathf.Sign(velocity.y));
        Vector2 velAbs = new Vector2(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));   
        velocity.x = (velAbs.x > maxVelocity.x * Time.fixedDeltaTime) ? velSign.x * maxVelocity.x * Time.fixedDeltaTime : velocity.x;
        velocity.y = (velAbs.y > maxVelocity.y * Time.fixedDeltaTime) ? velSign.y * maxVelocity.y * Time.fixedDeltaTime : velocity.y;
    }
    public void ApplyVelocityToPosition()
    { 
        transform.Translate(velocity.x, velocity.y, 0);
    }

    public void ApplySpriteDirection()
    {        
        if (xFacingDir == Vector2.right)
            obj_sprite.transform.localScale = new Vector2(-1, 1);
        if (xFacingDir == Vector2.left)
            obj_sprite.transform.localScale = new Vector2(1, 1);
    }

    




    



}
