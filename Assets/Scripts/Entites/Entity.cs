using UnityEngine;
using System.Collections;

/*
** Only objects that inherit this class can be created
** behaviourController is required in schildreen becouse there are multiple types of that class(children)
*/
[RequireComponent(typeof(BehaviourController))]
[RequireComponent(typeof(AnimationManager))]
public class Entity : MonoBehaviour {

  enum E_TYPE { PLAYER, ENEMY, ITEM }    

  [HideInInspector]
  public BehaviourController behaviourController;
  [HideInInspector]
  public AnimationManager animationManager;
  [HideInInspector]
  public GameObject obj_sprite;
  [HideInInspector]
  public SpriteRenderer spriteRenderer;

  public void Awake() {
    behaviourController = GetComponent<BehaviourController>();
    animationManager = GetComponent<AnimationManager>();  
    obj_sprite = transform.Find("Sprite").gameObject;
    spriteRenderer = obj_sprite.GetComponent<SpriteRenderer>();
  }
  

}
