using UnityEngine;

public interface IBullet {

  void SetBulletDirection(int horDirX);
  void OnEnemyTouch();
  void HandleMovement();

}
