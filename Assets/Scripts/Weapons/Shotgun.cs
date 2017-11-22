using UnityEngine;
using System.Collections;


public class Shotgun : Weapon {

    public int numOfBullets = 5;


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
        for (int i = 0; i < numOfBullets; ++i)
        {
            GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().velocity = new Vector2(Random.Range(7f, 12f), Random.Range(-0.8f, 2f));
            bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
        }
    }

}
