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
		private BuffData[] _scriptbleObjects;
		private int _scriptbleObjectsArrayIndex;
		private bool _isDataSaved;

		private void OnEnable()
		{
			_target = (AddBuffObject)target;
		}

		public override void OnInspectorGUI()
		{
			Resources.LoadAll<BuffData>("Data/Buffs");			
			_scriptbleObjects = Resources.FindObjectsOfTypeAll<BuffData>();

			var scriptbleObjectsNames = new string[_scriptbleObjects.Length];
			for(int element = 0; element < _scriptbleObjects.Length; element++)
            {
				scriptbleObjectsNames[element] = _scriptbleObjects[element].name;
            }

			_target.obj = EditorGUILayout.ObjectField("Префаб или объект для клонирования", _target.obj, typeof(GameObject), true) as GameObject;
			_scriptbleObjectsArrayIndex = EditorGUILayout.Popup("Бафф для объекта", _scriptbleObjectsArrayIndex, scriptbleObjectsNames);
			_target.buffData = _scriptbleObjects[_scriptbleObjectsArrayIndex];
			_target.icoForRadarObject = EditorGUILayout.ObjectField("Иконка для радара", _target.icoForRadarObject, typeof(Image), false) as Image;
			_target.gameStarter = EditorGUILayout.ObjectField("GameStarter", _target.gameStarter, typeof(GameStarter), true) as GameStarter;

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