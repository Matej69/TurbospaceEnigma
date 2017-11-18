using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {

    Vector2 velocity;
    public float gravity = 1f;


	void Awake()
	{
        SetInitialVelocity();
    }
	
	void Start () 
	{	
	}

	void Update () 
	{
        HandleBehaviour();
    }



    
    public void SetBulletDirection(int horDirX)
    {
        velocity.x = (horDirX == 1) ? -Mathf.Abs(velocity.x) : Mathf.Abs(velocity.x);
    }



    /*
    ** ALWAYS FROM LEFT TO RIGHT   
    */
    protected virtual void SetInitialVelocity()
    {
        velocity = Vector2.right * 8;
    }

    protected virtual void OnEnemyTouch()
    {
    }

    protected virtual void HandleBehaviour()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }





}
