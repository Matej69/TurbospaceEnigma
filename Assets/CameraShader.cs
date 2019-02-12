using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShader : MonoBehaviour {

  public Material enteringOrExitingPlanetMaterial;
  float shaderAppliedValue = 0.0f; //range from 0-1
  
  void OnRenderImage(RenderTexture src, RenderTexture dst) {
    if (enteringOrExitingPlanetMaterial != null) {
      enteringOrExitingPlanetMaterial.SetFloat("_CutOffValue", shaderAppliedValue);
      Graphics.Blit(src, dst, enteringOrExitingPlanetMaterial);
    }
  }

  private void Awake() {
    EventManager.event_sceneNeedsToChange.AddListener(StartBetweenSceneShader);
  }


  void StartBetweenSceneShader(SceneManager.e_sceneID sceneID) {
    StartCoroutine(StartBetweenSceneShaderCoroutine(sceneID));
  }

  IEnumerator StartBetweenSceneShaderCoroutine(SceneManager.e_sceneID sceneID) {
    float speed = 1;
    shaderAppliedValue = 0.0f;
    // fade in
    while (shaderAppliedValue != 1.0f) {
      shaderAppliedValue = (shaderAppliedValue + speed * Time.deltaTime < 1.0f) ? shaderAppliedValue + speed * Time.deltaTime : 1.0f;
      yield return null;
    }
    // change scene
    SceneManager.ChangeTo(sceneID);
    // fade out
    while (shaderAppliedValue != 0.0f)
    {
      shaderAppliedValue = (shaderAppliedValue - speed * Time.deltaTime > 0.0f) ? shaderAppliedValue - speed * Time.deltaTime : 0.0f;
      yield return null;
    }
  }

  }
