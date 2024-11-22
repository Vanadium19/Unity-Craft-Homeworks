using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.UI.DefaultControls;

namespace Converters
{
    public class ConverterTests
    {
        [TestCase(10, 20, 2, 4, 2f, true)]
        [TestCase(5, 10, 1, 2, 3f, false)]
        [TestCase(20, 40, 5, 10, 7f, true)]
        public void Instantiate(int loadingAreaCapacity,
                                int unloadingAreaCapacity,
                                int takenResources,
                                int givenResources,
                                float conversionTime,
                                bool enabled)
        {

            //Act
            var converter = new Converter(loadingAreaCapacity,
                                          unloadingAreaCapacity,
                                          takenResources,
                                          givenResources,
                                          conversionTime,
                                          enabled);

            //Assert
            Assert.AreEqual(loadingAreaCapacity, converter.LoadingAreaCapacity);
            Assert.AreEqual(unloadingAreaCapacity, converter.UnloadingAreaCapacity);
            Assert.AreEqual(takenResources, converter.TakenResourcesCount);
            Assert.AreEqual(givenResources, converter.GivenResourcesCount);
            Assert.AreEqual(conversionTime, converter.ConversionTime);
            Assert.AreEqual(enabled, converter.Enabled);
        }

        [TestCase(-10, 20, 2, 4, 2f)]
        [TestCase(10, -20, 2, 4, 2f)]
        [TestCase(10, 20, -2, 4, 2f)]
        [TestCase(10, 20, 2, -4, 2f)]
        [TestCase(10, 20, 2, 4, -2f)]
        [TestCase(-10, -20, -2, -4, -2f)]
        [TestCase(0, 20, 2, 4, 2f)]
        [TestCase(10, 0, 2, 4, 2f)]
        [TestCase(10, 20, 0, 4, 2f)]
        [TestCase(10, 20, 2, 0, 2f)]
        [TestCase(10, 20, 2, 4, 0)]
        [TestCase(0, 0, 0, 0, 0)]
        public void WhenInstantiateWithNegativeArgumentsThenException(int loadingAreaCapacity,
                                                                      int unloadingAreaCapacity,
                                                                      int takenResources,
                                                                      int givenResources,
                                                                      float conversionTime)
        {
            //Assert
            Assert.Catch<ArgumentException>(() =>
            {
                var converter = new Converter(loadingAreaCapacity,
                                          unloadingAreaCapacity,
                                          takenResources,
                                          givenResources,
                                          conversionTime);
            });
        }

        [TestCase(5, 5, 0)]
        [TestCase(13, 10, 3)]
        [TestCase(20, 10, 10)]
        public void AddResourcesCases(int count, int expectedCount, int extraResourcesCount)
        {
            var converter = new Converter(10, 20, 2, 4, 2f);

            //Arrange
            Resource[] resources = new Resource[count];

            for (int i = 0; i < count; i++)
                resources[i] = new Resource();

            //Act
            converter.Add(resources, out IReadOnlyList<Resource> extraResources);

            //Assert
            Assert.AreEqual(expectedCount, converter.LoadingAreaCount);
            Assert.AreEqual(extraResourcesCount, extraResources.Count());
        }

        [Test]
        public void WhenAddEmptyResourcesIEnumerableThenException()
        {
            //Act
            var converter = new Converter(10, 20, 2, 4, 2f);

            //Assert
            Assert.Catch<ArgumentException>(() => { converter.Add(new Resource[2], out var extra); });
        }

        [Test]
        public void WhenAddNullResourcesThenException()
        {
            //Act
            var converter = new Converter(10, 20, 2, 4, 2f);

            //Assert
            Assert.Catch<ArgumentNullException>(() => { converter.Add(null, out var extra); });
        }

