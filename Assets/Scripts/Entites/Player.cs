using UnityEngine;
using System.Collections;



[RequireComponent(typeof(PhysicsCollisionController))]
[RequireComponent(typeof(Weapon))]
public class Player : Entity
{

    PhysicsCollisionController collisionController;
    Weapon weapon;

    void Awake()
	{
        base.Awake();
        collisionController = GetComponent<PhysicsCollisionController>();
        weapon = transform.FindChild("Sprite").FindChild("Weapon").GetComponent<Weapon>();
    }
	
	void Start () 
	{
        animationManager.SetAnimation("Walk");
        StartCoroutine(animationManager.HandleAnimationUpdate());
    }

	void Update () 
	{	
	}
}
