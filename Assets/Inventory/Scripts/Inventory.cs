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

        private Item[,] _items;
        private HashSet<Item> _itemsHashSet;

        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException();

            _itemsHashSet = new();
            _items = new Item[width, height];
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

        public int Width => _items.GetLength(ColumnIndex);
        public int Height => _items.GetLength(RowIndex);
        public int Count => _itemsHashSet.Count;

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (!IsItemValid(item))
                return false;

            if (!IsPositionWithSizeValid(item.Size, position))
                return false;

            for (int i = position.x; i < position.x + item.Size.x; i++)
            {
                for (int j = position.y; j < position.y + item.Size.y; j++)
                {
                    if (_items[i, j] != null)
                        return false;
                }
            }

            return true;
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
                    _items[i, j] = item;
                }
            }

            _itemsHashSet.Add(item);
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

            while (!IsPositionFree(size, freePosition))
            {
                freePosition = new(++freePosition.x, freePosition.y);

                if (freePosition.x + size.x > Width)
                {
                    freePosition = new(0, ++freePosition.y);

                    if (freePosition.y + size.y > Height)
                    {
                        freePosition = default;
                        return false;
                    }
                }
            }

            return true;
        }

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
                    if (_items[i, j] != null)
                        return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
        {
            return _itemsHashSet.Contains(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return _items[position.x, position.y] != null;
        }

        public bool IsOccupied(in int x, in int y)
        {
            return _items[x, y] != null;
        }

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return _items[position.x, position.y] == null;
        }

        public bool IsFree(in int x, in int y)
        {
            return _items[x, y] == null;
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
            position = _invalidPosition;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (_items[i, j] == item)
                    {
                        if (position == _invalidPosition)
                            position = new(i, j);

                        _items[i, j] = default;
                    }
                }
            }

            _itemsHashSet.Remove(item);
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (!IsPositionValid(position))
                throw new IndexOutOfRangeException();

            var item = _items[position.x, position.y];

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

            item = _items[position.x, position.y];

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
            var positions = new Vector2Int[item.Size.x * item.Size.y];

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (_items[i, j] == item)
                    {
                        positions[index] = new Vector2Int(i, j);
                        index++;
                    }
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

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _items[i, j] = default;
                }
            }

            _itemsHashSet.Clear();

            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            return _itemsHashSet.Where(item => item.Name == name).Count();
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
            var orderedItems = _itemsHashSet.OrderByDescending(item => item.Size.x * item.Size.y)
                                            .ThenBy(item => item.Name)
                                            .ToArray();

            Clear();

            foreach (var item in orderedItems)
                AddItem(item);
        }

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    matrix[i, j] = _items[i, j];
                }
            }
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return _itemsHashSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _itemsHashSet.GetEnumerator();
        }
    }
}