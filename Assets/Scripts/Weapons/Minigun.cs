using UnityEngine;
using System.Collections;


public class Minigun : Weapon {

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
        bullet.GetComponent<Bullet>().velocity = new Vector2(10, Random.Range(-0.2f, 0.75f));
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }







}