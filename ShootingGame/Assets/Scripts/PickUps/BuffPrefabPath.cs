using System;
using System.Collections.Generic;

namespace Model.ShootingGame
{
    [Serializable]
    public class BuffPrefabPath
    {
        public readonly Dictionary<BuffTypes, string> prefabsPaths = new Dictionary<BuffTypes, string>
        {
            [BuffTypes.Speed] = "Prefabs/Buffs/Speed.prefab",
            [BuffTypes.Regeneration] = "Prefabs/Buffs/Regeneration.prefab",
            [BuffTypes.Rage] = "Prefabs/Buffs/Rage.prefab",
            [BuffTypes.Regeneration] = "Prefabs/Buffs/AttackSpeed.prefab"
        };
    }
}
