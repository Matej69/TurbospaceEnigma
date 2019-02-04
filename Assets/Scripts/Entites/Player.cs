using UnityEngine;
using System.Collections;



[RequireComponent(typeof(SlopeController))]
[RequireComponent(typeof(Weapon))]
public class Player : Entity
{

  SlopeController collisionController;

  void Awake() {
      base.Awake();
  }
	
	void Start () {
    animationManager.SetAnimation("Walk");
    StartCoroutine(animationManager.HandleAnimationUpdate());
  }

}
