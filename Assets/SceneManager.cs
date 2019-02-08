using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

  static SceneManager reference;

  public enum e_sceneID { IN_SPACE, ON_PLANET, NUM_OF_SCENES }

  [System.Serializable]
  public struct SceneIDPair {
    public e_sceneID sceneID;
    public GameObject sceneObj;
  }
  public List<SceneIDPair> scenes;

  private static SceneIDPair activeScene;

  void Awake () {
    reference = this;
  }

  private void Start()
  {
    SceneManager.ChangeTo(e_sceneID.ON_PLANET);
  }

  public static void ChangeTo(e_sceneID sceneID) {
    // Disable previous scene, and enable new scene
    if (activeScene.sceneObj != null)
      activeScene.sceneObj.SetActive(false);
    foreach(SceneIDPair scene in reference.scenes)
      if(scene.sceneID == sceneID) {
        activeScene = scene;
        activeScene.sceneObj.SetActive(true);
      }
    // Send event about scene change
    EventManager.event_sceneChange.Invoke(sceneID);
  }

  
}
