using UnityEngine;

namespace Code.Gameplay.Input.Services
{
    public class MobileScreenTapService : IScreenTapService
    {
        public Vector3 GetScreenTap
        {
            get
            {
                if (IsTapping)
                    return UnityEngine.Input.GetTouch(0).position;

                return Vector3.zero;
            }
        }

        public bool IsTapping =>
            UnityEngine.Input.touchCount > 0;
    }
}