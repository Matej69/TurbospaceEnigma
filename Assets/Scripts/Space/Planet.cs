using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour{

  static public float radius = 0.5f;

  private void OnMouseEnter() {
    EventManager.event_mouseOnPlanet.Invoke(true);
  }

  private void OnMouseExit() {
    EventManager.event_mouseOnPlanet.Invoke(false);
  }

  private void OnMouseDown() {
    EventManager.event_planetClicked.Invoke(this);
  }

}
