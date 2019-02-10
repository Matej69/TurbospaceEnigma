using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetScene : MonoBehaviour {

  private OnPlanetSpaceship onPlanetSpaceship;
  private GameObject onPlanetPlayer;

  Coroutine spaceshipLandingOnPlanet;

  private void Awake() {
    onPlanetSpaceship = transform.Find("Spaceship").gameObject.GetComponent<OnPlanetSpaceship>();
    onPlanetPlayer = transform.Find("Player").gameObject;
  }

  private void OnEnable() {
    spaceshipLandingOnPlanet = StartCoroutine(SpaceshipLandingOnPlanet());
  }
  private void OnDisable() {
    if (spaceshipLandingOnPlanet != null)
      StopCoroutine(spaceshipLandingOnPlanet);
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



}
