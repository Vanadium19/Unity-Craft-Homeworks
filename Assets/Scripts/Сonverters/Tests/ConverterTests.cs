using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;
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
            //Arrange
            var arguments = new ConverterArguments(loadingAreaCapacity,
                                                   unloadingAreaCapacity,
                                                   takenResources,
                                                   givenResources,
                                                   conversionTime);

            //Act
            var converter = new Converter(arguments, enabled);

            //Assert
            Assert.AreEqual(loadingAreaCapacity, converter.LoadingAreaCapacity);
            Assert.AreEqual(unloadingAreaCapacity, converter.UnloadingAreaCapacity);
            Assert.AreEqual(takenResources, converter.TakenResourcesCount);
            Assert.AreEqual(givenResources, converter.GivenResourcesCount);
            Assert.AreEqual(conversionTime, converter.ConversionTime);
            Assert.AreEqual(enabled, converter.Enabled);
        }

        [TestCase(5, 5, 0)]
        [TestCase(13, 10, 3)]
        [TestCase(20, 10, 10)]
        public void AddResourcesCases(int count, int expectedCount, int extraResourcesCount)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f));

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
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f));

            //Assert
            Assert.Catch<ArgumentException>(() => { converter.Add(new Resource[2], out var extra); });
        }

        [Test]
        public void WhenAddNullResourcesThenException()
        {
            //Act
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f));

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
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), value);

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

            yield return UpdateConverterDisabledCase(resources, deltaTimes).SetName("Converter disabled");
            yield return UpdateConverterEnabledCase(resources, deltaTimes).SetName("Converter enabled");
            yield return UpdateWhenNextConversionStartedCase(resources).SetName("When next conversion started");
            yield return UpdateWithEmptyLoadingAreaCase(deltaTimes).SetName("With empty loading area");
        }

        private static TestCaseData UpdateConverterEnabledCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), false);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 0f);
        }

        private static TestCaseData UpdateConverterDisabledCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, deltaTimes.Sum());
        }

        private static TestCaseData UpdateWhenNextConversionStartedCase(Resource[] resources)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, new float[] { 1f, 2f, 0.5f }, 0.5f);
        }

        private static TestCaseData UpdateWithEmptyLoadingAreaCase(float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            return new TestCaseData(converter, deltaTimes, 0f);
        }

        [Test]
        public void WhenNegativeTimeDeltaTimeThenArgumentException()
        {
            //Arrange
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

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

            yield return StartConvertConverterDisabledCase(resources, deltaTimes).SetName("Converter disabled");
            yield return StartConvertSimpleCase(resources, new float[] { 1f }).SetName("Simple");
            yield return StartConvertWhenStartNewLoopCase(resources, deltaTimes).SetName("When start new loop");
            yield return StartConvertWhenResourcesEndedCase(resources, new float[] { 1f, 1f, 1f, 1f, 1f }).SetName("When resources ended");
            yield return StartConvertWithEmptyLoadingAreaCase(resources, deltaTimes).SetName("With empty loading area");
        }

        private static TestCaseData StartConvertConverterDisabledCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), false);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 0, 4);
        }

        private static TestCaseData StartConvertSimpleCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 3, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 3, 1);
        }

        private static TestCaseData StartConvertWhenStartNewLoopCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 3, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 1, 0);
        }

        private static TestCaseData StartConvertWhenResourcesEndedCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 3, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 0, 0);
        }

        private static TestCaseData StartConvertWithEmptyLoadingAreaCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            return new TestCaseData(converter, deltaTimes, 0, 0);
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

            yield return EndConvertConverterDisabledCase(resources, deltaTimes).SetName("Converter disabled");
            yield return EndConvertDidNotHaveTimeToConvertCase(resources, new float[] { 1f }).SetName("Didn't have time to convert");
            yield return EndConvertSimpleCase(resources, deltaTimes).SetName("Simle");
            yield return EndConvertWhenResourcesEndedCase(resources, new float[] { 1f, 1f, 1f, 1f, 1f }).SetName("When resources ended");
            yield return EndConvertWhenComplicatedProportionCase(new Resource[]
            {
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource()
            }, new float[]
            {
                1f,
                1f,
                1f,
                1f,
                1f
            }).SetName("When complicated proportion");
            yield return EndConvertWhenUnloadingAreaCapacityEndedCase(resources, new float[]
            {
                1f,
                1f,
                1f,
                1f,
                1f
            }).SetName("When unloading area capacity ended");
            yield return EndConvertWithEmptyLoadingAreaCase(resources, deltaTimes).SetName("With empty loading area");
        }

        private static TestCaseData EndConvertConverterDisabledCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), false);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 0);
        }

        private static TestCaseData EndConvertDidNotHaveTimeToConvertCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 0);
        }

        private static TestCaseData EndConvertSimpleCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 4);
        }

        private static TestCaseData EndConvertWhenResourcesEndedCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 8);
        }

        private static TestCaseData EndConvertWhenComplicatedProportionCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 3, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 6);
        }

        private static TestCaseData EndConvertWhenUnloadingAreaCapacityEndedCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 6, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 6);
        }

        private static TestCaseData EndConvertWithEmptyLoadingAreaCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            return new TestCaseData(converter, deltaTimes, 0);
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
            Assert.AreEqual(0f, converter.ElapsedTime);
        }

        public static IEnumerable<TestCaseData> OnDisableCases()
        {
            //Arrange
            var deltaTimes = new float[] { 1f, 1f, 1f };

            var resources = new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() };

            yield return OnDisableSimpleCase(resources, deltaTimes).SetName("Simple");
            yield return OnDisableThereWasNotEnoughTimeForAlResourcesCase(resources, deltaTimes).SetName("There wasn't enough time for all the resources");

            resources = new Resource[]
            {
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource()
            };

            yield return OnDisablePartBurnsCase(resources, deltaTimes).SetName("Part burns");
        }

        private static TestCaseData OnDisableSimpleCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 4, 4, 5f), true);

            converter.Add(resources);

            return new TestCaseData(converter, new Resource[0], deltaTimes, 4);

        }

        private static TestCaseData OnDisableThereWasNotEnoughTimeForAlResourcesCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter, new Resource[0], deltaTimes, 2);
        }

        private static TestCaseData OnDisablePartBurnsCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(10, 20, 2, 4, 2f), true);

            converter.Add(resources);

            return new TestCaseData(converter,
                                    new Resource[] { new Resource(), new Resource(), new Resource(), new Resource() },
                                    deltaTimes,
                                    10);
        }

        [TestCaseSource(nameof(UnloadingAreaIsFullCases))]
        public void WhenUnloadingAreaIsFullConverterDoesNotWork(Converter converter,
                                                                float[] deltaTimes,
                                                                int expectedLoadingAreaCount,
                                                                int expectedUnloadingAreaCount)
        {
            Debug.Log(converter.LoadingAreaCount);

            //Act
            foreach (var deltaTime in deltaTimes)
            {
                converter.Update(deltaTime);
            }

            //Assert
            Assert.AreEqual(expectedLoadingAreaCount, converter.LoadingAreaCount);
            Assert.AreEqual(expectedUnloadingAreaCount, converter.UnloadingAreaCount);
            Assert.AreEqual(0f, converter.ElapsedTime);
        }

        public static IEnumerable<TestCaseData> UnloadingAreaIsFullCases()
        {
            //Arrange
            var deltaTimes = new float[] { 1f, 1f, 0.5f };

            var resources = new Resource[]
            {
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource(),
                new Resource()
            };

            yield return UnloadingAreaFullSimpleCase(resources, deltaTimes).SetName("Simple");
            yield return UnloadingAreaFullWhenNeedTakeOneLessCase(resources, deltaTimes).SetName("When need take one less");
            yield return UnloadingAreaFullWhenNeedTakeOneMoreCase(resources, deltaTimes).SetName("When need take one more");
        }

        private static TestCaseData UnloadingAreaFullSimpleCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(4, 2, 1, 2, 1f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 3, 2);
        }

        private static TestCaseData UnloadingAreaFullWhenNeedTakeOneLessCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(8, 4, 2, 3, 1f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 5, 4);
        }

        private static TestCaseData UnloadingAreaFullWhenNeedTakeOneMoreCase(Resource[] resources, float[] deltaTimes)
        {
            var converter = new Converter(new ConverterArguments(8, 5, 2, 3, 1f), true);

            converter.Add(resources);

            return new TestCaseData(converter, deltaTimes, 4, 5);
        }
    }
}