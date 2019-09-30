﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public variables
    public GameObject deathFloor;
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;
    public int maxAliensOnScreen;
    public int totalAliens;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;
    public GameObject upgradePrefab;
    public Gun gun;
    public float upgradeMaxTimeSpawn;
    public Animator arenaAnimator;

    //private variables
    private int aliensOnScreen = 0;
    private float generatedSpawnTime = 0;
    private float currentSpawnTime = 0;
    private bool spawnUpgrade = false;
    private float actualUpgradeTime = 0;
    private float currentUpgradeTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f,upgradeMaxTimeSpawn);
        actualUpgradeTime = Mathf.Abs(actualUpgradeTime);
    }

    // Update is called once per frame
    void Update()

    {

        if (player==null)
        {
            return;
        }
        currentUpgradeTime += Time.deltaTime;

        if (currentUpgradeTime > actualUpgradeTime)
        {
            
            if (!spawnUpgrade)
            {
                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                GameObject spawnLocation = spawnPoints[randomNumber];
                GameObject upgrade = Instantiate(upgradePrefab) as GameObject;
                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;
                
               
            spawnUpgrade = true;

             soundManager.Instance.PlayOneShot(soundManager.Instance.powerUpAppear);
            }
        }





        currentSpawnTime += Time.deltaTime;    
        //
        if (currentSpawnTime> generatedSpawnTime)
        {
            currentSpawnTime = 0;
        }

        generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        //
        if (aliensPerSpawn > 0 && aliensOnScreen < totalAliens)
        {
            List<int> previousSpawnLocations = new List<int>();

            if (aliensPerSpawn > spawnPoints.Length)
            {
                aliensPerSpawn = spawnPoints.Length - 1;
            }

            //
            aliensPerSpawn = (aliensPerSpawn > totalAliens) ? aliensPerSpawn - totalAliens : aliensPerSpawn;

            //
            for (int i = 0; i < aliensPerSpawn; i++)
            {
                if (aliensOnScreen < maxAliensOnScreen)
                {
                    aliensOnScreen += 1;

                    int spawnPoint = -1;

                    while (spawnPoint == -1)
                    {
                        int randomNumber = Random.Range(0, spawnPoints.Length - 1);

                        if (!previousSpawnLocations.Contains(randomNumber))
                        {
                            previousSpawnLocations.Add(randomNumber);

                            spawnPoint = randomNumber;
                        }
                    }

                    GameObject spawnLocation = spawnPoints[spawnPoint];
                    GameObject newAlien = Instantiate(alien) as GameObject;
                    newAlien.transform.position = spawnLocation.transform.position;

                    //refrence to alien script
                    Alien alienScript = newAlien.GetComponent<Alien>();

                    alienScript.target = player.transform;

                    Vector3 targetRotation = new Vector3(player.transform.position.x,newAlien.transform.position.y, player.transform.position.z);
                    newAlien.transform.LookAt(targetRotation);

                    alienScript.OnDestroy.AddListener(AlienDestroyed);

                    alienScript.GetDeathParticles().SetDeathFloor(deathFloor);
                }
            }
        }
        //
        



    }
    public void AlienDestroyed()
    {
        aliensOnScreen -= 1;
        totalAliens -= 1;
        if (totalAliens == 0)
        {
            Invoke("endGame", 2.0f);
        }
    }

    private void endGame()
    {
        soundManager.Instance.PlayOneShot(soundManager.Instance.
        elevatorArrived);
        arenaAnimator.SetTrigger("PlayerWon");
    }
   

}
