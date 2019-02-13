using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTravelerBehaviour : MonoBehaviour {

    public enum e_travelState { TRAVELING, NOT_TRAVELING }
    e_travelState travelState;

  [HideInInspector]
  float maxTravelSpeed;
  float timeOfMovementStart;
  float timeToReachMaxSpeed;
  float stopMovementTargetRadius;
  float slownDownMovementRadius;
  
  class TargetPlanetStatus {
    public Planet targetPlanet = null;
    public bool planetReached = false;
  }
  TargetPlanetStatus targetPlanetStatus = new TargetPlanetStatus();


    // Use this for initialization 
    void Start () {
    EventManager.event_planetClicked.AddListener(OnPlanetClicked);

    Camera.main.GetComponent<CameraController>().SetGameObjectToFollow(gameObject);
    travelState = e_travelState.NOT_TRAVELING;        
        maxTravelSpeed = 5f;
        timeOfMovementStart = Time.time;
        timeToReachMaxSpeed = 1.3f;
        stopMovementTargetRadius = 0.25f;
        slownDownMovementRadius = 2.2f;

    }
  

	// Update is called once per frame
	void Update () {
	}
  
  
  void TravelTowardsTarget() {
    StartCoroutine(TravelTowardsTargetCoroutine());
  }

  IEnumerator TravelTowardsTargetCoroutine() {
    Vector2 unitVecToDestination;
    float targetRotAngle;
    Quaternion targetRotation;
    float rotSpeed = 1;
    float travelSpeed = 1;
    float travelSpeedPercent = 0;
    Vector2 planetPos = targetPlanetStatus.targetPlanet.transform.position;
    // Keep rotating towards planet and move in direction of spacehip local up axis
    while (Vector2.Distance(transform.position, planetPos) > Planet.radius) {
      unitVecToDestination = (planetPos - (Vector2)transform.position).normalized;
      targetRotAngle = (planetPos.x < transform.position.x) ?
                          Vector2.Angle(Vector2.up, unitVecToDestination) :
                          360 - Vector2.Angle(Vector2.up, unitVecToDestination);
      targetRotation = Quaternion.Euler(new Vector3(0, 0, targetRotAngle));
      // Rotate
      rotSpeed += 2f * Time.deltaTime;
      transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotSpeed);
      // Move spaceship forward in y direction (move up)
      travelSpeedPercent += Time.deltaTime;
      travelSpeed = Mathf.Lerp(0, 3.7f, travelSpeedPercent);
      transform.Translate(Vector2.up * travelSpeed * Time.deltaTime);
      yield return null;
    }
    travelState = e_travelState.NOT_TRAVELING;
    targetPlanetStatus.planetReached = true;
    EventManager.event_travelingStateChange.Invoke(e_travelState.NOT_TRAVELING);
  }

  void OnPlanetClicked(Planet planet) {
    // If planet is clicked(second time) after spaceship has reached it, change to 'on planet' scene
    if (planet == targetPlanetStatus.targetPlanet && travelState != e_travelState.TRAVELING && targetPlanetStatus.planetReached) {
      EventManager.event_planetClickedWithSpaceshipOnIt.Invoke();
    }
    // Set clicked planet as targeted planet and start traveling towards it
    else if (travelState == e_travelState.NOT_TRAVELING) {
      targetPlanetStatus.targetPlanet = planet;
      travelState = e_travelState.TRAVELING;
      EventManager.event_travelingStateChange.Invoke(e_travelState.TRAVELING);
      TravelTowardsTarget();
    }
  }


}





