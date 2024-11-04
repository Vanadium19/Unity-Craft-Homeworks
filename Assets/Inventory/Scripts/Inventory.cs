using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable NotResolvedInText

namespace Inventories
{
    public sealed class Inventory : IEnumerable<Item>
    {
        public event Action<Item, Vector2Int> OnAdded;
        public event Action<Item, Vector2Int> OnRemoved;
        public event Action<Item, Vector2Int> OnMoved;
        public event Action OnCleared;

        public int Width => throw new NotImplementedException();
        public int Height => throw new NotImplementedException();
        public int Count => throw new NotImplementedException();

        public Inventory(in int width, in int height)
            => throw new NotImplementedException();

        public Inventory(
            in int width,
            in int height,
            params KeyValuePair<Item, Vector2Int>[] items
        ) : this(width, height) => throw new NotImplementedException();

        public Inventory(
            in int width,
            in int height,
            params Item[] items
        ) : this(width, height) => throw new NotImplementedException();

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<KeyValuePair<Item, Vector2Int>> items
        ) : this(width, height) => throw new NotImplementedException();

        public Inventory(
            in int width,
            in int height,
            in IEnumerable<Item> items
        ) : this(width, height) => throw new NotImplementedException();

        /// <summary>
        /// Checks for adding an item on a specified position
        /// </summary>
        public bool CanAddItem(in Item item, in Vector2Int position)
            => throw new NotImplementedException();

        public bool CanAddItem(in Item item, in int posX, in int posY)
            => throw new NotImplementedException();

        /// <summary>
        /// Adds an item on a specified position if not exists
        /// </summary>
        public bool AddItem(in Item item, in Vector2Int position)
            => throw new NotImplementedException();

        public bool AddItem(in Item item, in int posX, in int posY)
            => throw new NotImplementedException();

        /// <summary>
        /// Checks for adding an item on a free position
        /// </summary>
        public bool CanAddItem(in Item item)
            => throw new NotImplementedException();

        /// <summary>
        /// Adds an item on a free position
        /// </summary>
        public bool AddItem(in Item item)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns a free position for a specified item
        /// </summary>
        public bool FindFreePosition(in Vector2Int size, out Vector2Int freePosition)
            => throw new NotImplementedException();
        
        /// <summary>
        /// Checks if a specified item exists
        /// </summary>
        public bool Contains(in Item item)
            => throw new NotImplementedException();

        /// <summary>
        /// Checks if a specified position is occupied
        /// </summary>
        public bool IsOccupied(in Vector2Int position)
            => throw new NotImplementedException();

        public bool IsOccupied(in int x, in int y)
            => throw new NotImplementedException();

        /// <summary>
        /// Checks if a position is free
        /// </summary>
        public bool IsFree(in Vector2Int position)
            => throw new NotImplementedException();

        public bool IsFree(in int x, in int y)
            => throw new NotImplementedException();

        /// <summary>
        /// Removes a specified item if exists
        /// </summary>
        public bool RemoveItem(in Item item)
            => throw new NotImplementedException();

        public bool RemoveItem(in Item item, out Vector2Int position)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns an item at specified position 
        /// </summary>
        public Item GetItem(in Vector2Int position)
            => throw new NotImplementedException();

        public Item GetItem(in int x, in int y)
            => throw new NotImplementedException();

        public bool TryGetItem(in Vector2Int position, out Item item)
            => throw new NotImplementedException();

        public bool TryGetItem(in int x, in int y, out Item item)
            => throw new NotImplementedException();

        /// <summary>
        /// Returns matrix positions of a specified item 
        /// </summary>
        public Vector2Int[] GetPositions(in Item item)
            => throw new NotImplementedException();

        public bool TryGetPositions(in Item item, out Vector2Int[] positions)
            => throw new NotImplementedException();
        
        /// <summary>
        /// Clears all inventory items
        /// </summary>
        public void Clear()
            => throw new NotImplementedException();
        
        /// <summary>
        /// Returns a count of items with a specified name
        /// </summary>
        public int GetItemCount(string name)
            => throw new NotImplementedException();

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
            => throw new NotImplementedException();

        public IEnumerator<Item> GetEnumerator()
            => throw new NotImplementedException();

        IEnumerator IEnumerable.GetEnumerator()
            => throw new NotImplementedException();
    }
}