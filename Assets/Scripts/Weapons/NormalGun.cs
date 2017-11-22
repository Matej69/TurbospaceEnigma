using UnityEngine;
using System.Collections;


public class NormalGun : Weapon {
    
	void Awake()
	{
        base.Awake();
	}
	
	void Start () 
	{	
	}

	void Update () 
	{
        base.Update();
	}



    public override void SpawnBullet()
    {
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }







}
