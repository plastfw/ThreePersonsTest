using System;
using System.Collections.Generic;
using UnityEngine;


namespace Core
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private readonly Func<T> _objectFactory;
        private readonly List<T> _pool;
        private readonly Action<T> _onGet;
        private readonly Action<T> _onReturn;

        public ObjectPool(Func<T> objectFactory, Action<T> onGet, Action<T> onReturn, int initialSize)
        {
            _objectFactory = objectFactory;
            _pool = new List<T>(initialSize);
            _onGet = onGet;
            _onReturn = onReturn;

            for (int i = 0; i < initialSize; i++)
            {
                T obj = _objectFactory();
                obj.gameObject.SetActive(false);
                _pool.Add(obj);
            }
        }

        public T Get()
        {
            T obj = _pool.Find(item => !item.gameObject.activeSelf);

            if (obj == null)
            {
                obj = _objectFactory();
                obj.gameObject.SetActive(false);
                _pool.Add(obj);
            }

            obj.gameObject.SetActive(true);
            _onGet?.Invoke(obj);
            return obj;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _onReturn?.Invoke(obj);
        }
    }
}