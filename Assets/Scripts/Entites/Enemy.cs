using UnityEngine;
using System.Collections;


[RequireComponent(typeof(BehaviourController))]
public class Enemy : Entity
{
    public int health = 1;
    float onHitColorChangeSpeed = 2f;
    Color color_normal = new Color(1, 1, 1, 1);
    Color color_hit = new Color(1, 0, 0, 1);


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
        StartCoroutine(HandleOnHitColorNormalizing());
    }

	void Update () 
	{
    }



    private IEnumerator HandleOnHitColorNormalizing()
    {
        SpriteRenderer spriteRenderer = obj_sprite.GetComponent<SpriteRenderer>();       
        bool isNeverDoneFlag = true;
        while (isNeverDoneFlag)
        {
            Color color = spriteRenderer.color;
            //is there any need to do any work?, since g&b need to be one we are checking only one
            if (color.b == 1)
                yield return null;
            else
            {
                color.g = color.b = color.b + (onHitColorChangeSpeed * Time.deltaTime);
                color.g = color.b = (color.g > 1) ? 1 : color.g;
                spriteRenderer.color = color;
            }
            yield return null;
        }
    }

    public void OnBulletHit(int _dmg)
    {
        health -= _dmg;
        obj_sprite.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);
    }


    



}
