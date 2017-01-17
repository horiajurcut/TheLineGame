using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    public PowerUp[] PowerUpsPrefabs;
    public float TimeBetweenPowerUpSpawns = 5f;

    private float _nextPowerUpSpawnTime = 0f;

    private void Update()
    {
        if (!(Time.time > _nextPowerUpSpawnTime)) return;

        var powerUpPrefabIndex = Random.Range(0, PowerUpsPrefabs.Length);

        var spawnPosition = Camera.main.ScreenToWorldPoint(
            new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0f)
        );
        var newPowerUp = Instantiate(
            PowerUpsPrefabs[powerUpPrefabIndex],
            new Vector3(spawnPosition.x, spawnPosition.y, 0f),
            Quaternion.identity
        );
        newPowerUp.transform.parent = GameObject.Find("PowerUps").transform;

        _nextPowerUpSpawnTime = Time.time + TimeBetweenPowerUpSpawns;
    }
}
