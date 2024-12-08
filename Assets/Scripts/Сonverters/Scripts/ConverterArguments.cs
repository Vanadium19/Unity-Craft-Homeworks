using System;

namespace Converters
{
    public struct ConverterArguments
    {
        public ConverterArguments(int loadingAreaCapacity,
                                  int unloadingAreaCapacity,
                                  int takenResourcesCount,
                                  int givenResourcesCount,
                                  float conversionTime)
        {
            if (loadingAreaCapacity <= 0)
                throw new ArgumentException();

            if (unloadingAreaCapacity <= 0)
                throw new ArgumentException();

            if (takenResourcesCount <= 0)
                throw new ArgumentException();

            if (givenResourcesCount <= 0)
                throw new ArgumentException();

            if (conversionTime <= 0)
                throw new ArgumentException();

            LoadingAreaCapacity = loadingAreaCapacity;
            UnloadingAreaCapacity = unloadingAreaCapacity;
            TakenResourcesCount = takenResourcesCount;
            GivenResourcesCount = givenResourcesCount;
            ConversionTime = conversionTime;
        }

        public int LoadingAreaCapacity { get; }
        public int UnloadingAreaCapacity { get; }
        public int TakenResourcesCount { get; }
        public int GivenResourcesCount { get; }
        public float ConversionTime { get; }
    }
}