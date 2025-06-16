using UnityEngine;

namespace Code.Infrastructure.Services
{
    public class CameraProvider : ICameraProvider
    {
        private Camera _mainCamera;

        public Camera GetMainCamera()
        {
            if (_mainCamera == null)
                _mainCamera = Camera.main;

            return _mainCamera;
        }
    }
}