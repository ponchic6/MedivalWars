using UnityEngine;

namespace Code.Gameplay.Input.Services
{
    public class ComputerScreenTapService : IScreenTapService
    {
        public Vector3 GetScreenTap =>
            UnityEngine.Input.mousePosition;
    }
}