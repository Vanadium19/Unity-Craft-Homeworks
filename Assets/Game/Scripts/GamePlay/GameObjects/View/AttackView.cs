using UnityEngine;

namespace Game.View
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