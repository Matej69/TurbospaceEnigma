using UnityEngine;
using System.Collections;


public class BehaviourController : MonoBehaviour {

    Vector2 velocity;
    float curGravitationalForce = 0.1f;
    GameObject obj_sprite;
    public float walkSpeed;
    public float runSpeed;
    public float gravity;
    public float jumpForce;

    void Awake()
	{
        obj_sprite = transform.FindChild("Sprite").gameObject;
    }
	
	void Start () 
	{
        SetPossibleVelocity(new Vector2(1f, 0));
    }

	void FixedUpdate () 
	{
        //On ground touched
        if (GetComponent<PhysicsCollisionController>().IsGrounded())
        {
            velocity.y = 0;
            curGravitationalForce = 0;
        }
        

        
        if (Input.GetKey(KeyCode.W) && GetComponent<PhysicsCollisionController>().IsGrounded())
            SetPossibleVelocity(new Vector2(velocity.x, velocity.y + jumpForce));

        float xSpeed = (Input.GetKey(KeyCode.LeftShift)) ? runSpeed : walkSpeed;
        if (Input.GetKey(KeyCode.A))
            SetPossibleVelocity(new Vector2(velocity.x - xSpeed, velocity.y));
        else if (Input.GetKey(KeyCode.D))
            SetPossibleVelocity(new Vector2(velocity.x + xSpeed, velocity.y));
        else 
            SetPossibleVelocity(new Vector2(0, velocity.y));
        
        
                
        ApplyGravity();
        ApplyDeltaTime();
        ApplyLimitsToVelocity();
        CastRaysAndMaybeAlterkVelocity();             
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }

    
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



    public virtual void OnDeath()
    {

    }



}
