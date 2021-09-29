using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public sealed class BuffController
    {
        private delegate void BuffMethod(BuffData buffData);

        private Parameters _playerParameters;
        private BuffBehaviour[] _buffObjects;
        private Dictionary<BuffTypes, BuffMethod> _buffMethods;

        public BuffController(Parameters parameters, BuffBehaviour[] buffObjects)
        {
            _playerParameters = parameters;
            _buffObjects = buffObjects;

            SignOnBuffInstance(_buffObjects);

            _buffMethods = new Dictionary<BuffTypes, BuffMethod>
            {
                [BuffTypes.Speed] = (buffData) =>   // јнонимный метод тут больше дл€ вопроса можно ли юзать именно анонимные или это не правильно, так как код становитс€ непон€тным? 
                                                    // ѕро залипание помню, но тут вроде нет его. Ќормальный метод ChangeSpeed ниже.
                                                    // ≈сли что все вопросы к —ергею, он говорил что это не страшно и вполне пон€тно))
                {
                    var buffStructure = buffData.buffStructure;
                    var pastSpeed = _playerParameters.speed;
                    _playerParameters.speed = _playerParameters.speed * buffStructure.BonusValue;
                    BuffTimer timer = new BuffTimer(buffStructure.BonusDuration);
                    timer.timeIsOver += () =>
                    {
                        _playerParameters.speed = _playerParameters.speed/buffStructure.BonusValue;
                        timer.Dispose();

                    };
                },
                [BuffTypes.Regeneration] = GetRegeneration,
                [BuffTypes.Rage] = GetRage,
                [BuffTypes.AttackSpeed] = ChangeAttackSpeed
            };
        }

        private void SignOnBuffInstance(BuffBehaviour[] buffObjects)
        {
            if (_buffObjects.Length > 0)
            {
                for (int i = 0; i < _buffObjects.Length; i++)
                {
                    _buffObjects[i].buffCollected += ApplyBaff;
                }
            }
        }

        private void ApplyBaff(BuffData buff)
        {
            _buffMethods[buff.buffStructure.BuffType](buff);
        }

        private void GetRegeneration(BuffData buff)
        {
            Debug.Log("Feel good");
        }

        private void ChangeAttackSpeed(BuffData buffData)
        {
            Debug.Log("Hit like a bee!");
        }

        private void GetRage(BuffData buffData)
        {
            Debug.Log("POWER!!");
        }

        //private void ChangeSpeed(BuffData buffData)
        //{
        //    var buffStructure = buffData.buffStructure;
        //    var pastSpeed = _playerParameters.speed;
        //    _playerParameters.speed = _playerParameters.speed * buffStructure.BonusValue;
        //    BuffTimer timer = new BuffTimer(buffStructure.BonusDuration);
        //    timer.timeIsOver += () =>
        //    {
        //        _playerParameters.speed = _playerParameters.speed / buffStructure.BonusValue;
        //        timer.Dispose();

        //    };
        //}
    }
}