using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PlayerBehaviourController))]
[RequireComponent(typeof(PhysicsCollisionController))]
public class Player : Entity
{
        
    CollisionController collisionController;


    void Awake()
	{
        base.Awake();
        collisionController = GetComponent<CollisionController>();
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}
}
