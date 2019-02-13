using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

  List<GUI> guiList = new List<GUI>();



  // Use this for initialization
  void Awake () {
    guiList = new List<GUI>() {
      transform.Find("Console").gameObject.GetComponent<GUI>(),
      transform.Find("LeavePlanet").gameObject.GetComponent<GUI>()
    };
    EventManager.event_playerOnSurfaceAndInSpaceshipRange.AddListener(OnPlayerInSpaceshipRangeChanged);
    EventManager.event_startSpaceshipLunchFromPlanet.AddListener(delegate { SetGUIState(GUI.GUIType.LEAVE_PLANET_BTN, false); } );
  }

  // Update is called once per frame
  void Update () {
    HandleGameConsole();
  }


  void SetGUIState(GUI.GUIType guiType, bool state) {
    GetGUI(guiType).gameObject.SetActive(state);
  }

  GUI GetGUI(GUI.GUIType guiType) {
    foreach (GUI gui in guiList)
      if (gui.guiType == guiType)
        return gui;
    return null;
  }



  void HandleGameConsole() {
    if (Input.GetKeyDown(KeyCode.KeypadMultiply))
      if (GetGUI(GUI.GUIType.CONSOLE).gameObject.activeSelf)
        SetGUIState(GUI.GUIType.CONSOLE, false);
      else
        SetGUIState(GUI.GUIType.CONSOLE, true);
  }

  void OnPlayerInSpaceshipRangeChanged(bool state) {
    SetGUIState(GUI.GUIType.LEAVE_PLANET_BTN, state);
  }
  



}
