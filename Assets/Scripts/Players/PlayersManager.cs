﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Players
{
    public class PlayersManager : MonoBehaviour
    {

        public static PlayersManager Instance;

        [Header("Players Spawn Settings")]
        public GameObject PlayerPrefab;
        public int NumberOfPlayers = 1;
        public float ScreenCoverage = 0.9f;

        private List<GameObject> _players;
        private List<Vector3> _playerPositions;
        private float _minDistanceBetweenPlayers;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            _players = new List<GameObject>();
            _playerPositions = new List<Vector3>();

            _minDistanceBetweenPlayers = Vector3.Distance(
                GameManager.Instance.InitialVectorTopRight,
                GameManager.Instance.InitialVectorBottomLeft
            ) / NumberOfPlayers * ScreenCoverage;

            for (var i = 0; i < NumberOfPlayers; i++)
            {
                var newPlayerPosition = GetPlayerSpawnPosition();

                var newPlayer = Instantiate(
                    PlayerPrefab,
                    newPlayerPosition,
                    Quaternion.identity
                );

                _playerPositions.Add(newPlayerPosition);
                _players.Add(newPlayer);
            }
        }

        private Vector3 GetPlayerSpawnPosition()
        {
            var collidesWithOtherPlayer = true;
            var spawnPosition = Vector3.zero;

            while (collidesWithOtherPlayer)
            {
                spawnPosition = Camera.main.ScreenToWorldPoint(
                    new Vector3(
                        Random.Range((1f - ScreenCoverage) * Screen.width, ScreenCoverage * Screen.width),
                        Random.Range((1f - ScreenCoverage) * Screen.height, ScreenCoverage * Screen.height),
                        0f
                    )
                );
                spawnPosition.z = 0f;

                if (_playerPositions.Count == 0)
                {
                    return spawnPosition;
                }

                collidesWithOtherPlayer = _playerPositions.Any(
                    playerPosition => Vector3.Distance(spawnPosition, playerPosition) < _minDistanceBetweenPlayers);
            }

            return spawnPosition;
        }

        public int GetNumberOfPlayers()
        {
            return _players.Count;
        }

        public bool RemoveFromActivePlayers(GameObject deadPlayer)
        {
            if (!_players.Remove(deadPlayer))
            {
                return false;
            }

            if (_players.Count == 0)
            {
                GameManager.Instance.EndGame();
            }

            return true;
        }
    }
}