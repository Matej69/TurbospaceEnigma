using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetSpaceship : MonoBehaviour {

  public LayerMask surfaceMask;
  private Vector2 velocity = new Vector2(0, -10);
  [HideInInspector]
  public bool surfaceReached = false;


  public void TravelTowardsPlanetSurface() {
    Vector2 dtVelocity = velocity * Time.deltaTime;
    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Abs(dtVelocity.y), surfaceMask);
    if (hit)
      surfaceReached = true;
    else
      transform.Translate(dtVelocity, Space.World);
  }


}
