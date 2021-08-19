using UnityEngine;

namespace UI.ShootingGame
{

    public class Tooltipe : MonoBehaviour
    {
        private GUIStyle _guiStyle = new GUIStyle();
        private void OnGUI()
        {
            GUI.Box(new Rect(5, 10, Screen.width / 7, 60), "Подсказки:");
            
            _guiStyle.normal.textColor = Color.white;

            DrawButtonNameLabel(40, "P");
            DrawTooltipeLabel(40, "- изменить тип");
            GUI.Label(new Rect(10, 50, 1, 1), "управления", _guiStyle);
        }

        private void DrawButtonNameLabel(int y, string messege)
        {
            _guiStyle.fontStyle = FontStyle.Bold;
            GUI.Label(new Rect(10, y, 1, 1), messege, _guiStyle);
        }

        private void DrawTooltipeLabel(int y, string messege)
        {
            _guiStyle.fontStyle = FontStyle.Normal;
            GUI.Label(new Rect(22, y, 1, 1), messege, _guiStyle);
        }
    }
}
