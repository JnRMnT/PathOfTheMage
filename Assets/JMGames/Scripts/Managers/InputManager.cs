using DuloGames.UI;
using Invector.CharacterController;
using JMGames.Framework;
using JMGames.Scripts.Constants;
using JMGames.Common.Entities;
using JMGames.Scripts.ObjectControllers;
using JMGames.Scripts.ObjectControllers.Character;
using JMGames.Scripts.Spells;
using JMGames.Scripts.UI.Window;
using JMGames.Scripts.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JMGames.Scripts.Managers
{
    public class InputManager : JMBehaviour
    {
        #region variables

        public static InputManager Instance;
        [Header("Default Inputs")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Settings")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";

        protected vThirdPersonCamera tpCamera;                // acess camera info        
        [HideInInspector]
        public string customCameraState;                    // generic string to change the CameraState        
        [HideInInspector]
        public string customlookAtPoint;                    // generic string to change the CameraPoint of the Fixed Point Mode        
        [HideInInspector]
        public bool changeCameraState;                      // generic bool to change the CameraState        
        [HideInInspector]
        public bool smoothCameraState;                      // generic bool to know if the state will change with or without lerp  
        [HideInInspector]
        public bool keepDirection;                          // keep the current direction in case you change the cameraState

        protected vThirdPersonController cc;                // access the ThirdPersonController component  
        [HideInInspector]
        public int LayerMaskExcludingPlayer;
        [HideInInspector]
        public int WorldLayerMask;

        public UIWindow PauseMenu;
        public AreaSelector AreaSelector;
        public bool IsAreaSelectorActive
        {
            get
            {
                return AreaSelector.gameObject.activeInHierarchy;
            }
        }
        #endregion

        public override void DoStart()
        {
            Instance = this;
            LayerMaskExcludingPlayer = RaycastingUtilities.CreateLayerMask(true, LayerMask.NameToLayer(LayerConstants.Player));
            WorldLayerMask = RaycastingUtilities.CreateLayerMask(false, LayerMask.NameToLayer(LayerConstants.World));
            CharacterInit();            
            base.DoStart();
        }

        protected virtual void CharacterInit()
        {
            cc = GetComponent<vThirdPersonController>();
            if (cc != null)
                cc.Init();

            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera) tpCamera.SetMainTarget(this.transform);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            GetComponent<Rigidbody>().useGravity = true;
        }

        public override void DoLateUpdate()
        {
            if (cc == null) return;             // returns if didn't find the controller		    
            InputHandle();                      // update input methods
            UpdateCameraStates();               // update camera states
        }

        public override void DoFixedUpdate()
        {
            if (cc == null) return;
            cc.AirControl();
            if (!Cursor.visible)
            {
                CameraInput();
            }
        }


        public override void DoUpdate()
        {
            if (cc == null) return;
            cc.UpdateMotor();                   // call ThirdPersonMotor methods               
            cc.UpdateAnimator();                // call ThirdPersonAnimator methods
            base.DoUpdate();
        }

        protected virtual void InputHandle()
        {
            if (GameStateManager.Instance != null && GameStateManager.Instance.CurrentState == GameStateEnum.Playing)
            {
                ExitGameInput();
                CameraInput();

                if (!cc.lockMovement)
                {
                    MoveCharacter();
                    SprintInput();
                    StrafeInput();
                    JumpInput();
                    if (cc.speed < 0.3f)
                    {
                        SpellInput();
                        AOESelectionInput();
                        InteractionInput();
                    }
                    else
                    {
                        MainPlayerController.Instance.StopAlignment();
                    }
                }
            }
        }

        #region Basic Locomotion Inputs      

        protected virtual void MoveCharacter()
        {
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.y = Input.GetAxis(verticallInput);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput))
                cc.Jump();
        }

        protected virtual void ExitGameInput()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (UIWindowManager.Instance != null && UIWindowManager.Instance.escapedUsed)
                {
                    UIManager.Instance.SetCursorVisibility(false);
                    return;
                }

                if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
                {
                    UIManager.Instance.SetCursorVisibility(true);
                }
                else
                {
                    //Show menu
                    PauseMenuWindow.Instance.Pause();
                }
            }
        }

        #endregion
        #region Action Inputs
        private void InteractionInput()
        {
            if (UIManager.Instance != null && UIManager.Instance.InteractionText != null 
                && UIManager.Instance.InteractionText.isActiveAndEnabled && Input.GetButton(InputConstants.InteractionKey))
            {
                UIManager.Instance.InteractionText.Interact();
            }
        }
        #endregion
        #region Spell Inputs
        private void SpellInput()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpellManager.Instance.CastSpell(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpellManager.Instance.CastSpell(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SpellManager.Instance.CastSpell(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SpellManager.Instance.CastSpell(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SpellManager.Instance.CastSpell(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SpellManager.Instance.CastSpell(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SpellManager.Instance.CastSpell(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SpellManager.Instance.CastSpell(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SpellManager.Instance.CastSpell(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SpellManager.Instance.CastSpell(9);
            }
            else if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                SpellManager.Instance.CastSpell(10);
            }
            else if (Input.GetKeyDown(KeyCode.Minus))
            {
                SpellManager.Instance.CastSpell(11);
            }
        }

        protected virtual void AOESelectionInput()
        {
            if (IsAreaSelectorActive && Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (!AreaSelector.IsValid)
                {
                    AreaSelector.gameObject.SetActive(false);
                }
                else if (SpellManager.Instance.TriggerAnimationAndCast(AreaSelector.transform.position))
                {
                    AreaSelector.gameObject.SetActive(false);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                AreaSelector.gameObject.SetActive(false);
                SpellManager.Instance.ActiveSpell = null;
            }
        }

        public void InitializeAreaSelector(BaseSpell spell)
        {
            AreaSelector.gameObject.SetActive(true);
            AreaSelector.Initialize(spell.AOERadius);
        }
        #endregion
        #region Camera Methods

        protected virtual void CameraInput()
        {
            if (!Cursor.visible)
            {
                if (tpCamera == null)
                    return;
                var Y = Input.GetAxis(rotateCameraYInput);
                var X = Input.GetAxis(rotateCameraXInput);

                tpCamera.RotateCamera(X, Y);

                // tranform Character direction from camera if not KeepDirection
                if (!keepDirection)
                    cc.UpdateTargetDirection(tpCamera != null ? tpCamera.transform : null);
                // rotate the character with the camera while strafing        
                RotateWithCamera(tpCamera != null ? tpCamera.transform : null);
            }
            else if (CheckForMenuDisableClick())
            {
                Cursor.visible = false;
            }
        }

        protected bool CheckForMenuDisableClick()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                return true;
            }
            return false;
        }

        protected virtual void UpdateCameraStates()
        {
            // CAMERA STATE - you can change the CameraState here, the bool means if you want lerp of not, make sure to use the same CameraState String that you named on TPCameraListData
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void RotateWithCamera(Transform cameraTransform)
        {
            if (cc.isStrafing && !cc.lockMovement && !cc.lockMovement)
            {
                cc.RotateWithAnotherTransform(cameraTransform);
            }
        }
        #endregion

        #region Public Properties
        public bool HasMovementLock
        {
            get
            {
                return cc.lockMovement;
            }
        }
        #endregion
    }
}
