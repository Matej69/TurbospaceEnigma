﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameConsole : MonoBehaviour {

  InputField input_command;
  Text text_commandHistory;

  void Awake() {
    input_command = transform.Find("commandInput").GetComponent<InputField>();
    text_commandHistory = transform.Find("commandHistoryText").GetComponent<Text>();
  }

  // Update is called once per frame
  void Update () {
    if (!input_command.isFocused)
      input_command.ActivateInputField();
  
    if (Input.GetKeyDown(KeyCode.KeypadEnter))
      RunCommand(input_command.text);
  }



  void RunCommand(string cmd) {
    try {
  
      // Switch player weapon [pw 2]
      if(GetCmdSection(cmd, 1) == "pw") {
      int weaponID;
        if (int.TryParse(GetCmdSection(cmd, 2), out weaponID)) {
          GameObject.Find("Player").GetComponent<WeaponSwitching>().SetActiveWeapon((Weapon.E_WEAPON_TYPE)weaponID);
          PushToCommandHistory("[" + cmd + "] -> Player weapon switched to " + weaponID);
        }
      }
  
      // Camera zoom [cam 8 0.5]
      else if(GetCmdSection(cmd, 1) == "cam") {
        float targetZoom, zoomSpeed;
        if (float.TryParse(GetCmdSection(cmd, 2), out targetZoom) && float.TryParse(GetCmdSection(cmd, 3), out zoomSpeed)) {
          Camera.main.GetComponent<CameraController>().SetCameraZoom(targetZoom, zoomSpeed);
          PushToCommandHistory("[" + cmd + "] -> Camera zooming to size " + targetZoom + " by speed " + zoomSpeed);
        }
      }
  
      // Default message if command was not recognized
      else
         PushToCommandHistory("[" + cmd + "] -> Command could not be recognized");
  
  
    }
    catch(System.NullReferenceException e) {
      PushToCommandHistory("[" + cmd + "] -> NullReferenceException while executing command");
    }
    catch(System.Exception e) {
      PushToCommandHistory("[" + cmd + "] -> Exception while executing command");
    }
  }


  string GetCmdSection(string cmd, int section) {
    string[] sectionList = cmd.Split(' ');
    return sectionList[section-1];
  }

  void PushToCommandHistory(string str) {
    text_commandHistory.text += "\n" + str;
  }


}
