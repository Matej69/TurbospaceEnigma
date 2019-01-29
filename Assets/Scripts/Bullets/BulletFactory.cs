using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory {

  static private BulletFactory ref_this;

  public enum E_BULLET_TYPE { LINEAR, GRANADE }

  static public BulletFactory Get() {
    return (ref_this != null) ? ref_this : ref_this = new BulletFactory();
  }

  public void Create(E_BULLET_TYPE type, GameObject prefab, Vector3 pos, Vector2 velocity, int dir)
  {
    GameObject bullet = GameObject.Instantiate(prefab, pos, Quaternion.identity);
    if (type == E_BULLET_TYPE.LINEAR) {
      bullet.GetComponent<LinearBullet>().velocity = velocity;
      bullet.GetComponent<LinearBullet>().SetBulletDirection(dir);
    }
    else if (type == E_BULLET_TYPE.GRANADE) {
      bullet.GetComponent<GranadeBullet>().velocity = velocity;
      bullet.GetComponent<GranadeBullet>().SetBulletDirection(dir);
    }

  }
}
