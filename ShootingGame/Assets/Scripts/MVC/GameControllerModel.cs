using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public class GameControllerModel
    {
        private readonly List<IFixedExecute> _fixedControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<ILateExecute> _lateExecuteControllers;
        private readonly List<ITriggerableOnEnter> _triggerControllers;
        public GameControllerModel()
        {
            _executeControllers = new List<IExecute>(8);
            _lateExecuteControllers = new List<ILateExecute>(8);
            _fixedControllers = new List<IFixedExecute>(8);
            _triggerControllers = new List<ITriggerableOnEnter>(8);
        }

        public List<IExecute> ExecuteControllers => _executeControllers;

        public List<ILateExecute> LateExecuteControllers => _lateExecuteControllers;

        public List<ITriggerableOnEnter> TriggerEnterControllers => _triggerControllers;

        internal List<IFixedExecute> FixedControllers => _fixedControllers;
    }
}
