using UnityEngine;
using System.Collections;


public class GranadeLuncher : Weapon {


    public override void SpawnBullet()
    {
        CameraController.Shake(0.07f, 0.08f, 0.03f);
        for (int i = 0; i < 3; ++i)
        {
            GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().velocity = new Vector2(Random.Range(7.0f, 9.0f), Random.Range(0.3f, 2f));
            bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
        }
    }


}
