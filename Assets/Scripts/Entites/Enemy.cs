using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BehaviourController))]
public class Enemy : Entity
{
         

    void Awake()
	{
        base.Awake();
    }
	
	void Start () 
	{   
        /*     
        animationManager.SetAnimation("Walk");
        StartCoroutine(animationManager.HandleAnimationUpdate());
        */
    }

	void Update () 
	{	
	}
}
