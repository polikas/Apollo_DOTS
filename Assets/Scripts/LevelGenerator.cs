using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] availableLevel;
    public List<GameObject> currentLevel;
    private float screenWidthInPoints;

    // array to hold all the planets
    public GameObject[] availablePlanets;
    // list to store the created planets
    // so that I can check if I need to add more ahead 
    // or remove them when they have left the screen
    public List<GameObject> planets;

    // minDis and maxDis are used to pick random distance between
    // the last object and the currently added object
    // so that the objects don't appear at a fixed interval
    public float planetsMinDistance = 5.0f;
    public float planetsMaxDistance = 10.0f;

    // minY and MaxY to configure the minimum and maximum height
    // at which planets are placed
    public float planetsMinY = -1.4f;
    public float planetsMaxY = 1.4f;

   


    // Start is called before the first frame update
    void Start()
    {
        // calclulate the size of the screen in points
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;


        StartCoroutine(LevelGeneratorCheck());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // a method to add a new level using the levelEndX point
    void AddLevel(float levelEndX)
    {
        // picks a random index of the level type to generate
        int randomLevelIndex = Random.Range(0, availableLevel.Length);
        // creates a level object from the array of available 
        // levels using the random index chosen above
        GameObject level = Instantiate(availableLevel[randomLevelIndex]);
        // get the size of the floor inside the level
        float roomWidth = level.transform.Find("ground").localScale.x;
        // calculate the center and take the furthest edge of the level so far
        // and add half of the new level width
        float roomCenter = levelEndX + roomWidth * 0.5f;
        // set the position of the level 
        level.transform.position = new Vector3(roomCenter, 0, 0);
        // add the level to the list of current level
        currentLevel.Add(level);
    }


    // a method to check if a new level is required
    private void GenerateLevelIfRequired()
    {
        // create a new list to store levels that need
        // to be removed
        List<GameObject> levelsToRemove = new List<GameObject>();
        // a variable to show if need to add more levels
        // by default is set to true
        bool addLevels = true;
        // save rockets position
        float rocketX = transform.position.x;
        // a point after which the level should be removed
        float removeLevelX = rocketX - screenWidthInPoints;
        // if there is no level after the addLevelX point
        // then need to add a level
        float addLevelX = rocketX + screenWidthInPoints;
        // store to farthestLevelEndX the point where the level
        // currently ends
        float farthestLevelEndX = 0;
        foreach (var level in currentLevel)
        {
            // foreach loop to enumerate currentLevel
            float levelWidth = level.transform.Find("ground").localScale.x;
            float levelStartX = level.transform.position.x - (levelWidth * 0.5f);
            float levelEndX = levelStartX + levelWidth;
            // if there is a level starts after addLevelX 
            // then no need to add level right now
            if (levelStartX > addLevelX)
            {
                addLevels = false;
            }
            // if the level ends to the left of the removeLevelX
            // then it is already off the scree and needs to be removed
            if (levelEndX < removeLevelX)
            {
                levelsToRemove.Add(level);
            }
            // find the rightmost point of the level
            farthestLevelEndX = Mathf.Max(farthestLevelEndX, levelEndX);
        }
        // removes level that are mared for removal
        foreach (var level in levelsToRemove)
        {
            currentLevel.Remove(level);
            Destroy(level);
        }
        // if addLevels is true then level end is near
        // this indicates that a new level needs to be added
        if (addLevels)
        {
            AddLevel(farthestLevelEndX);
        }
    }

    // a coroutine to execute GenerateLevelIfRequired() or
    // GeneratePlanetIfRequired() periodically
    private IEnumerator LevelGeneratorCheck()
    {
        while (true)
        {
            GenerateLevelIfRequired();
            GeneratePlanetIfRequired();
            yield return new WaitForSeconds(0.25f);
        }
    }


    // a method to take the position of the last planet 
    // and creates a new planet at a random position after it
    // with a given interval
    void AddPlanet(float lastPlanetX)
    {
        // Generates a random index to select
        // a random planet from the array
        int randomIndex = Random.Range(0, availablePlanets.Length);
        // creates an instance of the planet that was just randomly selected
        GameObject planet = Instantiate(availablePlanets[randomIndex]);
        // sets the planets position, using a random interval
        // and a random height
        float planetPositionX = lastPlanetX + Random.Range(planetsMinDistance, planetsMaxDistance);
        float randomY = Random.Range(planetsMinY, planetsMaxY);
        planet.transform.position = new Vector3(planetPositionX, randomY, 0);
     
        // adds the newly created planet to the planets list
        // for tracking and ultimately, removal when it leaves the screen
        planets.Add(planet);
    }


    // a method to check if an object should be added or remove
    void GeneratePlanetIfRequired()
    {
        // if the planet is to the left of removePlanetsX
        // then it has already left the screen and is far behind
        // if there is no planet after addPlanetX
        // then add more planets since the last of the generated planet
        // is about to enter the screen
        // the farthestPlanetX variable is used to find the position of the last (rightmost)
        // planet to compare it with addPlanetX
        float rocketX = transform.position.x;
        float removePlanetsX = rocketX - screenWidthInPoints;
        float addPlanetX = rocketX + screenWidthInPoints;
        float farthestPlanetX = 0;
        // place planets that need to be removed in a list
        // to be removed after the loop
        List<GameObject> planetsToRemove = new List<GameObject>();
        foreach (var planet in planets)
        {
            // the position of the planet
            float planetX = planet.transform.position.x;
            // for each planetX get a maxium planetX value
            // in farthestPlanetX at the end of the loop
            farthestPlanetX = Mathf.Max(farthestPlanetX, planetX);
            // if the current planet is far behind
            // it is marde for removal to free up some resources
            if (planetX < removePlanetsX)
            {
                planetsToRemove.Add(planet);
            }
        }
        // Removes planets marked for removal
        foreach (var planet in planetsToRemove)
        {
            planets.Remove(planet);
            Destroy(planet);
        }
        // if the player is about to see the last planet
        // and there are no more planets ahead
        // call the method to add a new planet
        if (farthestPlanetX < addPlanetX)
        {
            AddPlanet(farthestPlanetX);
        }
    }

}
