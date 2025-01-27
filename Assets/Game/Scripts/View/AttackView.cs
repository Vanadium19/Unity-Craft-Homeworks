using UnityEngine;

namespace Game.Scripts.View
{
    public class AttackView : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Attack()
        {
            _audioSource.Play();
        }
    }
}