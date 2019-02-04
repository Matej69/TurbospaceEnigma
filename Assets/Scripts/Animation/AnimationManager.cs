using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AnimationManager : MonoBehaviour {

  public enum E_STATE { PLAY, PAUSED, STOPPED }
  private E_STATE state;
  
  public List<AnimationInfo> animations;
  private AnimationInfo curAnimationInfo;
  
  [HideInInspector]
  public SpriteRenderer spriteRenderer;
  //float frameSpeedInSeconds = 0.05f; //20 sprites per seconds


  void Awake() {
  spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
  }
	
	void Start () {
  }

	void Update () {
  }



    

  public void SetAnimation(string name) {
    if (animations.Count != 0) {
      foreach (AnimationInfo animInfo in animations) {
        if (animInfo.nameID == name) {
          //if it is already set to this one
          if (curAnimationInfo != null && animInfo.nameID == curAnimationInfo.nameID)
            return;
          //if it is changed to this one 
          curAnimationInfo = animInfo;                    
          break;
        }
      }
      SetState(E_STATE.PLAY);
      spriteRenderer.sprite = curAnimationInfo.sprites[0];
    }
    else
      Debug.LogError("CALLED SetAnimation(name) but no animation was found");
  }
  
  public void SetState(E_STATE newState) {
    state = newState;
  }
  
  public IEnumerator HandleAnimationUpdate() {
    while (true) {
      if (spriteRenderer.sprite != null) {
        if (state == E_STATE.STOPPED || state == E_STATE.PAUSED) {
            curAnimationInfo.spriteID = (state == E_STATE.STOPPED) ? 0 : curAnimationInfo.spriteID;
            yield return null;
            continue;
        }
        curAnimationInfo.spriteID = (curAnimationInfo.spriteID == curAnimationInfo.sprites.Count - 1) ? 0 : ++curAnimationInfo.spriteID;
        spriteRenderer.sprite = curAnimationInfo.sprites[curAnimationInfo.spriteID];
      }
      
      if (curAnimationInfo == null)
        yield return null;
      
      yield return new WaitForSeconds(curAnimationInfo.frameSpeedInSeconds);
    }
  }

    



}
