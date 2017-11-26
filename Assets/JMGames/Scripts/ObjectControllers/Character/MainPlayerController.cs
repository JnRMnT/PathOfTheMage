using Invector.CharacterController;
using JMGames.Scripts.Managers;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class MainPlayerController : BaseCharacterController
    {
        //public InputManager InputManager;
        public Animator Animator;
        public static MainPlayerController Instance;
        public override void DoStart()
        {
            Instance = this;
            base.DoStart();
        }
    }
}