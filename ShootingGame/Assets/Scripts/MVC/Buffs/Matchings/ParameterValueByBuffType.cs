using System;
using System.Collections.Generic;

namespace Model.ShootingGame
{
    public sealed class ParameterValueByBuffType
    {
        private RefOnClassFild<int> _speedRef;
        private RefOnClassFild<int> _curentHPRef;
        private RefOnClassFild<int> _attackPowerRef;

        private Dictionary<BuffTypes, RefOnClassFild<int>> _matchings;
        public Dictionary<BuffTypes, RefOnClassFild<int>> Matchings { get => _matchings; }
        public ParameterValueByBuffType(Parameters playerParameters)
        {
            CreateMatchingTable(playerParameters);
        }

        private void CreateMatchingTable(Parameters playerParameters)
        {
            _speedRef = new RefOnClassFild<int>(() => playerParameters.speed, (value) => { playerParameters.speed = value; });
            _curentHPRef = new RefOnClassFild<int>(() => playerParameters.currentHP, (value) => { playerParameters.currentHP = value; });
            _attackPowerRef = new RefOnClassFild<int>(() => playerParameters.attackPower, (value) => { playerParameters.attackPower = value; });

            _matchings = new Dictionary<BuffTypes, RefOnClassFild<int>>
            {
                [BuffTypes.Speed] = _speedRef,
                [BuffTypes.Regeneration] = _curentHPRef,
                [BuffTypes.Rage] = _attackPowerRef,
                [BuffTypes.Heal] = _curentHPRef
            };
        }
    }
}
