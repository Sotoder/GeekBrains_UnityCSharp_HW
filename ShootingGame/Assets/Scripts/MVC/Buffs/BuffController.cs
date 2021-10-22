using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class BuffController: IController
    {
        private delegate void BuffMethod(BuffStructure buff);

        private Parameters _playerParameters;
        private List<BuffObject> _buffObjects;
        private Dictionary<BuffTypes, BuffMethod> _timerMethods;

        public BuffController(IPlayer player, List<BuffObject> buffObjects)
        {
            _playerParameters = player.PlayerData.Parameters;
            _buffObjects = buffObjects;

            player.BuffObjectCollected += CheckObjectInBuffCollection;

            _timerMethods = new Dictionary<BuffTypes, BuffMethod>
            {
                [BuffTypes.Speed] = TemporaryBuff,
                [BuffTypes.Regeneration] = TickBuff,
                [BuffTypes.Rage] = TemporaryBuff,
                [BuffTypes.Heal] = InstantBuff
            };
        }

        private void CheckObjectInBuffCollection(GameObject gameObject)
        {
            foreach (var element in _buffObjects)
            {
                if (element.Object.GetInstanceID() == gameObject.GetInstanceID())
                {
                    ApplyBaff(element.BuffData.BuffStruct);
                }
            }
        }

        private void ApplyBaff(BuffStructure buff)
        {
            _timerMethods[buff.BuffType](buff);
        }

        private void TemporaryBuff(BuffStructure buff)
        {
            ParameterValueByBuffType parameterValueByType = new ParameterValueByBuffType(_playerParameters);

            parameterValueByType.Matchings[buff.BuffType].Value = parameterValueByType.Matchings[buff.BuffType].Value * buff.BonusValue;
            CountdownTimer timer = new CountdownTimer(buff.BonusDuration);
            
            timer.timeIsOver += () =>
            {
                parameterValueByType.Matchings[buff.BuffType].Value = parameterValueByType.Matchings[buff.BuffType].Value / buff.BonusValue;
                timer.Dispose();
            };
        }

        private void InstantBuff(BuffStructure buff)
        {
            Debug.Log("Get Instant Buff");
        }

        private void TickBuff(BuffStructure buff)
        {
            Debug.Log("Get Tick Buff");
        }
    }
}