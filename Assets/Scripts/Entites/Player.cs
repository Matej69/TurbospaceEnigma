using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BehaviourController))]
[RequireComponent(typeof(PhysicsCollisionController))]
public class Player : Entity
{
        
    CollisionController collisionController;


    void Awake()
	{
        Debug.Log("PLAYER awake called");
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
