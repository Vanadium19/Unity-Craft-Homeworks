using UnityEngine;

namespace Game.Scripts.View
{
    public class PushView : MonoBehaviour
    {
        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _tossSound;
        [SerializeField] private AudioClip _pushSound;

        [Header("VFX")]
        [SerializeField] private ParticleSystem _pushVFX;
        [SerializeField] private ParticleSystem _tossVFX;

        public void Push()
        {
            _pushVFX.Play();
            _audioSource.PlayOneShot(_pushSound);
        }

        public void Toss()
        {
            _tossVFX.Play();
            _audioSource.PlayOneShot(_tossSound);
        }
    }
}