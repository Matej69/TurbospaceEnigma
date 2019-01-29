using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

  GameObject ref_gameConsole;

	// Use this for initialization
	void Awake () {
        ref_gameConsole = transform.Find("Console").gameObject;
    }
	
	// Update is called once per frame
	void Update () {

      HandleGameConsole();

    }

    void HandleGameConsole()
    {
        if (Input.GetKeyDown(KeyCode.KeypadMultiply))
          if (ref_gameConsole.activeSelf)
            ref_gameConsole.gameObject.SetActive(false);
          else
            ref_gameConsole.gameObject.SetActive(true);
    }
}
