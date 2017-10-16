using UnityEngine;
using System.Collections;


public class PhysicsCollisionController : MonoBehaviour {

    SpriteRenderer spriteRenderer;
    int rayCount = 10;
    float skin = 0.00025f;

    BehaviourController controller;


	void Awake()
	{
        spriteRenderer = transform.FindChild("Sprite").GetComponent<SpriteRenderer>();
        controller = GetComponent<BehaviourController>();
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
        if (velocity.x == 0)
            return;
        Vector2 dir = (velocity.x > 0) ? Vector2.right : Vector2.left;
        Vector2 startPos = (dir == Vector2.right) ? new Vector2(spriteRenderer.bounds.max.x - skin, spriteRenderer.bounds.min.y + skin) : new Vector2(spriteRenderer.bounds.min.x + skin, spriteRenderer.bounds.min.y + skin);
        float length = skin + velocity.x;
        float rayDist = (spriteRenderer.bounds.size.y - skin * 2) / rayCount;

        float shortestRayDistance = 0f;
        for (int i = 0; i <= rayCount; ++i)
        {
            Vector2 rayPos = new Vector2(startPos.x, startPos.y + rayDist * i);
            RaycastHit2D curRayHit = Physics2D.Raycast(rayPos, dir, length);
            //set initial shortest ray distance
            if (i == 0)
                shortestRayDistance = dir.x * length;
            if (curRayHit.collider != null && curRayHit.distance < shortestRayDistance)            
                shortestRayDistance = curRayHit.distance;
            //fix ray length so it's abs value is never less then value of skin(fixing float decimal rounding)
            float fixedRayLength = (float)System.Math.Round(shortestRayDistance, 5);
            velocity.x = ((fixedRayLength - skin) * dir.x);
            Debug.DrawRay(rayPos, dir * fixedRayLength, Color.yellow);




        }                               
    }




}
