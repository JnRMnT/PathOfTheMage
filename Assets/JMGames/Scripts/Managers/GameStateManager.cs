using JMGames.Framework;
using JMGames.Scripts.Entities;
using JMGames.Scripts.ObjectControllers.Character;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JMGames.Scripts.Managers
{
    public class GameStateManager : JMBehaviour
    {
        public LocationTypeEnum LocationType;
        public GameStateEnum CurrentState;
        public static GameStateManager Instance;

        public void Start()
        {
            Instance = this;
            InitializeState();
        }

        private void InitializeState()
        {
            //TODO:Initialize state based on scene here
            Scene scene = SceneManager.GetActiveScene();
            if (scene.name == "Outdoor")
            {
                LocationType = LocationTypeEnum.Outdoor;
                CurrentState = GameStateEnum.Playing;
            }
            else
            {
                LocationType = LocationTypeEnum.Testing;
                CurrentState = GameStateEnum.Playing;
            }


            EnviroSkyRendering skyRendering = Camera.main.GetComponent<EnviroSkyRendering>();
            if (LocationType != LocationTypeEnum.Outdoor)
            {
                skyRendering.enabled = false;
            }
            else
            {
                skyRendering.enabled = true;
            }
        }
    }
}
