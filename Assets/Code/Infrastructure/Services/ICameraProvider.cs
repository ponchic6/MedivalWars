using UnityEngine;

namespace Code.Infrastructure.Services
{
    public interface ICameraProvider
    {
        public Camera GetMainCamera();
    }
}