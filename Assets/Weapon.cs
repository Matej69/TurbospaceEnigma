using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour {

    public enum E_WEAPON_TYPE { GUN, SHOTGUN, MINIGUN }
    public enum E_FIRE_TYPE { ON_HOLD, ON_TOUCH }
    public E_WEAPON_TYPE weaponType;
    public E_FIRE_TYPE fireType;

    private GameObject ownerSpriteObj;
    public GameObject pref_bullet;
    public float reloadTime;

    private GameObject bulletsSpawnObj;
    private Timer timer_automaticShooting;
    

    void Awake()
	{
        ownerSpriteObj = transform.parent.gameObject;
        bulletsSpawnObj = transform.FindChild("BulletSpawnPoint").gameObject;
        timer_automaticShooting = new Timer(reloadTime);
        timer_automaticShooting.currentTime = 0;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{
        HandleShoting();
	}



    void HandleShoting()
    {
        if (fireType == E_FIRE_TYPE.ON_TOUCH && Input.GetKeyDown(KeyCode.X))
        {
            SpawnBullet();
        }
        else if (fireType == E_FIRE_TYPE.ON_HOLD && timer_automaticShooting.IsFinished() && Input.GetKey(KeyCode.X))
        {
            SpawnBullet();
            timer_automaticShooting.Reset();
        }
    }   
    
    public virtual void SpawnBullet()
    {
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
    }




}
