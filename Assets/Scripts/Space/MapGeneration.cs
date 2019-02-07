using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    static private MapGeneration ref_mapGeneration;
    static public MapGeneration GetRefrence() { return ref_mapGeneration; }

    private void Awake()
    {
        ref_mapGeneration = this;
    }


    const float LAYER_DISTANCE = 2f;  //distance between 2 neighbourhood layers on which planets might be spawn
    const float MIN_PLANET_ON_SAME_LAYER_DISTANCE = 5f;

    public List<GameObject> visiblePlanets; //all planets that are not in this list will be considered non-existant since nothing can be done with them at this point

    // Use this for initialization
    void Start () {        
        RadialPlanetSpawn(new Vector2(0,0), 150, 5, PlanetTypes.e_type.ICE, "IcePlanetGroup");
        RadialPlanetSpawn(new Vector2(0, 0), 150, 5, PlanetTypes.e_type.MAGMA, "MagmaPlanetGroup");
        RadialPlanetSpawn(new Vector2(0, 0), 150, 5, PlanetTypes.e_type.VEGETATION, "VegetationPlanetGroup");
    }
	
	// Update is called once per frame
	void Update () {
    }


    void RadialPlanetSpawn(Vector2 centerPos, float maxRadiusSpawn, float density, PlanetTypes.e_type planetType, string planetGroupName)
    {
        float startTime = Time.realtimeSinceStartup;
        const float minDistanceFromCenter = 8f; //used since density close to center is huge(planets would be to close to each other)
        GameObject planetsHolder = new GameObject(planetGroupName);   //Parent GameObject of all instantiated objects

        for(float distanceFromCenter = minDistanceFromCenter; distanceFromCenter < maxRadiusSpawn; distanceFromCenter += LAYER_DISTANCE)    //loop through every layer(layer=one of manny circles around center with given radius. Every next circle will have bigger radius/will be further from center)
        {
            List<GameObject> currentLayerPlanetList = new List<GameObject>();
            for (int i = 0; i < density; ++i)    //Try to spawn this many planets on current layer
            {                
                float distancePercentFactor = 1 - distanceFromCenter / maxRadiusSpawn;  //it gets lower, the chance that planet will be spawn are also lower
                bool shouldPlanetBeSpawn = (distancePercentFactor > Random.Range(0f, 1f)) ? true : false;
                if (shouldPlanetBeSpawn)
                {
                    float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;   //sin and cos function work with radians
                    Vector2 newPlanetRandPosOnLayer = new Vector2(distanceFromCenter * Mathf.Cos(randomAngle) + centerPos.x, distanceFromCenter * Mathf.Sin(randomAngle) + centerPos.y);   //calculate position of coordinate with given angle
                   
                    bool planetDistanceToShort = false;
                    foreach (GameObject p in currentLayerPlanetList)    //loop through every planet on current layer and check if there is AT LEAST ONE that is to close to our planet
                        if (Vector2.Distance(p.transform.position, newPlanetRandPosOnLayer) < MIN_PLANET_ON_SAME_LAYER_DISTANCE)                        
                            planetDistanceToShort = true;                        
                    if (!planetDistanceToShort)
                    {
                        GameObject newPlanet = Instantiate(ResourceManager.GetPlanetObject(planetType), newPlanetRandPosOnLayer, Quaternion.identity, planetsHolder.transform);
                        newPlanet.AddComponent<Planet>();
                        newPlanet.AddComponent<CircleCollider2D>();

                        currentLayerPlanetList.Add(newPlanet);
                        /*
                         * visiblePlanets are here for now 
                        */
                        visiblePlanets.Add(newPlanet);
                    }                   
                }
            }            
        }
        // Debug.Log("Planets spawn => [" + visiblePlanets.Count + "," + planetType + "] Time="+ (Time.realtimeSinceStartup - startTime));
    }




}
