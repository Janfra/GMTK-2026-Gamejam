using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GMTK
{
    public interface IInventoryItem : IDraggable
    {
        public event Action<IInventoryItem, bool> OnReturnedToInventory;
        public void ReturnToInventory(bool isInstant);
        public void StartReturnAnimation();
        public void OnReturned();
        public float MoveDelay { get; }
    }

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
                item.OnReturnedToInventory += ReturnItem;
                _itemCoroutines.TryAdd(item, null);
            }
        }

        private void ReturnItem(IInventoryItem item, bool isInstant)
        {
            if (item is not DraggableTileComponent comp || !_items.Contains(comp))
            {
                return;
            }

            item.IsBeingDragged = false;
            item.IsLocked = false;
            if (isInstant)
            {
                comp.transform.position = GetWorldPositionOfItem(comp);
            }
            else
            {
                TryUpdatePosition(item);
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
            dragComp.StartReturnAnimation();
            yield return new WaitForSeconds(dragComp.MoveDelay);

            draggable.UpdateDesiredDragPosition(GetWorldPositionOfItem(dragComp));
            dragComp.OnReturned();
            _itemCoroutines[draggable] = null;
        }

        private Vector2 GetWorldPositionOfItem(DraggableTileComponent component)
        {
            int index = _items.IndexOf(component);
            return transform.TransformPoint(_itemPositions[index]);
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