using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers.Character
{
    [RequireComponent(typeof(ManaPool), typeof(Life), typeof(Armor))]
    public class BaseCharacterController : JMBehaviour
    {
        public ManaPool ManaPool;
        public Life Health;
        public Armor Armor;
        public Experience Experience;
    }
}