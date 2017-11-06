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
    /*
    [HideInInspector]
    public BehaviourController behaviourController;    
    [HideInInspector]
    public CollisionController collisionController;
    */

    [HideInInspector]
    public GameObject obj_sprite;

    public void Awake()
	{
        animationManager = GetComponent<AnimationManager>();
        /*
        behaviourController = GetComponent<BehaviourController>();
        collisionController = GetComponent<CollisionController>();
        */    
        obj_sprite = transform.FindChild("Sprite").gameObject;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}



    






}
