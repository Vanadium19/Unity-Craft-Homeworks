using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.Planets
{
    [CreateAssetMenu(
        fileName = "PlanetCatalog",
        menuName = "Modules/Planets/PlanetCatalog"
    )]
    public sealed class PlanetCatalog : ScriptableObject, IReadOnlyList<PlanetConfig>
    {
        public int Count => _planets.Length;

        [SerializeField]
        private PlanetConfig[] _planets;

        public PlanetConfig this[int index]
        {
            get { return _planets[index]; }
        }

        public IEnumerator<PlanetConfig> GetEnumerator()
        {
            for (int i = 0, count = _planets.Length; i < count; i++)
                yield return _planets[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}