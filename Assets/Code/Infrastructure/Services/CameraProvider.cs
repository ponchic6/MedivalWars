using UnityEngine;

namespace Code.Infrastructure.Services
{
    public class CameraProvider : ICameraProvider
    {
        private Camera _mainCamera;

        public Camera GetMainCamera() =>
            _mainCamera ??= Camera.main;
    }
}