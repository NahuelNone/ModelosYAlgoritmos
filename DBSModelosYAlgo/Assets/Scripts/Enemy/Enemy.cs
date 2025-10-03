using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Builder
{
    public class Enemy : MonoBehaviour
    {
        public float MaxLife
        {
            get => _maxLife; //devuelve la maxlife


            set
            {
                _maxLife = Mathf.Clamp(value, 0, 100); //clampeamos entre 0 y 100

                _currentLife = _maxLife; //hacemos que vida actual sea igual a la maxima
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
    }
}