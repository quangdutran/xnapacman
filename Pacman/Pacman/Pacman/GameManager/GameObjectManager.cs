using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pacman.GameObjects
{
    class GameObjectManager
    {

        #region Fields

        List<MonsterGameObject> monsters = new List<MonsterGameObject>();
        List<DotGameObject> dots = new List<DotGameObject>();
        List<WallGameObject> walls = new List<WallGameObject>();
        List<PortalGameObject> portals = new List<PortalGameObject>();
        List<FruitGameObject> fruits = new List<FruitGameObject>();

        PacmanGameObject pacman;
        MonsterGameObject monsterHouse;

        #endregion

        #region Initialization

        GameObjectManager()
        {
            MonsterGameObject monster = new MonsterGameObject(MonsterGameObject.MonsterGameObjectColor.Blue);
            monsters.Add(monster);
        }

        #endregion

    }
}
