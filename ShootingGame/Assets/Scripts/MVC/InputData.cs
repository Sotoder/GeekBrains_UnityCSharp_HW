using UnityEngine;

namespace Model.ShootingGame
{
    [CreateAssetMenu(menuName = "Data/Input", fileName = nameof(InputData))]
    public sealed class InputData : ScriptableObject
    {
        public KeyCode Fire = KeyCode.Mouse0;
        public KeyCode GameMenu = KeyCode.Escape;
        public KeyCode CameraRotate = KeyCode.Mouse1;
        public KeyCode SavePlayer = KeyCode.C;
        public KeyCode LoadPlayer = KeyCode.V;

        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
        public const string MOUSE_X = "Mouse X";
    }
}
