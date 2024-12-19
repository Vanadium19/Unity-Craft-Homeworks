using UnityEngine;

namespace Modules.UI
{
    public sealed class RotationAnimation : MonoBehaviour
    {
        [SerializeField]
        private float speed;
        
        private void Update()
        {
            transform.Rotate(new Vector3(0, 0, speed * Time.deltaTime));
        }
    }
}