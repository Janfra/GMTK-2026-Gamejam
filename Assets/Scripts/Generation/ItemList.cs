using Janito.EditorExtras;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GMTK.Generation
{
    [Serializable]
    public class ItemList<T>
        where T : class
    {
        [SerializeField]
        private bool _allowRepeat;
        [SerializeField]
        private List<T> _initialItems;

        private List<T> _existingItems = new();
        private T _lastPull;

        public void Initialise()
        {
            _existingItems = _initialItems;
        }

        public bool TryGet(out T result)
        {
            if (_existingItems.Count == 0)
            {
                result = default(T);
                return false;
            }

            int index = Random.Range(0, _existingItems.Count);
            result = _existingItems[index];
            if (!_allowRepeat)
            {
                _existingItems.RemoveAt(index);
                if (_lastPull != null)
                {
                    _existingItems.Add(_lastPull);
                }

                if (_existingItems.Count == 0)
                {
                    LogLibrary.LogErrorInDevelopment<ItemList<T>>("Must allow repeats since will be empty otherwise.");
                    _existingItems.Add(result);
                }
                else
                {
                    _lastPull = result;
                }
            }
            return true;
        }
    }
}