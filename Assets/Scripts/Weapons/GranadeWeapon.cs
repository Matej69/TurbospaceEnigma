using UnityEngine;
using System.Collections;


public class GranadeWeapon : Weapon {
    

    void Awake()
    {
        base.Awake();
    }

    void Start()
    {
    }

    void Update()
    {
        base.Update();
    }



    public override void SpawnBullet()
    {
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().velocity = new Vector2(5f,1.55f);
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }
    
}
