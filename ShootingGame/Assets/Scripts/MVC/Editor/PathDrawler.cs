using UnityEngine;
using UnityEditor;

namespace Model.ShootingGame
{
    [CustomPropertyDrawer(typeof(PathAttribute))]
    public class PathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            PathAttribute path = attribute as PathAttribute;

            if (property.stringValue != "")
            {
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 1;

                EditorGUI.LabelField(position, "Путь к объекту", property.stringValue);
                path.tgl = EditorGUI.Toggle(new Rect (position.x-20, position.y, position.width, position.height), path.tgl);
                if (path.tgl)
                {
                    property.stringValue = "";
                    path.obj = null;
                    path.tgl = false;
                }
            } else
            {
                if (path.obj is null)
                {
                    path.obj = EditorGUI.ObjectField(position, "BuffData", path.obj, typeof(BuffData), false) as BuffData;
                }
                else
                {
                    var pathString = AssetDatabase.GetAssetPath(path.obj);
                    property.stringValue = pathString;
                    EditorGUI.LabelField(position, "Путь к объекту", pathString);
                }
            }        
        }
    }
}
