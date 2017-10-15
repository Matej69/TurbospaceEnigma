using UnityEngine;
using System.Collections;

/*
** Only objects that inherit this class can be created
** behaviourController is required in schildreen becouse there are multiple types of that class(children)
*/
[RequireComponent(typeof(AnimationManager))]
public class Entity : MonoBehaviour {

    enum E_TYPE { CHARACTER, ENEMY }

    [HideInInspector]
    public AnimationManager animationManager;
    [HideInInspector]
    public BehaviourController behaviourController;
    [HideInInspector]
    public CollisionController collisionController;

    public void Awake()
	{
        animationManager = GetComponent<AnimationManager>();
        behaviourController = GetComponent<BehaviourController>();
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}



    






}
