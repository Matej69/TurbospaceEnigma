using UnityEngine;
using System.Collections;


public class Minigun : Weapon {
    

    public override void SpawnBullet()
    {
        CameraController.Shake(0.2f, 0.05f, 0.012f);
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().velocity = new Vector2(10, Random.Range(-0.2f, 0.75f));
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }







}