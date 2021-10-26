using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model.ShootingGame
{
	public class RadarController: IController, IExecute
	{
		private GameObject _radar;
		private Transform _player;
		private List<RadarObject> RadObjects = new List<RadarObject>();

		private const float MAP_SCALE = 2;

		public RadarController(IPlayer player, GameObject radar)
        {
			_radar = radar;
			_player = player.PlayerData.GameObject.transform;
        }
		
		public void Execute(float deltaTime)
		{
			if (Time.frameCount % 2 == 0)
			{
				DrawRadarDots();
			}
		}

		private void DrawRadarDots()
		{
			foreach (RadarObject radObject in RadObjects)
			{
				Vector3 radarPos = (radObject.Owner.transform.position -
									_player.position);
				float distToObject = Vector3.Distance(_player.position,
										 radObject.Owner.transform.position) * MAP_SCALE;
				float deltay = Mathf.Atan2(radarPos.x, radarPos.z) * Mathf.Rad2Deg -
							   270 - _player.eulerAngles.y;
				radarPos.x = distToObject * Mathf.Cos(deltay * Mathf.Deg2Rad) * -1;
				radarPos.z = distToObject * Mathf.Sin(deltay * Mathf.Deg2Rad);
				radObject.Icon.transform.SetParent(_radar.transform);
				radObject.Icon.transform.position = new Vector3(radarPos.x,
														radarPos.z, 0) + _radar.transform.position;
			}
		}
		public void RegisterRadarObject(GameObject o, Image i)
		{
			Image image = Object.Instantiate(i);
			RadObjects.Add(new RadarObject { Owner = o, Icon = image });
		}

		public void RemoveRadarObject(GameObject o)
		{
			List<RadarObject> newList = new List<RadarObject>();
			foreach (RadarObject element in RadObjects)
			{
				if (element.Owner == o)
				{
					Object.Destroy(element.Icon);
					continue;
				}
				newList.Add(element);
			}
			RadObjects.RemoveRange(0, RadObjects.Count);
			RadObjects.AddRange(newList);
		}
	}
}
