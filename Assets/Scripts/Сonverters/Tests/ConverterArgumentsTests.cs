using Converters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converters
{
    internal class ConverterArgumentsTests
    {
        [TestCase(10, 20, 2, 4, 2f)]
        [TestCase(5, 10, 1, 2, 3f)]
        [TestCase(20, 40, 5, 10, 7f)]
        public void Instantiate(int loadingAreaCapacity,
                                int unloadingAreaCapacity,
                                int takenResources,
                                int givenResources,
                                float conversionTime)
        {
            //Act
            var arguments = new ConverterArguments(loadingAreaCapacity,
                                                   unloadingAreaCapacity,
                                                   takenResources,
                                                   givenResources,
                                                   conversionTime);

            //Assert
            Assert.AreEqual(loadingAreaCapacity, arguments.LoadingAreaCapacity);
            Assert.AreEqual(unloadingAreaCapacity, arguments.UnloadingAreaCapacity);
            Assert.AreEqual(takenResources, arguments.TakenResourcesCount);
            Assert.AreEqual(givenResources, arguments.GivenResourcesCount);
            Assert.AreEqual(conversionTime, arguments.ConversionTime);
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
                var arguments = new ConverterArguments(loadingAreaCapacity,
                                                       unloadingAreaCapacity,
                                                       takenResources,
                                                       givenResources,
                                                       conversionTime);
            });
        }
    }
}