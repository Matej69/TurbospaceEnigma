using UnityEngine;
using System.Collections;


public class Shotgun : Weapon {

    public int numOfBullets = 5;


    public override void SpawnBullet()
    {
        CameraController.Shake(0.28f, 0.1f, 0.08f);
        for (int i = 0; i < numOfBullets; ++i)
        {
            GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().velocity = new Vector2(Random.Range(7f, 12f), Random.Range(-0.8f, 2f));
            bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
        }
    }

}
