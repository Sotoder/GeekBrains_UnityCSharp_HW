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
            GUILayout.Label("���������� ������ �����", EditorStyles.boldLabel);

            _nameObject = EditorGUILayout.TextField("��� �������", _nameObject);
            _buffValue = EditorGUILayout.IntSlider("���� �����", _buffValue, 1, 5);
            _buffDuration = EditorGUILayout.IntSlider("������������ �����", _buffDuration, 1, 10);
            _buffType = (BuffTypes) EditorGUILayout.EnumPopup("��� �����", _buffType);

            var createButton = GUILayout.Button("������� ����");
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

            var cancelButton = GUILayout.Button("������");
            if (cancelButton)
            {
                this.Close();
            }
        }
    }
}