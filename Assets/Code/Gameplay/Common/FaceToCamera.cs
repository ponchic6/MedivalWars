using Code.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Common
{
    public class FaceToCamera : MonoBehaviour
    {
        private ICameraProvider _cameraProvider;

        [Inject]
        public void Construct(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        
        private void Update()
        {
            Camera mainCamera = _cameraProvider.GetMainCamera();
            
            if (mainCamera == null)
                return;
            
            transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward,
                mainCamera.transform.rotation * Vector3.up);
        }
    }
}