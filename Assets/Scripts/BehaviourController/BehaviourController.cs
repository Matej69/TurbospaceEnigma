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
    }

	void Update () 
	{
        SetPossibleVelocity(new Vector2(98f, 0));
        TryCorrectVelocity();        
        ApplyVelocityToPosition();
        ApplySpriteDirection();
    }

    
    public void SetPossibleVelocity(Vector2 _possibleVel)
    {
        velocity = _possibleVel;
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
