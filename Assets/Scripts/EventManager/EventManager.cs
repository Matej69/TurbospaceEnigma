using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
  

  public class EventOnPlanetClicked         : UnityEvent<Planet> { };
  public class EventTravelingStateChanged   : UnityEvent<SpaceTravelerBehaviour.e_travelState> { };
  public class EventMouseOnPlanet           : UnityEvent<bool> { };
  public class EventSceneNeedsToChange      : UnityEvent<SceneManager.e_sceneID> { };
  public class EventSceneChanged            : UnityEvent<SceneManager.e_sceneID> { };
  public class EventPlayerInSpaceshipRange  : UnityEvent<bool> { };


  public static EventOnPlanetClicked        event_planetClicked           = new EventOnPlanetClicked();
  public static EventTravelingStateChanged  event_travelingStateChange    = new EventTravelingStateChanged();
  public static EventMouseOnPlanet          event_mouseOnPlanet           = new EventMouseOnPlanet();
  public static EventSceneNeedsToChange     event_sceneNeedsToChange      = new EventSceneNeedsToChange();        // Triggers BEFORE scene has changed
  public static EventSceneChanged           event_sceneChanged            = new EventSceneChanged();              // Triggers AFTER scene has changed
  public static UnityEvent                  event_spaceshipLanded         = new UnityEvent();
  public static EventPlayerInSpaceshipRange event_playerInSpaceshipRange  = new EventPlayerInSpaceshipRange();




  void Awake () {

  }

}
