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
        private const int RowIndex = 0;
        private const int ColumnIndex = 1;

        private Item[,] _items;

        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public int Width => _items.GetLength(ColumnIndex);
        public int Height => _items.GetLength(RowIndex);
        public int Count => this.ToHashSet().Count;

        public Inventory(in int width, in int height)
        {
            if (width <= 0 || height <= 0)
                throw new ArgumentOutOfRangeException();

            _items = new Item[height, width];
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
        }

        public Inventory(in int width,
                         in int height,
                         in IEnumerable<KeyValuePair<Item, Vector2Int>> items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();
        }

        public Inventory(in int width,
                         in int height,
                         in IEnumerable<Item> items) : this(width, height)
        {
            if (items == null)
                throw new ArgumentNullException();
        }

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
        {
            if (item == null)
                return false;

            if (Contains(item))
                return false;

            if (!IsSizeValid(item.Size))
                throw new ArgumentException();

            if (!IsPositionWithSizeValid(item.Size, position))
                return false;

            for (int i = position.y; i < position.y + item.Size.y; i++)
            {
                for (int j = position.x; j < position.x + item.Size.x; j++)
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

            for (int i = position.y; i < position.y + item.Size.y; i++)
            {
                for (int j = position.x; j < position.x + item.Size.x; j++)
                {
                    _items[i, j] = item;
                }
            }

            OnAdded?.Invoke(item, position);

            return true;
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
            if (item == null)
                return false;

            if (Contains(item))
                return false;

            if (!IsSizeValid(item.Size))
                throw new ArgumentException();

            return FindFreePosition(item.Size, out Vector2Int position);
        }

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
        {
            if (item == null)
                return false;

            if (Contains(item))
                return false;

            if (!IsSizeValid(item.Size))
                throw new ArgumentException();

            if (FindFreePosition(item.Size, out Vector2Int position))
            {
                AddItem(item, position);
                return true;
            }

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

        private bool IsPositionFree(in Vector2Int size, in Vector2Int position)
        {
            for (int i = position.y; i < position.y + size.y; i++)
            {
                for (int j = position.x; j < position.x + size.x; j++)
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
            return this.ToHashSet().Contains(item);
        }

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
        {
            return _items[position.y, position.x] != null;
        }

        public bool IsOccupied(in int x, in int y)
        {
            return _items[y, x] != null;
        }

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
        {
            return _items[position.y, position.x] == null;
        }

        public bool IsFree(in int x, in int y)
        {
            return _items[y, x] == null;
        }

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
            => throw new NotImplementedException();

        public bool RemoveItem(in Item item, out Vector2Int position)
        {
            position = default;
            var positionSet = false;

            if (item == null)
                return false;

            if (!Contains(item))
                return false;

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (_items[i, j] == item)
                    {
                        if (!positionSet)
                        {
                            position = new(j, i);
                            positionSet = true;
                        }

                        _items[i, j] = default;
                    }
                }
            }

            OnRemoved?.Invoke(item, position);

            return true;
        }

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
        {
            if (!IsPositionValid(position))
                throw new IndexOutOfRangeException();

            var item = _items[position.y, position.x];

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

            item = _items[position.y, position.x];

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
                    if (_items[j, i] == item)
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

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _items[i, j] = default;
                }
            }

            OnCleared?.Invoke();
        }

        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
        {
            int count = 0;

            foreach (var item in this)
            {
                if (item.Name == name)
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
            => throw new NotImplementedException();

        /// <summary>
        /// Reorganizes inventory space to make the free area uniform
        /// </summary>
        public void ReorganizeSpace()
            => throw new NotImplementedException();

        /// <summary>
        /// Copies inventory items to a specified matrix
        /// </summary>
        public void CopyTo(in Item[,] matrix)
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _items[i, j] = matrix[i, j];
                }
            }

        }

        public IEnumerator<Item> GetEnumerator()
        {
            List<Item> items = new();

            foreach (var item in _items)
            {
                if (item != null && !items.Contains(item))
                {
                    items.Add(item);
                }
            }

            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}