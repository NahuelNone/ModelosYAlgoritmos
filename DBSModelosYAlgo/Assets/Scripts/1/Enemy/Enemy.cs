using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class Enemy : MonoBehaviour
    {
        public float MaxLife
        {

            get => _maxLife;

            set
            {

                _maxLife = 100f;

            }

        }

        public float currentLife
        {

            get => currentLife;

            set
            {

                currentLife = 100f;

            }

        }

        [SerializeField] private float _maxLife;
        private float _currentLife;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

               GetHit();

            }
        }

        private void GetHit()
        {

            _currentLife -= 25;

            if (_currentLife < 0) return;

        }

        public void TakeDamage(int amount)
        {
            _currentLife -= amount;
            if (_currentLife <= 0) Destroy(gameObject);
        }

    }
}