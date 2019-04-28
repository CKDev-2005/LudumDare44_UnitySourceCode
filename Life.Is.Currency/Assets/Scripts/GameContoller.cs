using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContoller : MonoBehaviour
{

    public bool canSpawn;

    public GameObject[] asteroids;
    public Transform[] spawnPoints;

    public float startSpawnTime;
    public float minTimeBtwSpawns;
    public float maxTimeBtwSpawns;
    float spawnTime;

    public GameObject[] fuelTypes;

    public float fuelSpawnTime;
    public float minTimeBtwFuelSpawns;
    public float maxTimeBtwFuelSpawns;

    public GameObject upgradeStation;

    public float upgradeSpawnTime;
    public float minTimeBtwUpgradeSpawns;
    public float maxTimeBtwUpgradeSpawns;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = startSpawnTime;

        fuelSpawnTime = Random.Range(minTimeBtwFuelSpawns, maxTimeBtwFuelSpawns);
        upgradeSpawnTime = Random.Range(minTimeBtwUpgradeSpawns, maxTimeBtwUpgradeSpawns);

        Camera.main.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {

        if(spawnTime < 0)
        {
            SpawnAsteroid();

            spawnTime = Random.Range(minTimeBtwSpawns, maxTimeBtwSpawns);
        }
        else
        {
            spawnTime -= Time.deltaTime;
        }

        if (fuelSpawnTime < 0)
        {
            int fuel = Random.Range(0, fuelTypes.Length);
            int spawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(fuelTypes[fuel], spawnPoints[spawnPoint]);

            fuelSpawnTime = Random.Range(minTimeBtwFuelSpawns, maxTimeBtwFuelSpawns);
        }
        else
        {
            fuelSpawnTime -= Time.deltaTime;
        }

        if(upgradeSpawnTime < 0)
        {
            int spawnPoint = Random.Range(0, spawnPoints.Length);

            Instantiate(upgradeStation, spawnPoints[spawnPoint]);

            upgradeSpawnTime = Random.Range(minTimeBtwUpgradeSpawns, maxTimeBtwUpgradeSpawns);
        }
        else
        {
            upgradeSpawnTime -= Time.deltaTime;
        }

    }

    public void SpawnAsteroid()
    {
        int asteroidType = Random.Range(0, asteroids.Length);
        int spawnPoint = Random.Range(0, spawnPoints.Length);

        Instantiate(asteroids[asteroidType], spawnPoints[spawnPoint]);
    }
}
