using UnityEngine;
using System.Collections;


public class GlobalInformation : MonoBehaviour {

    [HideInInspector]
    static public GlobalInformation instance;
    
    public LayerMask mask_platform;
    public LayerMask mask_enemy;



    void Awake()
	{
        instance = this;
    }
	
	void Start () 
	{	
	}

	void Update () 
	{	
	}
}
