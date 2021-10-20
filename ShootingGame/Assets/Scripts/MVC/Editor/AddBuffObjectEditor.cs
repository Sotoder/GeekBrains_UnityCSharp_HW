#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    [CustomEditor(typeof(AddBuffObject))]
    public class AddBuffObjectEditor : Editor
    {
		private AddBuffObject _target;
		private bool _isDataSaved;
		private AddBuffWindow _buffWindow;

		public int scriptbleObjectsArrayIndex;

		private void OnEnable()
		{
			_target = (AddBuffObject)target;
		}

		public override void OnInspectorGUI()
		{
			Resources.LoadAll<BuffData>("Data/Buffs");			
			var scriptbleObjects = Resources.FindObjectsOfTypeAll<BuffData>();

			var scriptbleObjectsNames = new string[scriptbleObjects.Length + 1];
			for(int element = 0; element < scriptbleObjects.Length; element++)
            {
				scriptbleObjectsNames[element] = scriptbleObjects[element].name;
            }

			scriptbleObjectsNames[scriptbleObjectsNames.Length - 1] = "Add buff";

			scriptbleObjectsArrayIndex = EditorGUILayout.Popup("Бафф для объекта", scriptbleObjectsArrayIndex, scriptbleObjectsNames);

            if (scriptbleObjectsArrayIndex == scriptbleObjectsNames.Length - 1)
            {
				scriptbleObjectsArrayIndex = 0;
				_buffWindow = ScriptableObject.CreateInstance<AddBuffWindow>();
				_buffWindow.Open(this);

				//var buffWindow = EditorWindow.GetWindow(typeof(AddBuffWindow), false, "BuffWindow", true);
            }
            else
            {
				var scriptbleObjectPath = AssetDatabase.GetAssetPath(scriptbleObjects[scriptbleObjectsArrayIndex]);
				EditorGUILayout.SelectableLabel($"Путь к экземпляру BuffData: {scriptbleObjectPath}", EditorStyles.textField, GUILayout.Height(20f));

				_target.buffData = scriptbleObjects[scriptbleObjectsArrayIndex];
				_target.icoForRadarObject = EditorGUILayout.ObjectField("Иконка для радара", _target.icoForRadarObject, typeof(Image), false) as Image;
				_target.gameStarter = EditorGUILayout.ObjectField("GameStarter", _target.gameStarter, typeof(GameStarter), true) as GameStarter;
			}


            var isPressSaveButton = GUILayout.Button("Начать создание объектов", EditorStyles.miniButton);
			var isPressCancelButton = GUILayout.Button("Прекратить создание объектов", EditorStyles.miniButton);

			if (isPressSaveButton)
			{
				_isDataSaved = true;
			}

			if (isPressCancelButton)
			{
				_isDataSaved = false;
			}
		}

			private void OnSceneGUI()
		{
			if (Event.current.button == 0 && Event.current.type == EventType.MouseDown && _isDataSaved)
			{
				Ray ray = Camera.current.ScreenPointToRay(new Vector3(Event.current.mousePosition.x,
					SceneView.currentDrawingSceneView.camera.pixelHeight - Event.current.mousePosition.y));

				if (Physics.Raycast(ray, out var hit))
				{
					_target.InstantiateObj(hit.point);
					SetObjectDirty(_target.gameObject);
				}
			}
			Selection.activeGameObject = _target.gameObject;
		}

		public void SetObjectDirty(GameObject obj)
		{
			if (!Application.isPlaying)
			{
				EditorUtility.SetDirty(obj);
				EditorSceneManager.MarkSceneDirty(obj.scene);
			}
		}
	}
}
#endif