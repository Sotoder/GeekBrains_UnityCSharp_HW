using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class BuffController
    {
        private delegate void BuffMethod(BuffStructure buff);

        private Parameters _playerParameters;
        private IBuff[] _buffObjects;
        private Dictionary<BuffTypes, BuffMethod> _buffMethods;

        public BuffController(Parameters parameters, IBuff[] buffObjects)
        {
            _playerParameters = parameters;
            _buffObjects = buffObjects;

            SignOnBuffInstance(_buffObjects);

            _buffMethods = new Dictionary<BuffTypes, BuffMethod>
            {
                [BuffTypes.Speed] = TemporaryBuff,
                [BuffTypes.Regeneration] = TickBuff,
                [BuffTypes.Rage] = TemporaryBuff,
                [BuffTypes.Heal] = InstantBuff
            };
        }

        private void SignOnBuffInstance(IBuff[] buffObjects)
        {
            if (_buffObjects.Length > 0)
            {
                for (int i = 0; i < _buffObjects.Length; i++)
                {
                    _buffObjects[i].BuffCollected += ApplyBaff;
                }
            }
        }

        private void ApplyBaff(BuffStructure buff)
        {
            _buffMethods[buff.BuffType](buff);
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