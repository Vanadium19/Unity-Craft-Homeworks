using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Converters
{
    public class Converter
    {
        private readonly int _loadingAreaCapacity;
        private readonly int _unloadingAreaCapacity;
        private readonly int _takenResourcesCount;
        private readonly int _givenResourcesCount;
        private readonly float _conversionTime;

        private int _loadingAreaCount;
        private int _unloadingAreaCount;
        private int _conversionResourcesCount;

        private bool _enabled;
        private float _elapsedTime;

        public Converter(ConverterArguments arguments, bool enabled = false)
        {
            _enabled = enabled;
            _elapsedTime = 0f;

            _loadingAreaCapacity = arguments.LoadingAreaCapacity;
            _unloadingAreaCapacity = arguments.UnloadingAreaCapacity;
            _takenResourcesCount = arguments.TakenResourcesCount;
            _givenResourcesCount = arguments.GivenResourcesCount;
            _conversionTime = arguments.ConversionTime;
        }

        public bool Enabled => _enabled;
        public float ElapsedTime => _elapsedTime;
        public int LoadingAreaCount => _loadingAreaCount;
        public int LoadingAreaCapacity => _loadingAreaCapacity;
        public int UnloadingAreaCapacity => _unloadingAreaCapacity;
        public int UnloadingAreaCount => _unloadingAreaCount;
        public int TakenResourcesCount => _takenResourcesCount;
        public int GivenResourcesCount => _givenResourcesCount;
        public float ConversionTime => _conversionTime;
        public int ConversionResourcesCount => _conversionResourcesCount;

        public void Add(IEnumerable<Resource> resources, out IReadOnlyList<Resource> extraResources)
        {
            CheckResources(resources);

            List<Resource> extra = new();

            _loadingAreaCount += resources.Count();

            if (_loadingAreaCount > _loadingAreaCapacity)
            {
                int extraResourcesCount = _loadingAreaCount - _loadingAreaCapacity;
                _loadingAreaCount = _loadingAreaCapacity;

                for (int i = 0; i < extraResourcesCount; i++)
                    extra.Add(new Resource());
            }

            extraResources = extra;
        }

        public void Add(IEnumerable<Resource> resources)
        {
            CheckResources(resources);

            _loadingAreaCount = Mathf.Min(_loadingAreaCount + resources.Count(), _loadingAreaCapacity);
        }

        public void SetActive(bool value)
        {
            _enabled = value;

            if (!value && _conversionResourcesCount > 0)
            {
                Add(_conversionResourcesCount);
                _conversionResourcesCount = 0;
                _elapsedTime = 0f;
            }
        }

        public void Update(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentException();

            if (!_enabled)
                return;

            if (_unloadingAreaCapacity - _unloadingAreaCount < _givenResourcesCount)
                return;

            if (_loadingAreaCount < _takenResourcesCount && _conversionResourcesCount == 0)
                return;

            if (_conversionResourcesCount == 0)
                StartConvert();

            _elapsedTime += deltaTime;

            if (_elapsedTime >= _conversionTime)
            {
                EndConvert();
                _elapsedTime = 0;
            }
        }

        private void StartConvert()
        {
            _loadingAreaCount -= _takenResourcesCount;
            _conversionResourcesCount = _takenResourcesCount;
        }

        private void EndConvert()
        {
            _unloadingAreaCount += _givenResourcesCount;
            _conversionResourcesCount = 0;
        }

        private void Add(int resourcesCount)
        {
            if (resourcesCount <= 0)
                throw new ArgumentException();

            _loadingAreaCount = Mathf.Min(_loadingAreaCount + resourcesCount, _loadingAreaCapacity);
        }

        private void CheckResources(IEnumerable<Resource> resources)
        {
            if (resources == null)
                throw new ArgumentNullException();

            foreach (var resource in resources)
            {
                if (resource == null)
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}