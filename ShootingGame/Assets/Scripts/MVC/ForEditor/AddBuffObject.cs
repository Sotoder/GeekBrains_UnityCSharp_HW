#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
    public class AddBuffObject : MonoBehaviour
    {
		public BuffData buffData;
		public Image icoForRadarObject;
		public GameStarter gameStarter;

        public GameObject InstantiateObj(Vector3 pos)
		{
			var obj = new GameObject();
			var buffElement = Instantiate(obj, pos, Quaternion.identity, gameObject.transform);
			DestroyImmediate(obj);

			var sphereCollider = buffElement.AddComponent<SphereCollider>();
			sphereCollider.radius = 0.5f;
			sphereCollider.isTrigger = true;

			var buffObject = new BuffObject(buffElement, buffData, icoForRadarObject);

			gameStarter.DataForInitialization.BuffObjectsInitializationData.BuffObjectCollection.Add(buffObject);

			return buffElement;
		}
	}
}
#endif