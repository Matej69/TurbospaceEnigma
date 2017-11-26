using UnityEngine;
using System.Collections;


public class SpaceGun : Weapon {

    
    public override void SpawnBullet()
    {
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().velocity = new Vector2(10, 0);
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }


}
