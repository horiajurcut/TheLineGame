﻿using UnityEngine;
using Util;

namespace PowerUp
{
    public class PowerUp : MonoBehaviour
    {

        public enum PowerUpType
        {
            Default,
            Reverse,
            Speed
        };
        public PowerUpType TypeOfPowerUp;

        private void Start()
        {
            TypeOfPowerUp = RandomValues.RandomEnumValue<PowerUpType>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PowerUpManager.Instance.OnPickUp(TypeOfPowerUp, collision);
            }
            GameObject.Destroy(gameObject);
        }
    }
}
