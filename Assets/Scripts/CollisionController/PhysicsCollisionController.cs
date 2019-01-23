using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BehaviourController))]
[RequireComponent(typeof(Entity))]
public class PhysicsCollisionController : MonoBehaviour
{
    public enum E_RAY_HIT_SIDE { LEFT, RIGHT, TOP, BOTTOM, NONE }
    public class RayHitSide
    {
        public E_RAY_HIT_SIDE hor;
        public E_RAY_HIT_SIDE ver;
    }

    SpriteRenderer spriteRenderer;
    int rayCount = 10;
    float skin = 0.00025f;

    bool isGrounded = false;

    BehaviourController controller;


	void Awake()
	{
        controller = GetComponent<BehaviourController>();
    }
	
	void Start () 
	{
        spriteRenderer = GetComponent<Entity>().spriteRenderer;
    }

	void Update () 
	{	
	}


    public RayHitSide CastRays(ref Vector2 velocity)
    {
        RayHitSide rayHitSide = new RayHitSide();
        rayHitSide.ver = CastVerticalRay(ref velocity);
        rayHitSide.hor = CastHorizontalRay(ref velocity);
        return rayHitSide;       
    }




    private E_RAY_HIT_SIDE CastHorizontalRay(ref Vector2 velocity)
    {
        if (velocity.x == 0)
            return E_RAY_HIT_SIDE.NONE;
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
            curRayHit = Physics2D.Raycast(rayPos, dir, rayLength, GlobalInformation.instance.mask_platform); 
            
            if (i == 0)
            {
                //set initial shortest ray distance
                shortestRayDistance = rayLength;
                //if the shortest ray is hitting edge that is not vertical or nothing(normal x != -1,0-1) :: ray with index 0 is also the bottom ray that is checking against slopes :: we will move it up for the same length that would be inside collider
                if (curRayHit.normal.x != -1 && curRayHit.normal.x != 0 && curRayHit.normal.x != 1)
                {
                    if (curRayHit.collider != null)
                    {
                        float angleInRadians = (90 - Vector2.Angle(curRayHit.normal, velocity)) * (Mathf.PI / 180);
                        float yDistanceToMove = ( Mathf.Abs(Mathf.Tan(angleInRadians)) * (Mathf.Abs(velocity.x) - curRayHit.distance )) ;
                        velocity.y = yDistanceToMove;              
                        isGrounded = true;
                        return (dir == Vector2.right) ? E_RAY_HIT_SIDE.RIGHT : E_RAY_HIT_SIDE.LEFT;
                    } 
                }   
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
            return (dir == Vector2.right) ? E_RAY_HIT_SIDE.RIGHT : E_RAY_HIT_SIDE.LEFT;
        }

        //if it gets to here, then there was no ray hit
        return E_RAY_HIT_SIDE.NONE;

    }



    private E_RAY_HIT_SIDE CastVerticalRay(ref Vector2 velocity)
    {
        if (velocity.y == 0)
            return E_RAY_HIT_SIDE.NONE;
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
            curRayHit = Physics2D.Raycast(rayPos, dir, rayLength, GlobalInformation.instance.mask_platform);
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
                return E_RAY_HIT_SIDE.BOTTOM;
            }
            else if (dir == Vector2.up)
            {
                velocity.y = -1 * (velocity.y / 5f);
                return E_RAY_HIT_SIDE.TOP;
            }
        }

        //if it gets to here, then there was no ray hit
        return E_RAY_HIT_SIDE.NONE;

    }

    public bool IsGrounded() { return isGrounded; }




}
