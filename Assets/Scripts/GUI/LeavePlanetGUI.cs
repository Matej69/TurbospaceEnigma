﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeavePlanetGUI : GUI {


  void Awake() {
    Button btn_leavePlanet = transform.Find("Button").GetComponent<Button>();
    btn_leavePlanet.onClick.AddListener(delegate {
      SceneManager.ChangeTo(SceneManager.e_sceneID.IN_SPACE);
    });
  }
  
}