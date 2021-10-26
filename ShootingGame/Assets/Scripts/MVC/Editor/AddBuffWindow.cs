using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Model.ShootingGame
{
    public class AddBuffWindow : EditorWindow
    {
        private string _nameObject;
        private int _buffValue;
        private int _buffDuration;
        private BuffTypes _buffType;
        private AddBuffObjectEditor _addBuffObjectEditor;

        public void Open(AddBuffObjectEditor addBuffObjectEditor)
        {
            _addBuffObjectEditor = addBuffObjectEditor;
            EditorWindow.GetWindow(typeof(AddBuffWindow), false, "BuffWindow", true);
        }

        private void OnGUI()
        {
            GUILayout.Label("Добавление нового баффа", EditorStyles.boldLabel);

            _nameObject = EditorGUILayout.TextField("Имя объекта", _nameObject);
            _buffValue = EditorGUILayout.IntSlider("Сила баффа", _buffValue, 1, 5);
            _buffDuration = EditorGUILayout.IntSlider("Длительность баффа", _buffDuration, 1, 10);
            _buffType = (BuffTypes) EditorGUILayout.EnumPopup("Тип баффа", _buffType);

            var createButton = GUILayout.Button("Создать бафф");
            if (createButton)
            {
                BuffData buffScriptbleObject = ScriptableObject.CreateInstance<BuffData>();
                buffScriptbleObject.SetValue(_buffValue, _buffDuration, _buffType);
                AssetDatabase.CreateAsset(buffScriptbleObject, $"Assets/Resources/Data/Buffs/{_nameObject}.asset");
                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(buffScriptbleObject);
                _addBuffObjectEditor.scriptbleObjectsArrayIndex = 0;
                this.Close();
            }

            var cancelButton = GUILayout.Button("Отмена");
            if (cancelButton)
            {
                this.Close();
            }
        }
    }
}