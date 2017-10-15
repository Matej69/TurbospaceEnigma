using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AnimationInfo{
    
    public string nameID;    
    public List<Sprite> sprites;
    [HideInInspector]
    public int spriteID;

}
