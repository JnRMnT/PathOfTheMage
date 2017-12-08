using JMGames.Framework;
using JMGames.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Scripts.ObjectControllers
{
    public class AreaSelector : JMBehaviour
    {
        protected Vector3 InvalidPosition = new Vector3(-1500, -1500, -1500);
        protected float InitialScaleFactor = 1f;

        public bool IsValid
        {
            get
            {
                return transform.position != InvalidPosition;
            }
        }

        public override void DoUpdate()
        {
            UpdateAOESelector();
            base.DoUpdate();
        }

        protected void UpdateAOESelector()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f, InputManager.Instance.WorldLayerMask))
            {
                transform.position = hit.point + Vector3.up * 0.1f;
            }
            else
            {
                transform.position = InvalidPosition;
            }
        }

        public void Initialize(float radius)
        {
            List<float> yPositions = new List<float>();
            foreach (Transform child in transform)
            {
                yPositions.Add(child.position.y);
            }
            this.transform.localScale = new Vector3(radius * InitialScaleFactor, radius * InitialScaleFactor, radius * InitialScaleFactor);
            int i = 0;
            foreach (Transform child in transform)
            {
                child.position = new Vector3(child.position.x, yPositions[i], child.position.z);
                i++;
            }

            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
