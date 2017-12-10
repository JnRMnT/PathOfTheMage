using JMGames.Framework;
using JMGames.Scripts.Behaviours;
using JMGames.Scripts.Behaviours.Actions;
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

        private Interactable activeInteractable;
        public Interactable ActiveInteractable
        {
            get
            {
                return activeInteractable;
            }
            set
            {
                if(activeInteractable != null && activeInteractable != value)
                {
                    activeInteractable.StopAllCoroutines();
                }
                activeInteractable = value;
            }
        }
        public bool InteractionKeyValid;
    }
}