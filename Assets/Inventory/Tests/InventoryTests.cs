using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

// ReSharper disable ExpressionIsAlwaysNull

namespace Inventories
{
    /// Don't modify
    public sealed class InventoryTests
    {
        [TestCaseSource(nameof(InstantiateWithItemsCases))]
        public void InstantiateWithItems(int width, int height, KeyValuePair<Item, Vector2Int>[] items)
        {
        Arrange:
            var inventory = new Inventory(width, height, items);

        Assert:
            Assert.AreEqual(width, inventory.Width);
            Assert.AreEqual(height, inventory.Height);

            Assert.AreEqual(items.Length, inventory.Count);
            Assert.AreEqual(items.Select(it => it.Key).ToHashSet(), inventory.ToHashSet());
        }

        private static IEnumerable<TestCaseData> InstantiateWithItemsCases()
        {
            yield return new TestCaseData(10, 10, new[]
            {
                new KeyValuePair<Item, Vector2Int>(new Item("A", 10, 10), new Vector2Int(0, 0)),
            }).SetName("Full item");

            yield return new TestCaseData(4, 4, new[]
            {
                new KeyValuePair<Item, Vector2Int>(new Item("A", 1, 2), new Vector2Int(0, 0)),
                new KeyValuePair<Item, Vector2Int>(new Item("B", 1, 1), new Vector2Int(0, 3)),
                new KeyValuePair<Item, Vector2Int>(new Item("C", 3, 2), new Vector2Int(1, 2)),
                new KeyValuePair<Item, Vector2Int>(new Item("D", 2, 1), new Vector2Int(2, 0))
            }).SetName("Half filling");

            yield return new TestCaseData(4, 4, new[]
            {
                new KeyValuePair<Item, Vector2Int>(new Item("A", 2, 2), new Vector2Int(0, 0)),
                new KeyValuePair<Item, Vector2Int>(new Item("B", 2, 2), new Vector2Int(2, 0)),
                new KeyValuePair<Item, Vector2Int>(new Item("C", 2, 2), new Vector2Int(0, 2)),
                new KeyValuePair<Item, Vector2Int>(new Item("D", 2, 2), new Vector2Int(2, 2))
            }).SetName("Full dense");
        }

        [TestCase(5, 10)]
        [TestCase(3, 2)]
        [TestCase(1, 100)]
        [TestCase(255, 1)]
        public void Instantiate(int width, int height)
        {
            //Act:
            var inventory = new Inventory(width, height);

            //Assert:
            Assert.AreEqual(width, inventory.Width);
            Assert.AreEqual(height, inventory.Height);
            Assert.AreEqual(0, inventory.Count);

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    Assert.IsTrue(inventory.IsFree(x, y));
                }
        }

