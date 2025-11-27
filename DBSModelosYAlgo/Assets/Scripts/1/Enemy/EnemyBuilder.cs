using Builder;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Builder
{
    public class EnemyBuilder
    {
        Enemy _enemyPrefab;

        private Color _newColor;

        private float _newMaxLife;

        private Vector3 _newposition;

        private Vector3 _newScale;

        public EnemyBuilder(Enemy enemyPrefab)
        {
            _enemyPrefab = enemyPrefab;

            _newMaxLife = 100;

            _newScale = Vector3.one;

            _newColor = Color.white;
        }

        #region Max Life
        public EnemyBuilder SetMaxLife(float maxLife)
        {
            _newMaxLife = maxLife;

            return this;
        }
        #endregion

        #region Position
        public EnemyBuilder SetPosition(Vector3 position)
        {
            _newposition = position;

            return this;
        }


        public EnemyBuilder SetPosition(float x, float y, float z)
        {
            return SetPosition(new Vector3(x, y, z));


        }
        #endregion

        #region Scale
        public EnemyBuilder SetScale(Vector3 scale)
        {
            _newScale = scale;

            return this;
        }
        #endregion

        #region Color
        public EnemyBuilder SetColor(Color color)
        {
            _newColor = color;

            return this;
        }
        #endregion

        public Enemy Done()
        {
            var enemy = Object.Instantiate(_enemyPrefab);

            var enemyTransform = enemy.transform;

            enemy.transform.position = _newposition;
            enemyTransform.localScale = _newScale;

            enemy.MaxLife = _newMaxLife;

            enemy.GetComponent<Renderer>().material.color = _newColor;

            return enemy;

        }

    }
}
