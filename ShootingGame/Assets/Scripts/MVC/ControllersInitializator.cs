using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.ShootingGame
{
    public class ControllersInitializator
    {
        public ControllersInitializator(Player player, IBuff[] buffObjects)
        {
            new BuffController(player.Parameters, buffObjects);
        }
    }
}