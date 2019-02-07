using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    static public GameObject GetPlanetObject(PlanetTypes.e_type planetType)
    {
        GameObject planet = Resources.Load<GameObject>("GameObjects/PlanetTravelScreen");
        string basePath = "Graphics/planets";
        switch (planetType)
        {
            case PlanetTypes.e_type.ANTI_MATER: planet.GetComponent<SpriteRenderer>().sprite = GetSprite(basePath +"/antimater") ; break;
            case PlanetTypes.e_type.ICE: planet.GetComponent<SpriteRenderer>().sprite = GetSprite(basePath + "/ice"); break;
            case PlanetTypes.e_type.MAGMA: planet.GetComponent<SpriteRenderer>().sprite = GetSprite(basePath + "/magma"); break;
            case PlanetTypes.e_type.VEGETATION: planet.GetComponent<SpriteRenderer>().sprite = GetSprite(basePath + "/vegetation"); break;
            case PlanetTypes.e_type.WATER: planet.GetComponent<SpriteRenderer>().sprite = GetSprite(basePath + "/water"); break;
        }
        return planet;
    }

    static public Sprite GetSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }
}