        [Test]
        public void WhenInstantiateWithNullItemsThenException()
        {
            //Assert:
            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Inventory(5, 3, (IEnumerable<KeyValuePair<Item, Vector2Int>>)null);
            });

            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Inventory(5, 3, (KeyValuePair<Item, Vector2Int>[])null);
            });

            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Inventory(5, 3, (IEnumerable<Item>)null);
            });

            Assert.Catch<ArgumentNullException>(() =>
            {
                var _ = new Inventory(5, 3, (Item[])null);
            });
        }

        [TestCase(-1, 10)]
        [TestCase(2, -1)]
        [TestCase(-10, -100)]
        [TestCase(0, 0)]
        [TestCase(10, 0)]
        [TestCase(0, 10)]
        public void WhenInstantiateWithInvalidSizeThenException(int width, int height)
        {
            //Assert:
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var _ = new Inventory(width, height);
            });
        }

        [TestCaseSource(nameof(CanAddOnSpecifiedPositionCases))]
        public bool CanAddOnSpecifiedPosition(Inventory inventory, Item item, Vector2Int position)
        {
            return inventory.CanAddItem(item, position);
        }

        private static IEnumerable<TestCaseData> CanAddOnSpecifiedPositionCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(2, 2)),
                new Vector2Int(0, 0)
            ).Returns(true).SetName("Empty Inventory");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(3, 3))
                ),
                new Item("A", new Vector2Int(2, 2)),
                new Vector2Int(0, 0)
            ).Returns(true).SetName("Free Slot");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 2), new Vector2Int(3, 3))
                ),
                new Item("A", new Vector2Int(3, 3)),
                new Vector2Int(1, 1)
            ).Returns(false).SetName("Intersects");

            var item = new Item("X", 1, 1);
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(item, new Vector2Int(3, 3))
                ),
                item,
                new Vector2Int(0, 0)
            ).Returns(false).SetName("Already Exists");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                null,
                new Vector2Int(0, 0)
            ).Returns(false).SetName("Item is null");
        }

        [TestCaseSource(nameof(AddOnSpecifiedPositionSuccessfulCases))]
        public void AddOnSpecifiedPositionSuccessful(Inventory inventory, Item item, Vector2Int position)
        {
            //Arrange:
            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            int count = inventory.Count;

            //Pre-assert:
            for (int x = position.x; x < position.x + item.Size.x; x++)
                for (int y = position.y; y < position.y + item.Size.y; y++)
                {
                    Assert.IsTrue(inventory.IsFree(x, y));
                }

            //Act:
            bool success = inventory.AddItem(item, position);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(count + 1, inventory.Count);
            Assert.AreEqual(item, addedItem);
            Assert.AreEqual(addedPosition, position);
            Assert.IsTrue(inventory.Contains(item));

            for (int x = position.x; x < position.x + item.Size.x; x++)
                for (int y = position.y; y < position.y + item.Size.y; y++)
                    Assert.IsTrue(inventory.IsOccupied(x, y));
        }

        private static IEnumerable<TestCaseData> AddOnSpecifiedPositionSuccessfulCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(2, 2)),
                new Vector2Int(0, 0)
            ).SetName("(2, 2) at (0, 0)");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(2, 2)),
                new Vector2Int(3, 3)
            ).SetName("(2, 2) at (3, 3)");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(5, 5)),
                new Vector2Int(0, 0)
            ).SetName("Full Item");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item(new Vector2Int(5, 5)),
                new Vector2Int(0, 0)
            ).SetName("Without name");
        }

        [Test]
        public void WhenAddNullItemOnSpecifiedPositionThenFalse()
        {
            //Arrange:
            var inventory = new Inventory(5, 5);

            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            //Act:
            bool success = inventory.AddItem(null, 0, 0);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(0, inventory.Count);
            Assert.IsNull(addedItem);
            Assert.AreEqual(Vector2Int.zero, addedPosition);
            Assert.IsTrue(inventory.IsFree(0, 0));
        }

        [TestCaseSource(nameof(WhenAddItemOnOutOfBoundsPositionThenExceptionCases))]
        public void WhenAddItemOutOfRangePositionThenFalse(Inventory inventory, Item item,
            Vector2Int position)
        {
            //Arrange:
            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            //Act:
            bool success = inventory.AddItem(item, position);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(Vector2Int.zero, addedPosition);
            Assert.IsNull(addedItem);
        }

        private static IEnumerable<TestCaseData> WhenAddItemOnOutOfBoundsPositionThenExceptionCases()
        {
            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(-1, 0)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (-1, 0)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(-1, -1)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (-1, -1)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(0, -1)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (0, -1)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(5, 5)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (5, 5)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(5, 0)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (5, 0)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 1),
                new Vector2Int(0, 5)
            ).SetName("Inventory (5, 5); Item: (1, 1); Position: (0, 5)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item("A", 1, 2),
                new Vector2Int(0, 4)
            ).SetName("Inventory (5, 5); Item: (1, 2); Position: (0, 4)");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Item(2, 3),
                new Vector2Int(3, 3)
            ).SetName("Inventory (5, 5); Item: (2, 3); Position: (3, 3)");
        }

        [Test]
        public void WhenAddItemOnSpecifiedPositionThatAlreadyExistsThenException()
        {
            //Arrange:
            Item item = new Item(1, 1);
            Inventory inventory = new Inventory(5, 5);
            inventory.AddItem(item, 3, 3);

            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            //Pre-assert:
            Assert.IsTrue(inventory.Contains(item));
            Assert.IsTrue(inventory.IsFree(0, 0));

            //Act:
            bool success = inventory.AddItem(item, 0, 0);

            Assert.IsFalse(success);
            Assert.IsNull(addedItem);
            Assert.AreEqual(Vector2Int.zero, addedPosition);

            Assert.AreEqual(1, inventory.Count);
            Assert.IsTrue(inventory.IsFree(0, 0));
        }

        [TestCaseSource(nameof(WhenAddItemOnSpecifiedPositionThatIntersectsCases))]
        public void WhenAddItemOnSpecifiedPositionThatIntersectsWithAnotherThenFalse(
            Inventory inventory,
            Item item,
            Vector2Int position
        )
        {
            //Arrange:
            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            //Act:
            bool success = inventory.AddItem(new Item(2, 2), 2, 2);

            Assert.IsFalse(success);
            Assert.IsNull(addedItem);
            Assert.AreEqual(Vector2Int.zero, addedPosition);
            Assert.IsFalse(inventory.Contains(item));
        }

        private static IEnumerable<TestCaseData> WhenAddItemOnSpecifiedPositionThatIntersectsCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(3, 3))
                ),
                new Item("Y", 2, 2),
                new Vector2Int(2, 2)
            ).SetName("Side Intersect");

            yield return new TestCaseData(
                new Inventory(width: 3, height: 3,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 2), new Vector2Int(1, 1))
                ),
                new Item("Y", 2, 2),
                new Vector2Int(1, 1)
            ).SetName("Full Intersect");
        }

        [TestCaseSource(nameof(ContainsCases))]
        public bool Contains(Inventory inventory, Item item)
        {
            return inventory.Contains(item);
        }

        private static IEnumerable<TestCaseData> ContainsCases()
        {
            yield return ContainsTrueCase();
            yield return ContainsFalseCase();
            yield return ContainsWhenInventoryIsEmptyCase();
            yield return ContainsWhenItemIsNullCase();
        }

        private static TestCaseData ContainsTrueCase()
        {
            var item = new Item("X", width: 2, height: 2);
            Inventory inventory = new Inventory(5, 5,
                new KeyValuePair<Item, Vector2Int>(item,
                    Vector2Int.zero)
            );
            return new TestCaseData(inventory, item).Returns(true).SetName("True");
        }

        private static TestCaseData ContainsFalseCase()
        {
            Inventory inventory = new Inventory(5, 5,
                new KeyValuePair<Item, Vector2Int>(new Item("B", 2, 2), Vector2Int.zero)
            );
            return new TestCaseData(inventory, new Item("C", 2, 2)).Returns(false).SetName("False");
        }

        private static TestCaseData ContainsWhenInventoryIsEmptyCase()
        {
            Inventory inventory = new Inventory(5, 5);
            return new TestCaseData(inventory, new Item("B", 2, 2)).Returns(false)
                .SetName("Inventory is empty");
        }

        private static TestCaseData ContainsWhenItemIsNullCase()
        {
            Inventory inventory = new Inventory(5, 5);
            return new TestCaseData(inventory, null).Returns(false).SetName("Item is null");
        }

        [TestCaseSource(nameof(GetFreePositionCases))]
        public bool GetFreePosition(Inventory inventory, Vector2Int size, Vector2Int expectedPosition)
        {
            //Act:
            bool success = inventory.FindFreePosition(size, out Vector2Int actualPosition);

            //Assert:
            Assert.AreEqual(expectedPosition, actualPosition);
            return success;
        }

        private static IEnumerable<TestCaseData> GetFreePositionCases()
        {
            yield return new TestCaseData(
                new Inventory(5, 5),
                new Vector2Int(2, 2),
                new Vector2Int(0, 0)
            ).Returns(true).SetName("Inventory is empty");

            yield return new TestCaseData(
                new Inventory(5, 5),
                new Vector2Int(5, 5),
                new Vector2Int(0, 0)
            ).Returns(true).SetName("Full item");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(1, 1))
                ),
                new Vector2Int(3, 3),
                new Vector2Int(2, 0)
            ).Returns(true).SetName("Right Ordering");

            yield return new TestCaseData(
                new Inventory(5, 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 3, 3), new Vector2Int(1, 1))
                ),
                new Vector2Int(2, 2),
                new Vector2Int()
            ).Returns(false).SetName("Middle Item");

            yield return new TestCaseData(
                new Inventory(5, 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 3, 5), new Vector2Int(0, 0)),
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 3), new Vector2Int(3, 0))
                ),
                new Vector2Int(2, 2),
                new Vector2Int(3, 3)
            ).Returns(true).SetName("Top Right Corner Space");


            yield return new TestCaseData(
                new Inventory(5, 5),
                new Vector2Int(6, 6),
                new Vector2Int(0, 0)
            ).Returns(false).SetName("Item size is bigger than inventory");
        }

        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -2)]
        [TestCase(-2, -2)]
        [TestCase(0, 10)]
        [TestCase(5, 0)]
        public void WhenGetFreePositionWithInvalidSizeThenException(int width, int height)
        {
            //Arrange:
            var inventory = new Inventory(5, 5);

            //Assert:
            Assert.Catch<ArgumentOutOfRangeException>(() =>
                inventory.FindFreePosition(new Vector2Int(width, height), out _));
        }

        [TestCaseSource(nameof(CanAddOnFreePositionCases))]
        public bool CanAddOnFreePosition(Inventory inventory, Item item)
        {
            return inventory.CanAddItem(item);
        }

        private static IEnumerable<TestCaseData> CanAddOnFreePositionCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(2, 2))
            ).Returns(true).SetName("Empty Inventory");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(3, 3))
                ),
                new Item("A", new Vector2Int(2, 2))
            ).Returns(true).SetName("Free Slot");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 2), new Vector2Int(2, 2))
                ),
                new Item("A", new Vector2Int(3, 3))
            ).Returns(false).SetName("Intersects");

            var item = new Item("X", 1, 1);
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(item, new Vector2Int(3, 3))
                ),
                item
            ).Returns(false).SetName("Already Exists");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                null
            ).Returns(false).SetName("Item is null");
        }

        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -2)]
        [TestCase(-2, -2)]
        [TestCase(0, 10)]
        [TestCase(5, 0)]
        public void WhenCanAddItemOnFreePositionWithInvalidSizeThenException(int width, int height)
        {
            //Arrange:
            var inventory = new Inventory(5, 5);
            var item = new Item(width, height);

            //Assert:
            Assert.Catch<ArgumentException>(() => inventory.CanAddItem(item));
        }

        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -2)]
        [TestCase(-2, -2)]
        [TestCase(0, 10)]
        [TestCase(5, 0)]
        public void WhenCanAddItemOnSpecifiedPositionWithInvalidSizeThenException(int width, int height)
        {
            //Arrange:
            var inventory = new Inventory(5, 5);
            var item = new Item(width, height);

            //Assert:
            Assert.Catch<ArgumentException>(() => inventory.CanAddItem(item, Vector2Int.zero));
        }

        [TestCaseSource(nameof(AddOnFreePositionSuccessfulCases))]
        public void AddOnFreePositionSuccessful(
            Inventory inventory,
            Item item,
            Vector2Int expectedPosition
        )
        {
            //Arrange:
            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };
            int count = inventory.Count;

            //Pre-assert:
            for (int x = expectedPosition.x; x < expectedPosition.x + item.Size.x; x++)
                for (int y = expectedPosition.y; y < expectedPosition.y + item.Size.y; y++)
                {
                    Assert.IsTrue(inventory.IsFree(x, y));
                }

            //Act:
            bool success = inventory.AddItem(item);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(item, addedItem);
            Assert.AreEqual(expectedPosition, addedPosition);

            Assert.AreEqual(count + 1, inventory.Count);
            Assert.IsTrue(inventory.Contains(item));

            for (int x = expectedPosition.x; x < expectedPosition.x + item.Size.x; x++)
                for (int y = expectedPosition.y; y < expectedPosition.y + item.Size.y; y++)
                {
                    Assert.IsTrue(inventory.IsOccupied(x, y));
                }
        }

        private static IEnumerable<TestCaseData> AddOnFreePositionSuccessfulCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(2, 2)),
                new Vector2Int()
            ).SetName("Empty Inventory");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 1, 1), new Vector2Int(1, 1))
                ),
                new Item("A", new Vector2Int(3, 3)),
                new Vector2Int(2, 0)
            ).SetName("Free Slot");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item("A", new Vector2Int(5, 5)),
                new Vector2Int(0, 0)
            ).SetName("Full Item");


            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                new Item(new Vector2Int(5, 5)),
                new Vector2Int(0, 0)
            ).SetName("Without name");
        }

        [TestCaseSource(nameof(AddOnFreePositionFailedCases))]
        public void AddOnFreePositionFailed(Inventory inventory, Item item)
        {
            //Arrange:
            Item addedItem = null;
            Vector2Int addedPosition = Vector2Int.zero;

            inventory.OnAdded += (i, p) =>
            {
                addedItem = i;
                addedPosition = p;
            };

            //Act:
            bool success = inventory.AddItem(item);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(addedItem);
            Assert.AreEqual(Vector2Int.zero, addedPosition);
        }

        private static IEnumerable<TestCaseData> AddOnFreePositionFailedCases()
        {
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 2), new Vector2Int(2, 2))
                ),
                new Item("A", new Vector2Int(3, 3))
            ).SetName("Intersects");

            var item = new Item("X", 1, 1);
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(item, new Vector2Int(3, 3))
                ),
                item
            ).SetName("Already Exists");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5),
                null
            ).SetName("Item is null");
        }

        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -2)]
        [TestCase(-2, -2)]
        [TestCase(0, 10)]
        [TestCase(5, 0)]
        public void WhenAddOnFreePositionItemWithInvalidSizeThenException(int width, int height)
        {
            //Arrange:
            var inventory = new Inventory(5, 5);
            var item = new Item(width, height);

            //Assert:
            Assert.Catch<ArgumentException>(() => inventory.AddItem(item));
        }


        [TestCase(0, 0)]
        [TestCase(-1, 10)]
        [TestCase(10, -2)]
        [TestCase(-2, -2)]
        [TestCase(0, 10)]
        [TestCase(5, 0)]
        public void WhenAddOnSpecifiedPositionItemWithInvalidSizeThenException(int width, int height)
        {
            //Arrange:
            var inventory = new Inventory(5, 5);
            var item = new Item(width, height);

            //Assert:
            Assert.Catch<ArgumentException>(() => inventory.AddItem(item, Vector2Int.zero));
        }

        [TestCaseSource(nameof(RemoveFailedCases))]
        public void RemoveFailed(Inventory inventory, Item item)
        {
            //Arrange:
            Item removedItem = null;
            Vector2Int removedPosition = Vector2Int.zero;

            inventory.OnRemoved += (i, p) =>
            {
                removedItem = i;
                removedPosition = p;
            };

            //Act:
            bool success = inventory.RemoveItem(item, out Vector2Int actualPosition);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(removedItem);
            Assert.AreEqual(Vector2Int.zero, removedPosition);
            Assert.AreEqual(Vector2Int.zero, actualPosition);
        }

        private static IEnumerable<TestCaseData> RemoveFailedCases()
        {
            yield return new TestCaseData(
                new Inventory(5, 5),
                null
            ).SetName("Item is null");

            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(new Item("X", 2, 2), new Vector2Int(2, 2))
                ),
                new Item("Y", 5, 5)
            ).SetName("Item is absent!");
        }

        [TestCaseSource(nameof(RemoveSuccessfulCases))]
        public void RemoveSuccessful(
            Inventory inventory,
            Item item,
            Vector2Int expectedPosition
        )
        {
            //Arrange:
            Item removedItem = null;
            Vector2Int removedPosition = Vector2Int.zero;

            inventory.OnRemoved += (i, p) =>
            {
                removedItem = i;
                removedPosition = p;
            };
            int count = inventory.Count;

            //Pre-assert:
            Assert.IsTrue(inventory.Contains(item));

            for (int x = expectedPosition.x; x < expectedPosition.x + item.Size.x; x++)
                for (int y = expectedPosition.y; y < expectedPosition.y + item.Size.y; y++)
                {
                    Assert.IsTrue(inventory.IsOccupied(x, y));
                }

            //Act:
            bool success = inventory.RemoveItem(item, out Vector2Int actualPosition);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(item, removedItem);
            Assert.AreEqual(expectedPosition, removedPosition);
            Assert.AreEqual(expectedPosition, actualPosition);

            Assert.AreEqual(count - 1, inventory.Count);
            Assert.IsFalse(inventory.Contains(item));

            for (int x = expectedPosition.x; x < expectedPosition.x + item.Size.x; x++)
                for (int y = expectedPosition.y; y < expectedPosition.y + item.Size.y; y++)
                {
                    Assert.IsTrue(inventory.IsFree(x, y));
                }
        }

        private static IEnumerable<TestCaseData> RemoveSuccessfulCases()
        {
            Item item1 = new Item("X", width: 2, height: 2);
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(item1,
                        new Vector2Int(2, 2))
                ),
                item1,
                new Vector2Int(2, 2)
            ).SetName("Case 1");

            Item item2 = new Item("X", width: 3, height: 2);
            yield return new TestCaseData(
                new Inventory(width: 5, height: 5,
                    new KeyValuePair<Item, Vector2Int>(item2,
                        new Vector2Int(1, 2)),
                    new KeyValuePair<Item, Vector2Int>(new Item("D", width: 1, height: 2),
                        new Vector2Int(4, 0))
                ),
                item2,
                new Vector2Int(1, 2)
            ).SetName("Case 2");
        }

        [TestCaseSource(nameof(GetCases))]
        public Item Get(Inventory inventory, Vector2Int position)
        {
            return inventory.GetItem(position);
        }

        private static IEnumerable<TestCaseData> GetCases()
        {
            var item1 = new Item("D", 1, 2);
            var item2 = new Item("X", 3, 2);

            var inventory = new Inventory(width: 5, height: 5,
                new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(1, 2)),
                new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(4, 0))
            );

            yield return new TestCaseData(inventory, new Vector2Int(1, 2))
                .Returns(item2).SetName("(1, 2)");

            yield return new TestCaseData(inventory, new Vector2Int(1, 3))
                .Returns(item2).SetName("(1, 3)");

            yield return new TestCaseData(inventory, new Vector2Int(3, 3))
                .Returns(item2).SetName("(3, 3)");

            yield return new TestCaseData(inventory, new Vector2Int(4, 1))
                .Returns(item1).SetName("(4, 1)");
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        [TestCase(3, 3)]
        public void WhenGetItemOutOfRangeThenException(int x, int y)
        {
            //Arrange:
            var inventory = new Inventory(3, 3);

            //Assert:
            Assert.Catch<IndexOutOfRangeException>(() => inventory.GetItem(x, y));
        }

        [Test]
        public void WhenGetNullItemThenException()
        {
            //Arrange:
            var inventory = new Inventory(3, 3);

            //Assert:
            Assert.Catch<NullReferenceException>(() => inventory.GetItem(0, 0));
        }

        [TestCaseSource(nameof(TryGetCases))]
        public bool TryGet(Inventory inventory, Vector2Int position, Item expectedItem)
        {
            bool success = inventory.TryGetItem(position, out Item actualItem);
            Assert.AreEqual(expectedItem, actualItem);
            return success;
        }

        private static IEnumerable<TestCaseData> TryGetCases()
        {
            var item1 = new Item("D", 1, 2);
            var item2 = new Item("X", 3, 2);

            var inventory = new Inventory(width: 5, height: 5,
                new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(1, 2)),
                new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(4, 0))
            );

            yield return new TestCaseData(inventory, new Vector2Int(1, 2), item2)
                .Returns(true).SetName("(1, 2)");

            yield return new TestCaseData(inventory, new Vector2Int(1, 3), item2)
                .Returns(true).SetName("(1, 3)");

            yield return new TestCaseData(inventory, new Vector2Int(3, 3), item2)
                .Returns(true).SetName("(3, 3)");

            yield return new TestCaseData(inventory, new Vector2Int(4, 1), item1)
                .Returns(true).SetName("(4, 1)");

            yield return new TestCaseData(inventory, new Vector2Int(0, 0), null)
                .Returns(false).SetName("Null");
        }

        [TestCase(-1, -1)]
        [TestCase(-1, 0)]
        [TestCase(0, -1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        [TestCase(3, 3)]
        public void WhenTryGetItemOutOfRangeThenFalse(int x, int y)
        {
            //Arrange:
            var inventory = new Inventory(3, 3);

            //Act:
            bool success = inventory.TryGetItem(x, y, out Item actualItem);

            //Assert:
            Assert.IsFalse(success);
            Assert.IsNull(actualItem);
        }

        [TestCaseSource(nameof(GetPositionsCases))]
        public Vector2Int[] GetPositions(Inventory inventory, Item item)
        {
            return inventory.GetPositions(item);
        }

        private static IEnumerable<TestCaseData> GetPositionsCases()
        {
            var itemD = new Item("D", 1, 2);
            var itemX = new Item("X", 3, 2);

            var inventory = new Inventory(width: 5, height: 5,
                new KeyValuePair<Item, Vector2Int>(itemD, new Vector2Int(4, 0)),
                new KeyValuePair<Item, Vector2Int>(itemX, new Vector2Int(1, 2))
            );

            yield return new TestCaseData(inventory, itemD).Returns(new[]
            {
                new Vector2Int(4, 0),
                new Vector2Int(4, 1)
            }).SetName("Item D");

            yield return new TestCaseData(inventory, itemX).Returns(new[]
            {
                new Vector2Int(1, 2),
                new Vector2Int(1, 3),
                new Vector2Int(2, 2),
                new Vector2Int(2, 3),
                new Vector2Int(3, 2),
                new Vector2Int(3, 3)
            }).SetName("Item X");
        }

        [Test]
        public void WhenGetPositionsAndItemIsNullThenException()
        {
            var inventory = new Inventory(3, 3);
            Assert.Catch<NullReferenceException>(() => inventory.GetPositions(null));
        }

        [Test]
        public void WhenGetPositionsAndItemAbsentThenThrowsException()
        {
            var inventory = new Inventory(3, 3);
            var item = new Item("X", 1, 1);

            Assert.Catch<KeyNotFoundException>(() => inventory.GetPositions(item));
        }

        [TestCaseSource(nameof(TryGetPositionsCases))]
        public bool TryGetPositions(Inventory inventory, Item item, Vector2Int[] expectedPositions)
        {
            bool succcess = inventory.TryGetPositions(item, out Vector2Int[] actualPositions);
            Assert.AreEqual(expectedPositions, actualPositions);
            return succcess;
        }

        private static IEnumerable<TestCaseData> TryGetPositionsCases()
        {
            var itemD = new Item("D", 1, 2);
            var itemX = new Item("X", 3, 2);

            var inventory = new Inventory(width: 5, height: 5,
                new KeyValuePair<Item, Vector2Int>(itemD, new Vector2Int(4, 0)),
                new KeyValuePair<Item, Vector2Int>(itemX, new Vector2Int(1, 2))
            );

            yield return new TestCaseData(inventory, itemD, new[]
            {
                new Vector2Int(4, 0),
                new Vector2Int(4, 1)
            }).Returns(true).SetName("Item D");

            yield return new TestCaseData(inventory, itemX, new[]
            {
                new Vector2Int(1, 2),
                new Vector2Int(1, 3),
                new Vector2Int(2, 2),
                new Vector2Int(2, 3),
                new Vector2Int(3, 2),
                new Vector2Int(3, 3)
            }).Returns(true).SetName("Item X");

            yield return new TestCaseData(inventory, null, null)
                .Returns(false).SetName("Null");

            yield return new TestCaseData(inventory, new Item("X", 1, 1), null)
                .Returns(false).SetName("Absent");
        }


        [Test]
        public void Clear()
        {
            //Arrange:
            var itemD = new Item("D", 1, 2);
            var itemX = new Item("X", 3, 2);
            var inventory = new Inventory(5, 5,
                new KeyValuePair<Item, Vector2Int>(itemD, new Vector2Int(4, 0)),
                new KeyValuePair<Item, Vector2Int>(itemX, new Vector2Int(1, 2))
            );

            bool wasEvent = false;
            inventory.OnCleared += () => wasEvent = true;

            //Act:
            inventory.Clear();

            //Assert:
            Assert.AreEqual(0, inventory.Count);
            Assert.IsTrue(wasEvent);
            Assert.IsFalse(inventory.Contains(itemD));
            Assert.IsFalse(inventory.Contains(itemX));

            Assert.AreEqual(Array.Empty<Item>(), inventory.ToArray());

            for (int x = 0; x < inventory.Width; x++)
                for (int y = 0; y < inventory.Height; y++)
                {
                    Assert.IsTrue(inventory.IsFree(x, y));
                }
        }

        [Test]
        public void WhenClearEmptyInventoryThenEventNotRisen()
        {
            //Arrange:
            var inventory = new Inventory(5, 5);

            bool wasEvent = false;
            inventory.OnCleared += () => wasEvent = true;

            //Act:
            inventory.Clear();

            //Assert:
            Assert.IsFalse(wasEvent);
        }

        [TestCaseSource(nameof(GetCountCases))]
        public int GetCount(Inventory inventory, string name)
        {
            return inventory.GetItemCount(name);
        }

        private static IEnumerable<TestCaseData> GetCountCases()
        {
            var item1 = new Item("X", 1, 1);
            var item2 = new Item("X", 1, 1);
            var item3 = new Item("X", 1, 1);
            var item4 = new Item("", 1, 1);
            var item5 = new Item(null, 1, 1);

            var inventory = new Inventory(5, 5,
                new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0)),
                new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(0, 1)),
                new KeyValuePair<Item, Vector2Int>(item3, new Vector2Int(1, 0)),
                new KeyValuePair<Item, Vector2Int>(item4, new Vector2Int(1, 1)),
                new KeyValuePair<Item, Vector2Int>(item5, new Vector2Int(2, 1))
            );

            yield return new TestCaseData(inventory, "X").Returns(3).SetName("X");
            yield return new TestCaseData(inventory, "").Returns(1).SetName("Empty Name");
            yield return new TestCaseData(inventory, null).Returns(1).SetName("Name is null");
            yield return new TestCaseData(inventory, "F").Returns(0).SetName("Absent items");
        }

        [TestCaseSource(nameof(CopyToCases))]
        public void CopyTo(Inventory inventory, Item[,] expected, Item[,] actual)
        {
            inventory.CopyTo(actual);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> CopyToCases()
        {
            var x = new Item("X", 2, 2);
            var y = new Item("Y", 1, 3);
            var z = new Item("Z", 2, 1);

            var inventory = new Inventory(3, 3,
                new KeyValuePair<Item, Vector2Int>(x, new Vector2Int(0, 0)),
                new KeyValuePair<Item, Vector2Int>(y, new Vector2Int(2, 0)),
                new KeyValuePair<Item, Vector2Int>(z, new Vector2Int(0, 2))
            );

            var expected = new[,]
            {
                {x, x, z},
                {x, x, z},
                {y, y, y}
            };
            var actual = new Item[3, 3];
            yield return new TestCaseData(inventory, expected, actual).SetName("Sample");
        }

        [TestCaseSource(nameof(MoveItemSuccessfulCases))]
        public void MoveItemSuccessful(Inventory inventory, Item item, Vector2Int position)
        {
            //Arrange:
            Item movedItem = default;
            Vector2Int movedPosition = Vector2Int.zero;
            inventory.OnMoved += (i, p) =>
            {
                movedItem = i;
                movedPosition = p;
            };

            bool added = false;
            bool removed = false;
            inventory.OnAdded += (_, _) => added = true;
            inventory.OnRemoved += (_, _) => removed = true;

            //Act:
            bool success = inventory.MoveItem(item, position);

            //Assert:
            Assert.IsTrue(success);
            Assert.AreEqual(item, movedItem);
            Assert.AreEqual(position, movedPosition);

            foreach (var newPosition in inventory.GetPositions(item))
            {
                Assert.AreEqual(item, inventory.GetItem(newPosition));
            }

            Assert.IsFalse(added);
            Assert.IsFalse(removed);
        }

        private static IEnumerable<TestCaseData> MoveItemSuccessfulCases()
        {
            var item1 = new Item(1, 1);
            yield return new TestCaseData(
                new Inventory(5, 5, new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0))),
                item1,
                new Vector2Int(1, 1)
            ).SetName("Simple");

            var item2 = new Item(2, 2);
            yield return new TestCaseData(
                new Inventory(5, 5, new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(0, 0))),
                item2,
                new Vector2Int(1, 1)
            ).SetName("Intersects with itself");
        }

        [Test]
        public void WhenMoveNullItemThenException()
        {
            //Arrange:
            var inventory = new Inventory(5, 5);

            //Assert:
            Assert.Catch<ArgumentNullException>(() => inventory.MoveItem(null, new Vector2Int(1, 1)));
        }


        [TestCaseSource(nameof(MoveItemFailedCases))]
        public void MoveItemFailed(Inventory inventory, Item item, Vector2Int position)
        {
            //Arrange:
            Item movedItem = default;
            Vector2Int movedPosition = Vector2Int.zero;
            inventory.OnMoved += (i, p) =>
            {
                movedItem = i;
                movedPosition = p;
            };

            //Act:
            bool success = inventory.MoveItem(item, position);

            //Assert:
            Assert.IsFalse(success);
            Assert.AreEqual(default, movedItem);
            Assert.AreEqual(Vector2Int.zero, movedPosition);

            inventory.TryGetItem(position, out Item actualItem);
            Assert.AreNotEqual(item, actualItem);
        }

        public static IEnumerable<TestCaseData> MoveItemFailedCases()
        {
            yield return MoveAbsentItemCase();

            foreach (TestCaseData @case in MoveOutOfBoundsCases())
                yield return @case;

            foreach (TestCaseData @case in MoveNearBoundsFailedCases())
                yield return @case;

            yield return MoveFailedWhenIntersectsWithAnother();
        }

        private static TestCaseData MoveFailedWhenIntersectsWithAnother()
        {
            var x = new Item("X", 2, 2);
            var z = new Item("Z", 2, 1);

            return new TestCaseData(new Inventory(3, 3,
                new KeyValuePair<Item, Vector2Int>(x, new Vector2Int(0, 0)),
                new KeyValuePair<Item, Vector2Int>(z, new Vector2Int(0, 2))
            ), z, new Vector2Int(1, 1)).SetName("Intersects with another");
        }

        private static TestCaseData MoveAbsentItemCase()
        {
            return new TestCaseData(new Inventory(5, 5),
                new Item(1, 1), new Vector2Int(2, 2)).SetName("Asbent item");
        }

        private static IEnumerable<TestCaseData> MoveOutOfBoundsCases()
        {
            var outOfRangeCases = new Vector2Int[]
            {
                new(-1, -1),
                new(-1, 1),
                new(1, -1),
                new(5, 5),
                new(5, 0),
                new(0, 5),
            };

            var item1 = new Item(1, 1);

            foreach (var position in outOfRangeCases)
            {
                yield return new TestCaseData(
                    new Inventory(5, 5, new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0))),
                    item1,
                    position
                ).SetName($"Out of Range Posiiton {position}");
            }
        }

        private static IEnumerable<TestCaseData> MoveNearBoundsFailedCases()
        {
            var outOfRangeCases = new Vector2Int[]
            {
                new(3, 3),
                new(4, 2),
                new(3, 3)
            };

            var item1 = new Item(3, 3);

            foreach (var position in outOfRangeCases)
            {
                yield return new TestCaseData(
                    new Inventory(5, 5, new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0))),
                    item1,
                    position
                ).SetName($"Near bounds failed {position}");
            }
        }

        [TestCaseSource(nameof(ReorganizeSpaceCases))]
        public void ReorganizeSpace(Inventory inventory, Item[,] expected)
        {
            //Act:
            inventory.ReorganizeSpace();

            //Assert:
            Item[,] actual = new Item[inventory.Width, inventory.Height];
            inventory.CopyTo(actual);
            Assert.AreEqual(expected, actual);
        }

        private static IEnumerable<TestCaseData> ReorganizeSpaceCases()
        {
            yield return ReorganizeSimpleCase();
            yield return ReorganizeFullCase();
            yield return ReorganizeMediumCase();
        }

        private static TestCaseData ReorganizeSimpleCase()
        {
            var item1 = new Item("1", 1, 1);
            var item2 = new Item("2", 1, 1);
            var item3 = new Item("3", 1, 1);
            var item4 = new Item("4", 1, 1);
            var item5 = new Item("5", 2, 2);

            return new TestCaseData(new Inventory(4, 4,
                    new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0)),
                    new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(3, 3)),
                    new KeyValuePair<Item, Vector2Int>(item3, new Vector2Int(0, 3)),
                    new KeyValuePair<Item, Vector2Int>(item4, new Vector2Int(3, 0)),
                    new KeyValuePair<Item, Vector2Int>(item5, new Vector2Int(1, 1))
                ), new[,]
                {
                    {item5, item5, null, null},
                    {item5, item5, null, null},
                    {item1, item3, null, null},
                    {item2, item4, null, null}
                }
            ).SetName("Simple");
        }

        private static TestCaseData ReorganizeFullCase()
        {
            var item1 = new Item("1", 1, 2);
            var item2 = new Item("2", 2, 2);
            var item3 = new Item("3", 1, 4);
            var item4 = new Item("4", 3, 2);

            return new TestCaseData(new Inventory(4, 4,
                    new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0)),
                    new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(1, 0)),
                    new KeyValuePair<Item, Vector2Int>(item3, new Vector2Int(3, 0)),
                    new KeyValuePair<Item, Vector2Int>(item4, new Vector2Int(0, 2))
                ), new[,]
                {
                    {item4, item4, item2, item2},
                    {item4, item4, item2, item2},
                    {item4, item4, item1, item1},
                    {item3, item3, item3, item3}
                }
            ).SetName("Full");
        }

        private static TestCaseData ReorganizeMediumCase()
        {
            var item1 = new Item("1", 1, 1);
            var item2 = new Item("2", 2, 3);
            var item3 = new Item("3", 2, 1);
            var item4 = new Item("4", 2, 1);
            var item5 = new Item("5", 2, 2);
            var item6 = new Item("6", 2, 1);
            var item7 = new Item("7", 1, 4);

            return new TestCaseData(new Inventory(5, 5,
                    new KeyValuePair<Item, Vector2Int>(item1, new Vector2Int(0, 0)),
                    new KeyValuePair<Item, Vector2Int>(item2, new Vector2Int(0, 2)),
                    new KeyValuePair<Item, Vector2Int>(item3, new Vector2Int(3, 4)),
                    new KeyValuePair<Item, Vector2Int>(item4, new Vector2Int(2, 3)),
                    new KeyValuePair<Item, Vector2Int>(item5, new Vector2Int(2, 1)),
                    new KeyValuePair<Item, Vector2Int>(item6, new Vector2Int(2, 0)),
                    new KeyValuePair<Item, Vector2Int>(item7, new Vector2Int(4, 0))
                ), new[,]
                {
                    {item2, item2, item2, item4, item1},
                    {item2, item2, item2, item4, null},
                    {item5, item5, item3, item6, null},
                    {item5, item5, item3, item6, null},
                    {item7, item7, item7, item7, null}
                }
            ).SetName("Medium");
        }
    }
}