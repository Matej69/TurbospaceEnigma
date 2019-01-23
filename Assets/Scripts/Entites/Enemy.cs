using UnityEngine;
using System.Collections;


public class Enemy : Entity
{
    public int health = 1;

    public Color colorHit = new Color(1, 0, 0, 1);
    Timer colorHitTimer = new Timer(0.05f);
    


    void Awake()
	{
        base.Awake();
    }
	
	void Start () 
	{
        /*     
        animationManager.SetAnimation("Walk");
        StartCoroutine(animationManager.HandleAnimationUpdate());
        */
        obj_sprite.GetComponent<SpriteRenderer>().material.SetColor("_HitColor", colorHit);
    }

	void Update () 
	{
        HandleSpriteHitColorTime();
    }

    public void HandleSpriteHitColorTime()
    {
        colorHitTimer.Tick(Time.deltaTime);
        if(colorHitTimer.IsFinished())
        {
            obj_sprite.GetComponent<SpriteRenderer>().material.SetInt("_HitEffectActive", 0);
        }

    }
    

    public void OnBulletHit(int _dmg)
    {
        health -= _dmg;
        obj_sprite.GetComponent<SpriteRenderer>().material.SetInt("_HitEffectActive", 1);
        colorHitTimer.Reset();
    }


    



}
