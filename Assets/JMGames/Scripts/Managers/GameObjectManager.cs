using JMGames.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.Managers
{
    public class GameObjectManager
    {
        public static GameObject InstantiatePrefab(GameObject prefab, GameObjectTypeEnum objectType)
        {
            return GameObject.Instantiate(prefab);
        }
        public static GameObject InstantiatePrefab(GameObject prefab, Transform parent, GameObjectTypeEnum objectType)
        {
            return GameObject.Instantiate(prefab, parent);
        }
    }
}
