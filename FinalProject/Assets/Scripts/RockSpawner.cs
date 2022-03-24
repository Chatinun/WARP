using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject rockPrefab;
    [SerializeField] private GameObject flyingPrefab;

    private float timeBtwSpawnRock;
    public float startTimeBtwSpawnRock;

    private float timeBtwSpawnFlying;
    public float startTimeBtwSpawnFlying;

    void Update()
    {
        //Spawning Rock
        if(timeBtwSpawnRock >= startTimeBtwSpawnRock)
        {
            GameObject rock = Instantiate(rockPrefab, transform.position, transform.rotation);
            
            Destroy(rock, 5f);
            ScreenShakeController.instance.StartShake(1f, 0.02f);
            AudioManager.instance.audioPlay("RockFall");
            timeBtwSpawnRock = 0;
        }
        else
        {
            timeBtwSpawnRock += Time.deltaTime;
        }

        //Spawning Flying
        if(timeBtwSpawnFlying >= startTimeBtwSpawnFlying)
        {
            Instantiate(flyingPrefab, transform.position, transform.rotation);
            timeBtwSpawnFlying = 0;
        }
        else
        {
            timeBtwSpawnFlying += Time.deltaTime;
        }
    }


}
