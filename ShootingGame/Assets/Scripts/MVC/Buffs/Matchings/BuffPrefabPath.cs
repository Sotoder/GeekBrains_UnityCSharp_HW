using System;
using System.Collections.Generic;

namespace Model.ShootingGame
{
    [Serializable]
    public sealed class BuffPrefabPath
    {
        public readonly Dictionary<BuffTypes, string> prefabsPaths = new Dictionary<BuffTypes, string>
        {
            [BuffTypes.Speed] = "Prefabs/Buffs/Speed",
            [BuffTypes.Regeneration] = "Prefabs/Buffs/Regeneration",
            [BuffTypes.Rage] = "Prefabs/Buffs/Rage",
            [BuffTypes.Heal] = "Prefabs/Buffs/Heal"
        };
    }
}
