using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTravelerBehaviour : MonoBehaviour {

    public enum e_travelState { TRAVELING, NOT_TRAVELING }
    e_travelState travelState;

  [HideInInspector]
    public Vector2 pointToTravelTo;
    float maxTravelSpeed;
    float timeOfMovementStart;
    float timeToReachMaxSpeed;
    float stopMovementTargetRadius;
    float slownDownMovementRadius;

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
    // Keep rotating towards planet and move in direction of spacehip local up axis
    while (Vector2.Distance(transform.position, pointToTravelTo) > Planet.radius) {
      unitVecToDestination = (pointToTravelTo - (Vector2)transform.position).normalized;
      targetRotAngle = (pointToTravelTo.x < transform.position.x) ?
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
    EventManager.event_travelingStateChange.Invoke(e_travelState.NOT_TRAVELING);
    SceneManager.ChangeTo(SceneManager.e_sceneID.ON_PLANET);
  }

  void OnPlanetClicked(Vector2 pos) {
    if (!gameObject.activeInHierarchy)
      return;
    if (travelState == e_travelState.NOT_TRAVELING) {
      pointToTravelTo = (travelState == e_travelState.NOT_TRAVELING) ? pos : (Vector2)transform.position;
      travelState = e_travelState.TRAVELING;
      EventManager.event_travelingStateChange.Invoke(e_travelState.TRAVELING);
      TravelTowardsTarget();
      
    }
  }


}





