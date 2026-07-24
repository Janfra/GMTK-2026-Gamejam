using System;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public class InventoryComponent : MonoBehaviour
    {
        [SerializeField]
        private Vector2[] _itemPositions;
        private List<DraggableTileComponent> _items = new();

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