using UnityEngine;
using System.Collections;


public class PhysicsCollisionController : CollisionController {

    SpriteRenderer spriteRenderer;
    int rayCount = 10;
    float skin = 0.00025f;

    bool isGrounded = false;

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


    public void CastRays(ref Vector2 velocity)
    {
        CastVerticalRay(ref velocity);
        CastHorizontalRay(ref velocity);
    }




    private void CastHorizontalRay(ref Vector2 velocity)
    {
        if (velocity.x == 0)
            return;
        Vector2 dir = (velocity.x > 0) ? Vector2.right : Vector2.left;
        Vector2 startPos = (dir == Vector2.right) ? new Vector2(spriteRenderer.bounds.max.x - skin, spriteRenderer.bounds.min.y + skin) : new Vector2(spriteRenderer.bounds.min.x + skin, spriteRenderer.bounds.min.y + skin);
        float rayLength = skin + Mathf.Abs(velocity.x);
        float rayDist = (spriteRenderer.bounds.size.y - skin * 2) / rayCount;

        float shortestRayDistance = 0f;
        bool atLeastOneRayHit = false;
        RaycastHit2D curRayHit;
        for (int i = 0; i <= rayCount; ++i)
        {            
            Vector2 rayPos = new Vector2(startPos.x, startPos.y + rayDist * i);
            curRayHit = Physics2D.Raycast(rayPos, dir, rayLength);
            
            if (i == 0)
            {
                //set initial shortest ray distance
                shortestRayDistance = rayLength;
                //if the shortest ray is hitting edge that is not vertical or nothing(normal x != -1,0-1) :: ray with index 0 is also the bottom ray that is checking against slopes :: we will move it up for the same length that would be inside collider
                if (curRayHit.normal.x != -1 && curRayHit.normal.x != 0 && curRayHit.normal.x != 1)
                    velocity.y += Mathf.Abs(velocity.x) - curRayHit.distance;                    
            }
            if (curRayHit.collider != null && curRayHit.distance <= shortestRayDistance)
            {
                atLeastOneRayHit = true;
                shortestRayDistance = curRayHit.distance;
            }
        }

        //apply horizontal velocity
        if (atLeastOneRayHit)
        {
            //fix ray length so it's abs value is never less then value of skin(fixing float decimal rounding)
            float fixedRayLength = (float)System.Math.Round(shortestRayDistance, 5);
            velocity.x = ((fixedRayLength - skin) * dir.x);            
        }
        
        
                                     
    }



    private void CastVerticalRay(ref Vector2 velocity)
    {
        if (velocity.y == 0)
            return;
        Vector2 dir = (velocity.y > 0) ? Vector2.up : Vector2.down;
        Vector2 startPos = (dir == Vector2.up) ? new Vector2(spriteRenderer.bounds.min.x + skin, spriteRenderer.bounds.max.y - skin) : new Vector2(spriteRenderer.bounds.min.x + skin, spriteRenderer.bounds.min.y + skin);
        float rayLength = skin + Mathf.Abs(velocity.y);
        float rayDist = (spriteRenderer.bounds.size.x - skin * 2) / rayCount;

        isGrounded = false;
        float shortestRayDistance = 0f;
        bool atLeastOneRayHit = false;
        RaycastHit2D curRayHit;
        //find shortest ray that hit
        for (int i = 0; i <= rayCount; ++i)
        {
            Vector2 rayPos = new Vector2(startPos.x + rayDist * i, startPos.y);
            curRayHit = Physics2D.Raycast(rayPos, dir, rayLength);
            //set initial shortest ray distance
            if (i == 0)
                shortestRayDistance = rayLength;

            if (curRayHit.collider != null && curRayHit.distance <= shortestRayDistance)
            {
                atLeastOneRayHit = true;
                shortestRayDistance = curRayHit.distance;
            }       
        }
        //apply changes to velocity from shortest hit ray
        if (atLeastOneRayHit)
        {
            if (dir == Vector2.down)
            {                             
                isGrounded = true;
                //fix ray length so it's abs value is never less then value of skin(fixing float decimal rounding)
                float fixedRayLength = (float)System.Math.Round(shortestRayDistance, 5);
                velocity.y = ((fixedRayLength - skin) * dir.y);
            }
            else if (dir == Vector2.up)
            {
                velocity.y = -1 * (velocity.y / 5f);
                return;
            }
        }

    }

    public bool IsGrounded() { return isGrounded; }




}
