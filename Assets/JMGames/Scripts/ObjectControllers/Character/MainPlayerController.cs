using System;
using Invector.CharacterController;
using JMGames.Scripts.Managers;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers.Character
{
    public class MainPlayerController : BaseCharacterController
    {
        public Transform LeftArm, RightArm, LeftHand, RightHand;

        private Vector3 RequiredRotationAngles;

        public InputManager InputManager;
        public Animator Animator;
        public static MainPlayerController Instance;
        public override void DoStart()
        {
            Instance = this;
            base.DoStart();
        }

        public bool IsInAnimation(string stateName)
        {
            return Animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        private float AngleBetweenPoints(Vector2 a, Vector2 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        public void AlignWithCamera(Vector3 rotationOffset)
        {
            RequiredRotationAngles = new Vector3(0f, Camera.main.transform.rotation.eulerAngles.y, 0f) + rotationOffset;
        }

        public void StopAlignment()
        {
            RequiredRotationAngles = Vector3.zero;
        }

            public bool IsAlignedWithCamera()
        {
            return Quaternion.Angle(transform.rotation, Quaternion.Euler(RequiredRotationAngles)) < 1f;
        }

        public override void DoUpdate()
        {
            if (RequiredRotationAngles != Vector3.zero && !IsAlignedWithCamera())
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(RequiredRotationAngles), Time.fixedDeltaTime * 30f);
            }
            else
            {
                RequiredRotationAngles = Vector3.zero;
            }
            base.DoUpdate();
        }
    }
}