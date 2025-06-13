using UnityEngine;

namespace Code.Gameplay.Input.Services
{
    public class MobileScreenTapService : IScreenTapService
    {
        public Vector3 GetScreenTap => UnityEngine.Input.GetTouch(0).position;
    }
}