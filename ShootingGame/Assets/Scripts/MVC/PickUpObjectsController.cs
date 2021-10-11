using Model.ShootingGame;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickUpObjectsController: IFixedExecute, IController, ITwitching, IRotateble
{
    private Dictionary<GameObject,float> _objectList = new Dictionary<GameObject, float>();
    private RadarController _radarController;

    private const int Z_ANGLE = 3;
    private const float TWITCH_SPEED = 3f;
    private const float TWITCH_AMPLITUDE = 0.2f;


    public PickUpObjectsController(Player player, RadarController radarController)
    {
        _radarController = radarController;
        player.buffObjectCollected += DestroyBuffObject;
        player.keyObjectCollected += DestroyBuffObject;
    }

    public void AddObjectsList(Dictionary<GameObject, float> objectList)
    {
        if (_objectList.Count < 1)
        {
            _objectList = objectList;
        } else
        {
            foreach (var element in objectList)
            {
                _objectList.CheckOnParentRepeats(element.Key);
                _objectList.Add(element.Key, element.Value);
            }
        }
    }

    public void DestroyBuffObject(GameObject gameObject)
    {
        for (int element=0; element < _objectList.Keys.Count; element++)
        {
            if (_objectList.Keys.ElementAt(element).transform.parent.GetInstanceID() == gameObject.transform.GetInstanceID())
            {
                _objectList.Remove(_objectList.Keys.ElementAt(element));
                _radarController.RemoveRadarObject(gameObject);
                //Object.Destroy(gameObject); //чит для более легкого сохранения
                gameObject.SetActive(false);
                return;
            }
        }
    }

    public void FixedExecute(float fixedTime)
    {
        foreach (var element in _objectList)
        {
            RotateBuff(element.Key);
            TwitchBuf(element.Key, element.Value, fixedTime);
        }
    }

    public void TwitchBuf(GameObject buffObject, float baseY, float fixedTime)
    {
        buffObject.transform.position = new Vector3(buffObject.transform.position.x, baseY + Mathf.Sin(fixedTime * TWITCH_SPEED) * TWITCH_AMPLITUDE, buffObject.transform.position.z);
    }

    public void RotateBuff(GameObject buffObject)
    {
        buffObject.transform.Rotate(0, 0, Z_ANGLE);
    }
}