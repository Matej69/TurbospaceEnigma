using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetScene : MonoBehaviour {

  private OnPlanetSpaceship onPlanetSpaceship;
  private GameObject onPlanetPlayer;

  Coroutine spaceshipLandingOnPlanet;
  Coroutine playerInSpaceshipRange;


  private void Awake() {
    onPlanetSpaceship = transform.Find("Spaceship").gameObject.GetComponent<OnPlanetSpaceship>();
    onPlanetPlayer = transform.Find("Player").gameObject;
  }

  private void OnEnable() {
    spaceshipLandingOnPlanet = StartCoroutine(SpaceshipLandingOnPlanet());
    playerInSpaceshipRange = StartCoroutine(PlayerInSpaceshipRange());
  }
  private void OnDisable() {
    if (spaceshipLandingOnPlanet != null)
      StopCoroutine(spaceshipLandingOnPlanet);
    if (playerInSpaceshipRange != null)
      StopCoroutine(playerInSpaceshipRange);
  }

  IEnumerator SpaceshipLandingOnPlanet() {
    onPlanetSpaceship.transform.position = new Vector2(10, 20);
    onPlanetSpaceship.surfaceReached = false;
    onPlanetPlayer.transform.position = new Vector2(10, 22);
    onPlanetPlayer.SetActive(false);
    onPlanetPlayer.transform.SetParent(onPlanetSpaceship.transform);
    while (!onPlanetSpaceship.surfaceReached) {
      onPlanetSpaceship.TravelTowardsPlanetSurface();
      yield return null;
    }
    
    onPlanetPlayer.transform.SetParent(gameObject.transform);
    onPlanetPlayer.SetActive(true);
    EventManager.event_spaceshipLanded.Invoke();
    yield return null;
  }

  IEnumerator PlayerInSpaceshipRange() {
    bool wasPlayerInRangeLastFrame = false;
    bool isPlayerInRange = false;
    float inRangeRadius = 3f;
    while (true) {
      isPlayerInRange = Vector2.Distance(onPlanetPlayer.transform.position, onPlanetSpaceship.transform.position) < inRangeRadius;
      if (isPlayerInRange && !wasPlayerInRangeLastFrame)
        EventManager.event_playerInSpaceshipRange.Invoke(true);
      else if (!isPlayerInRange && wasPlayerInRangeLastFrame)
        EventManager.event_playerInSpaceshipRange.Invoke(false);
      wasPlayerInRangeLastFrame = isPlayerInRange;
      yield return null;
    }
  }



}
