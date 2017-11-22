using UnityEngine;
using System.Collections;


public class GranadeBullet : Bullet {


    public GameObject pref_explosion;


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
    
    public override void OnEnemyTouch()
    {
        Instantiate(pref_explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);        
    }






}
