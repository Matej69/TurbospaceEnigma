using UnityEngine;
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
    public override void HandleAnimation()
    {
        if (velocity.x > 0 || velocity.x < 0)
            GetComponent<Entity>().animationManager.SetAnimation("Walk");
        //else
        //    GetComponent<Entity>().animationManager.SetAnimation("Idle");
    }


}
