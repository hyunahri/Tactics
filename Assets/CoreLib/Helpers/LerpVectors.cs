using System;
using System.Collections;
using UnityEngine;

namespace CoreLib.Helpers
{
    public struct LerpVectors
    {
        public static Func<IEnumerator, Coroutine> StartCoroutine;
        private readonly float _offset;
        private readonly GameObject[] _gameObjects;
        private readonly Vector3[] _ends;
        public LerpVectors(float offset, GameObject[] gameObjects, Vector3[] ends)
        {
            _offset = offset;
            _gameObjects = gameObjects;
            _ends = ends;
        }
        public void MoveToTargets(float speed, AnimationCurve curve = null)
        {
            StartCoroutine(Lerp(speed, curve));
        }
        IEnumerator Lerp(float speed, AnimationCurve curve = null)
        {
            int remainder = _gameObjects.Length - 1;
            while (remainder > 0)
            {
                for (int i = 0; i < _gameObjects.Length; i++)
                {
                    var t = _gameObjects[i].transform;
                    t.position = new Vector3(_ends[i].x, 0f, _ends[i].z);
                    while (t.position != _ends[i])
                    {
                        t.position = Vector3.MoveTowards(t.position, _ends[i], curve?.Evaluate(speed) ?? speed);
                        yield return null;
                    }
                }
            }
        }
    }
}
