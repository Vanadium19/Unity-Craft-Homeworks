using Codice.CM.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable NotResolvedInText

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        private const int RowIndex = 1;
        private const int ColumnIndex = 0;

        private readonly Vector2Int _invalidPosition = new(-1, -1);

        private Item[,] _itemsMatrix;
        private Dictionary<Item, Vector2Int> _items;

        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException();

            _items = new();
            _itemsMatrix = new Item[width, height];
        }

        public Inventory(in int width,
                         in int height,
                         params KeyValuePair<Item, Vector2Int>[] items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (var item in items)
                AddItem(item.Key, item.Value);
        }

        public Inventory(in int width,
                         in int height,
                         params Item[] items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (var item in items)
                AddItem(item);
        }

        public Inventory(in int width,
                         in int height,
                         in IEnumerable<KeyValuePair<Item, Vector2Int>> items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (var item in items)
                AddItem(item.Key, item.Value);
        }

        public Inventory(in int width,
                         in int height,
                         in IEnumerable<Item> items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();

            foreach (var item in items)
                AddItem(item);
        }

        public int Width => _itemsMatrix.GetLength(ColumnIndex);
        public int Height => _itemsMatrix.GetLength(RowIndex);
        public int Count => _items.Count;

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (!IsItemValid(item))
                return false;

            if (!IsPositionWithSizeValid(item.Size, position))
                return false;

            return IsPositionFree(item.Size, position);
        }

        public bool CanAddItem(in Item item, in int posX, in int posY)
        {
            return CanAddItem(item, new(posX, posY));
        }

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
        {
            if (!CanAddItem(item, position))
                return false;

            AddItemWithoutChecks(item, position);

            OnAdded?.Invoke(item, position);

            return true;
        }

        private void AddItemWithoutChecks(in Item item, in Vector2Int position)
        {
            for (int i = position.x; i < position.x + item.Size.x; i++)
            {
                for (int j = position.y; j < position.y + item.Size.y; j++)
                {
                    _itemsMatrix[i, j] = item;
                }
            }

            _items.Add(item, position);
        }

        public bool AddItem(in Item item, in int posX, in int posY)
        {
            return AddItem(item, new(posX, posY));
        }

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
        {
            if (!IsItemValid(item))
                return false;

            return FindFreePosition(item.Size, out Vector2Int position);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (!IsItemValid(item))
                return false;

            if (FindFreePosition(item.Size, out Vector2Int position))
                return AddItem(item, position);

            return false;
        }

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
        {
            freePosition = default;

            if (!IsSizeValid(size))
                throw new ArgumentOutOfRangeException();

            if (!IsPositionWithSizeValid(size, freePosition))
                return false;

            return FindFreePositionWithoutChecks(size, out freePosition);
        }

        private bool FindFreePositionWithoutChecks(in Vector2Int size, out Vector2Int freePosition)
        {
            for (int i = 0; i <= Height - size.y; i++)
            {
                for (int j = 0; j <= Width - size.x; j++)
                {
                    freePosition = new(j, i);

                    if (IsPositionFree(size, freePosition))
                        return true;
                }
            }

            freePosition = default;
            return false;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return item == null ? false : _items.ContainsKey(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return _itemsMatrix[position.x, position.y] != null;
        }

        public bool IsOccupied(in int x, in int y)
        {
            return _itemsMatrix[x, y] != null;
        }

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return _itemsMatrix[position.x, position.y] == null;
        }

        public bool IsFree(in int x, in int y)
        {
            return _itemsMatrix[x, y] == null;
        }

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
        {
            return RemoveItem(item, out Vector2Int position);
        }

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = default;

            if (item == null)
                return false;

            if (!Contains(item))
                return false;

            RemoveItemWithoutChecks(item, out position);

            OnRemoved?.Invoke(item, position);

            return true;
        }

        private void RemoveItemWithoutChecks(in Item item, out Vector2Int position)
        {
            position = _items[item];

            for (int i = position.x; i < position.x + item.Size.x; i++)
            {
                for (int j = position.y; j < position.y + item.Size.y; j++)
                {
                    _itemsMatrix[i, j] = default;
                }
            }

            _items.Remove(item);
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (!IsPositionValid(position))
                throw new IndexOutOfRangeException();

            var item = _itemsMatrix[position.x, position.y];

            if (item == null)
                throw new NullReferenceException();

            return item;
        }

        public Item GetItem(in int x, in int y)
        {
            return GetItem(new Vector2Int(x, y));
        }

        public bool TryGetItem(in Vector2Int position, out Item item)
        {
            item = default;

            if (!IsPositionValid(position))
                return false;

            item = _itemsMatrix[position.x, position.y];

            return item != null;
        }

        public bool TryGetItem(in int x, in int y, out Item item)
        {
            return TryGetItem(new Vector2Int(x, y), out item);
        }

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (!Contains(item))
                throw new KeyNotFoundException();

            int index = 0;
            Vector2Int position = _items[item];

            var positions = new Vector2Int[item.Size.x * item.Size.y];

            for (int i = position.x; i < position.x + item.Size.x; i++)
            {
                for (int j = position.y; j < position.y + item.Size.y; j++)
                {
                    positions[index++] = new Vector2Int(i, j);
                }
            }

            return positions;
        }

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
        {
            positions = default;

            if (item == null)
                return false;

            if (!Contains(item))
                return false;

            positions = GetPositions(item);
            return true;
        }

        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
        {
            if (Count == 0)
                return;

            Array.Clear(_itemsMatrix, 0, _itemsMatrix.Length);

            _items.Clear();

            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;

            foreach (var item in _items)
            {
                if (item.Key.Name == name)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Moves a specified item to a target position if it exists
        /// </summary>
        public bool MoveItem(in Item item, in Vector2Int newPosition)
        {
            if (item == null)
                throw new ArgumentNullException();

            if (!Contains(item))
                return false;

            RemoveItemWithoutChecks(item, out Vector2Int position);

            if (CanAddItem(item, newPosition))
            {
                position = newPosition;
                OnMoved?.Invoke(item, position);
            }

            AddItemWithoutChecks(item, position);

            return position == newPosition;
        }

        /// <summary>
        /// Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
        {
            var orderedItems = _items.OrderByDescending(item => item.Key.Size.x * item.Key.Size.y)
                                     .ThenBy(item => item.Key.Name)
                                     .ToArray();

            Array.Clear(_itemsMatrix, 0, _itemsMatrix.Length);

            _items.Clear();

            foreach (var item in orderedItems)
            {
                FindFreePositionWithoutChecks(item.Key.Size, out Vector2Int position);
                AddItemWithoutChecks(item.Key, position);
            }
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            Array.Copy(_itemsMatrix, matrix, _itemsMatrix.Length);
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return _items.Keys.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        #region Checks

        private bool IsSizeValid(Vector2Int size)
        {
            return size.x > 0 && size.y > 0;
        }

        private bool IsPositionWithSizeValid(in Vector2Int size, in Vector2Int position)
        {
            return position.x >= 0
                   && position.y >= 0
                   && position.x + size.x <= Width
                   && position.y + size.y <= Height;
        }

        private bool IsPositionValid(in Vector2Int position)
        {
            return position.x >= 0
                   && position.y >= 0
                   && position.x < Width
                   && position.y < Height;
        }

        public bool IsItemValid(Item item)
        {
            if (item == null)
                return false;

            if (Contains(item))
                return false;

            if (!IsSizeValid(item.Size))
                throw new ArgumentException();

            return true;
        }

        private bool IsPositionFree(in Vector2Int size, in Vector2Int position)
        {
            for (int i = position.x; i < position.x + size.x; i++)
            {
                for (int j = position.y; j < position.y + size.y; j++)
                {
                    if (_itemsMatrix[i, j] != null)
                        return false;
                }
            }

            return true;
        }

        #endregion
    }
}