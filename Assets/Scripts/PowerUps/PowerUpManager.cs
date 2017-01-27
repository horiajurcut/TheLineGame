using UnityEngine;

namespace PowerUps
{
    public class PowerUpManager : MonoBehaviour
    {

        public static PowerUpManager Instance;

        public PowerUp[] PowerUpsPrefabs;
        public float TimeBetweenPowerUpSpawns = 5f;

        private float _nextPowerUpSpawnTime;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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

        public void OnPickUp(PowerUp.PowerUpType powerUpType, Collider2D collision)
        {
            Debug.Log(collision.name + " picked up " + powerUpType);
        }
    }
}
