using UnityEngine;
using System.Collections;


public class Explosion : MonoBehaviour {


    public Sprite spr_whiteExplosion;
    public Sprite spr_blackxplosion;
        
    public float minScale = 1;
    public float changeToBlackScale = 1.85f;
    public float maxScale = 2;
    public float scaleIncreaseSpeed = 1;

    private SpriteRenderer spriteRend;
    private Transform spriteTrans;



    void Awake()
	{
        spriteRend = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
        spriteTrans = transform.FindChild("Sprite").transform;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{
        HandleExplosionIncrease();
    }


    void HandleExplosionIncrease()
    {
        spriteTrans.localScale = new Vector2(spriteTrans.localScale.x + scaleIncreaseSpeed * Time.deltaTime, spriteTrans.localScale.y + scaleIncreaseSpeed * Time.deltaTime);
        if (spriteTrans.localScale.x > changeToBlackScale && spriteTrans.localScale.x < maxScale)
            spriteRend.sprite = spr_blackxplosion;
        else if(spriteTrans.localScale.x > maxScale)
        {
            OnExplosionEnd();
            Destroy(gameObject);
        }


    }

    void OnExplosionEnd()
    {

    }






}
