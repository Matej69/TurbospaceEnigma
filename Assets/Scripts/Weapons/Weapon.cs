using UnityEngine;
using System.Collections;
using System;


public class Weapon : MonoBehaviour {

  [Serializable]
  public struct MinMaxPair
  {
    public float min, max;
  }
  
    public enum E_WEAPON_TYPE { GUN, SHOTGUN, MINIGUN, GRANADE, UZI, SPACEGUN, BFG, GRANADE_LUNCHER }
    public enum E_FIRE_TYPE { ON_HOLD, ON_TOUCH }
    public E_WEAPON_TYPE weaponType;
    public E_FIRE_TYPE fireType;

    protected GameObject ownerSpriteObj;
    public GameObject pref_bullet;
    public float reloadTime;
    [Space(10)]
    public int numOfBullets;
    public MinMaxPair xRandVelocity;
    public MinMaxPair yRandVelocity;
    [Space(10)]
    public float shakeMagnitude;
    public float shakeTime;
    public float shakeEveryXSec;


    protected GameObject bulletsSpawnObj;
    protected Timer timer_automaticShooting;
    

    public void Awake()
	{
        ownerSpriteObj = transform.parent.gameObject;
        bulletsSpawnObj = transform.Find("BulletSpawnPoint").gameObject;
        timer_automaticShooting = new Timer(reloadTime);
        timer_automaticShooting.currentTime = 0;
    }
	
	void Start () 
	{	
	}

	public void Update () 
	{
        HandleShoting();
	}



    void HandleShoting()
    {
        timer_automaticShooting.Tick(Time.deltaTime);
        if (fireType == E_FIRE_TYPE.ON_TOUCH && Input.GetKeyDown(KeyCode.X) && timer_automaticShooting.IsFinished())
        {
            SpawnBullet();
            timer_automaticShooting.Reset();
        }
        else if (fireType == E_FIRE_TYPE.ON_HOLD && timer_automaticShooting.IsFinished() && Input.GetKey(KeyCode.X))
        {
            SpawnBullet();
            timer_automaticShooting.Reset();
        }
    }   
    
    public virtual void SpawnBullet()
    {
      CameraController.Shake(shakeMagnitude, shakeTime, shakeEveryXSec);
      for(int i = 0; i < numOfBullets; ++i) { 
        GameObject bullet = (GameObject)Instantiate(pref_bullet, bulletsSpawnObj.transform.position, Quaternion.identity);
        bullet.GetComponent<Bullet>().velocity = new Vector2(UnityEngine.Random.Range(xRandVelocity.min, xRandVelocity.max), UnityEngine.Random.Range(yRandVelocity.min, yRandVelocity.max));
        bullet.GetComponent<Bullet>().SetBulletDirection((int)ownerSpriteObj.transform.localScale.x);
      }
  }




}
