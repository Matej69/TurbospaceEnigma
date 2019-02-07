using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
  

  public class EventOnPlanetClicked : UnityEvent<Vector2> { };
  public class EventTravelingStateChanged : UnityEvent<SpaceTravelerBehaviour.e_travelState> { };
  public class EventMouseOnPlanet : UnityEvent<bool> { };


  public static EventOnPlanetClicked        event_planetClicked         = new EventOnPlanetClicked();
  public static EventTravelingStateChanged  event_travelingStateChange  = new EventTravelingStateChanged();
  public static EventMouseOnPlanet          event_mouseOnPlanet         = new EventMouseOnPlanet();

  void Awake () {

  }

}
