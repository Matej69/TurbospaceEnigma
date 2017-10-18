using UnityEngine;
using System.Collections;


public class BehaviourController : MonoBehaviour {

    Vector2 velocity;
    GameObject obj_sprite;

    void Awake()
	{
        obj_sprite = transform.FindChild("Sprite").gameObject;
    }
	
	void Start () 
	{
        SetPossibleVelocity(new Vector2(1f, 0));
    }

	void Update () 
	{
        if (Input.GetKeyDown(KeyCode.W))
            SetPossibleVelocity(new Vector2(velocity.x, velocity.y + 60));
        if (Input.GetKey(KeyCode.A))
            SetPossibleVelocity(new Vector2(velocity.x - 3, velocity.y));
        else if (Input.GetKey(KeyCode.D))
            SetPossibleVelocity(new Vector2(velocity.x + 3, velocity.y));
        else 
            SetPossibleVelocity(new Vector2(0, velocity.y));


        ApplyDeltaTime();
        Global.ApplyGravity(ref velocity);        
        TryCorrectVelocity();        
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }

    
    public void SetPossibleVelocity(Vector2 _possibleVel)
    {
        velocity = _possibleVel;        
    }
    public void ApplyDeltaTime()
    {
        velocity *= Time.deltaTime;
    }
    public void TryCorrectVelocity()
    {        
        //Call collision if needed
        GetComponent<PhysicsCollisionController>().AlterPosition(ref velocity);
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
