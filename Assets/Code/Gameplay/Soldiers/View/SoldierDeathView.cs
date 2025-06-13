using UnityEngine;

namespace Code.Gameplay.Soldiers.View
{
    public class SoldierDeathView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem; 
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _mesh;
        
        public void SetBeforeDeathState()
        {
            _collider.enabled = false;
            _mesh.SetActive(false);
            _particleSystem.Play();
        }
    }
}
