using UnityEngine;
using System.Collections;


public class Uzi : Weapon {
    
    


    public override void SpawnBullet()
    {
        CameraController.Shake(0.08f, 0.1f, 0.03f);
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().velocity = new Vector2(10, Random.Range(-1f, 3.7f));
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }



}
	
