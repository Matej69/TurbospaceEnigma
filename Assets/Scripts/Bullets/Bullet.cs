using UnityEngine;
using System.Collections;


public class Bullet : MonoBehaviour {

    [HideInInspector]
    public Vector2 velocity;
    public float gravity = 0f;


	public void Awake()
	{
    }
	
	void Start () 
	{	
	}

    public void Update() 
	{
        HandleBehaviour();
    }



    
    public void SetBulletDirection(int horDirX)
    {
        velocity.x = (horDirX == 1) ? -Mathf.Abs(velocity.x) : Mathf.Abs(velocity.x);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((GlobalInformation.instance.mask_enemy & 1 << other.gameObject.layer) == (1 << other.gameObject.layer))
        {
            other.transform.parent.GetComponent<Enemy>().OnBulletHit(0);
            OnEnemyTouch();
        }
            
    }


    

    public virtual void OnEnemyTouch()
    {
        Destroy(gameObject);
    }

    public virtual void HandleBehaviour()
    {
        velocity.y -= gravity * Time.deltaTime;
        transform.Translate(velocity * Time.deltaTime);
    }





}
