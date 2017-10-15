using UnityEngine;
using System.Collections;


public class PhysicsCollisionController : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    int rayCount = 10;
    float skin = 0.00025f;


	void Awake()
	{
        spriteRenderer = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}


    public void AlterPosition(ref Vector2 velocity)
    {
        CastHorizontalRay(ref velocity);
    }


    private void CastHorizontalRay(ref Vector2 velocity)
    {
        Vector2 dir = (velocity.x > 0) ? Vector2.right : Vector2.left;
        Vector2 startPos = (dir == Vector2.right) ? new Vector2(spriteRenderer.bounds.max.x - skin, spriteRenderer.bounds.min.y + skin) : new Vector2(spriteRenderer.bounds.min.x + skin, spriteRenderer.bounds.min.y + skin);
        float rayDist = (spriteRenderer.bounds.size.y - skin * 2) / rayCount;

        RaycastHit2D rayInfo;
        for (int i = 0; i <= rayCount; ++i)
        {
            Vector2 rayPos = new Vector2(startPos.x, startPos.y + rayDist * i);
            rayInfo = Physics2D.Raycast(rayPos, dir, velocity.x);
            //Debug.DrawRay(rayPos, dir * velocity.x, Color.yellow);
        }                               
    }




}
