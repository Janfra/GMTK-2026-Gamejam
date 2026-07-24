using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace GMTK
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField]
        private float _delay;
        [SerializeField]
        private Vector2[] _itemPositions;
        private List<DraggableTileComponent> _items = new();
        private Dictionary<IDraggable, Coroutine> _itemCoroutines = new();

        private void Awake()
        {
            var existing = GetComponentsInChildren<DraggableTileComponent>();
            if (existing.Length > _itemPositions.Length)
            {
                int difference = existing.Length - _itemPositions.Length;
                for (int i = _itemPositions.Length; i < existing.Length; i++)
                {
                    Destroy(existing[i].gameObject);
                }

                Array.Resize(ref existing, _itemPositions.Length);
            }

            _items.AddRange(existing);
        }

        private void Start()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                item.transform.localPosition = _itemPositions[i];
                item.OnDragStateChanged += TryUpdatePosition;
                _itemCoroutines.TryAdd(item, null);
            }
        }

        private void TryUpdatePosition(IDraggable draggable)
        {
            if (_itemCoroutines.TryGetValue(draggable, out Coroutine coroutine) && coroutine != null)
            {
                StopCoroutine(coroutine);
            }

            coroutine = StartCoroutine(TryUpdatePositionAfterDelay(draggable));
            _itemCoroutines[draggable] = coroutine;
        }

        private IEnumerator TryUpdatePositionAfterDelay(IDraggable draggable)
        {
            yield return new WaitForSeconds(Mathf.Max(_delay, 0.5f));

            if (draggable.IsBeingDragged || draggable.IsLocked)
            {
                _itemCoroutines[draggable] = null;
                yield break;
            }

            var dragComp = draggable as DraggableTileComponent;
            int index = _items.IndexOf(dragComp);
            draggable.UpdateDesiredDragPosition(transform.TransformPoint(_itemPositions[index]));
            _itemCoroutines[draggable] = null;
        }

        private void OnDrawGizmosSelected()
        {
            if (_itemPositions == null) return;
            Gizmos.color = Color.green;
            foreach (Vector2 item in _itemPositions)
            {
                Gizmos.DrawSphere(item + (Vector2)transform.position, 0.1f);
            }
        }
    }
}