        [TestCase(false, true)]
        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, false)]
        public void SetActive(bool startState, bool value)
        {
            //Arrange
            var converter = new Converter(10, 20, 2, 4, 2f, value);

            //Act
            converter.SetActive(value);

            //Assert
            Assert.AreEqual(value, converter.Enabled);
        }

        [TestCaseSource(nameof(UpdateCases))]
        public void TestUpdateRunning(Converter converter, float[] deltaTimes, float elapsedTime)
        {
            //Act
            foreach (var deltaTime in deltaTimes)
            {
                converter.Update(deltaTime);
            }

            //Assert
            Assert.AreEqual(elapsedTime, converter.ElapsedTime);
        }

        public static IEnumerable<TestCaseData> UpdateCases()
        {
            //Arrange
            var deltaTimes = new float[] { 0.3f, 0.6f, 0.7f };

            var resources = new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() };

            var converter = new Converter(10, 20, 2, 4, 2f, false);

            converter.Add(resources, out var extra);

            yield return new TestCaseData(converter, deltaTimes, 0f).SetName("Converter disabled");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, deltaTimes, deltaTimes.Sum()).SetName("Converter enabled");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f, 2f, 0.5f }, 0.5f).SetName("When next conversion started");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            yield return new TestCaseData(converter, deltaTimes, 0f).SetName("With empty loading area");
        }

        [Test]
        public void WhenNegativeTimeDeltaTimeThenArgumentException()
        {
            //Arrange
            var converter = new Converter(10, 20, 2, 4, 2f, true);

            //Act
            Assert.Catch<ArgumentException>(() => { converter.Update(-1f); });
        }

        [TestCaseSource(nameof(StartConvertCases))]
        public void ResourcesStartConvert(Converter converter, float[] deltaTimes, int expectedConversionResources, int expectedLoadingAreaCount)
        {
            //Act
            foreach (var deltaTime in deltaTimes)
            {
                converter.Update(deltaTime);
            }

            //Assert
            Assert.AreEqual(expectedConversionResources, converter.ConversionResourcesCount);
            Assert.AreEqual(expectedLoadingAreaCount, converter.LoadingAreaCount);
        }

        public static IEnumerable<TestCaseData> StartConvertCases()
        {
            //Arrange
            var deltaTimes = new float[] { 1f, 2f, 0.5f };

            var resources = new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() };

            var converter = new Converter(10, 20, 2, 4, 2f, false);

            converter.Add(resources, out var extra);

            yield return new TestCaseData(converter, deltaTimes, 0, 4).SetName("Converter disabled");

            converter = new Converter(10, 20, 3, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f }, 3, 1).SetName("Simple");

            converter = new Converter(10, 20, 3, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, deltaTimes, 1, 0).SetName("When start new loop");

            converter = new Converter(10, 20, 3, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f, 1f, 1f, 1f, 1f }, 0, 0).SetName("When resources ended");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            yield return new TestCaseData(converter, deltaTimes, 0, 0).SetName("With empty loading area");
        }

        [TestCaseSource(nameof(EndConvertCases))]
        public void ResourcesEndConvert(Converter converter, float[] deltaTimes, int expectedProductCount)
        {
            //Act
            foreach (var deltaTime in deltaTimes)
            {
                converter.Update(deltaTime);
            }

            //Assert
            Assert.AreEqual(expectedProductCount, converter.UnloadingAreaCount);
        }

        public static IEnumerable<TestCaseData> EndConvertCases()
        {
            //Arrange
            var deltaTimes = new float[] { 1f, 2f, 0.5f };

            var resources = new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() };

            var converter = new Converter(10, 20, 2, 4, 2f, false);

            converter.Add(resources, out var extra);

            yield return new TestCaseData(converter, deltaTimes, 0).SetName("Converter disabled");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f }, 0).SetName("Didn't have time to convert");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, deltaTimes, 4).SetName("Simle");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f, 1f, 1f, 1f, 1f }, 8).SetName("When resources ended");

            converter = new Converter(10, 20, 3, 4, 2f, true);

            converter.Add(new Resource[] { new Resource(), new Resource(), new Resource(), new Resource(), new Resource() }, out extra);

            yield return new TestCaseData(converter, new float[] { 1f, 1f, 1f, 1f, 1f }, 6).SetName("When complicated proportion");

            converter = new Converter(10, 6, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new float[] { 1f, 1f, 1f, 1f, 1f }, 6).SetName("When unloading area capacity ended");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            yield return new TestCaseData(converter, deltaTimes, 0).SetName("With empty loading area");
        }

        [TestCaseSource(nameof(OnDisableCases))]
        public void DisableConverter(Converter converter, IEnumerable<Resource> resources, float[] deltaTimes, int expectedLoadingAreaCount)
        {
            //Act
            bool resourcesAdded = false;

            foreach (var deltaTime in deltaTimes)
            {
                converter.Update(deltaTime);

                if (resourcesAdded == false)
                {
                    converter.Add(resources);
                    resourcesAdded = true;
                }
            }

            converter.SetActive(false);

            //Assert
            Assert.AreEqual(expectedLoadingAreaCount, converter.LoadingAreaCount);
        }

        public static IEnumerable<TestCaseData> OnDisableCases()
        {
            //Arrange
            var deltaTimes = new float[] { 1f, 1f, 1f };

            var resources = new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() };

            var converter = new Converter(10, 20, 4, 4, 5f, true);

            converter.Add(resources, out var extra);

            yield return new TestCaseData(converter, new Resource[0], deltaTimes, 4).SetName("Simple");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, new Resource[0], deltaTimes, 2).SetName("There wasn't enough time for all the resources");

            converter = new Converter(10, 20, 2, 4, 2f, true);

            converter.Add(resources, out extra);
            converter.Add(resources, out extra);

            yield return new TestCaseData(converter, resources, deltaTimes, 10).SetName("Part burns");
        }
    }
}