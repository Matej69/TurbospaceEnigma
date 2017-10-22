using UnityEngine;
using System.Collections;


public class GameProperties : MonoBehaviour {

    public int framerate = 60;

	void Awake()
	{
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = framerate;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{
        Application.targetFrameRate = framerate;
    }
}
