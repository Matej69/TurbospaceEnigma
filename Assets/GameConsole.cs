using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConsole : MonoBehaviour {

    InputField input_command;
    Text text_commandHistory;

    void Awake() {
        input_command = transform.Find("commandInput").GetComponent<InputField>();
        text_commandHistory = transform.Find("commandHistoryText").GetComponent<Text>();
    }

    void OnEnable() {
        input_command.ActivateInputField();
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            RunCommand(input_command.text);
		
	}



    void RunCommand(string cmd)
    {
      try
      {

        // Switch player weapon [pw 2]
        if(cmd.StartsWith("pw")) {
        int weaponID;
          if (int.TryParse(cmd.Substring(cmd.Length - 1, 1), out weaponID)) {
            GameObject.Find("Player").GetComponent<WeaponSwitching>().SetActiveWeapon((Weapon.E_WEAPON_TYPE)weaponID);
            PushToCommandHistory("[" + cmd + "] -> Player weapon switched to " + weaponID);
          }
        }


      }
      catch(System.NullReferenceException e) {
        PushToCommandHistory("[" + cmd + "] -> NullReferenceException while executing command");
      }
      catch(System.Exception e) {
        PushToCommandHistory("[" + cmd + "] -> Exception while executing command");
      }
    }

    void PushToCommandHistory(string str) {
      text_commandHistory.text += "\n" + str;
    }


}
