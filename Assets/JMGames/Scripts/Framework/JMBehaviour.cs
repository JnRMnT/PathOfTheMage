using JMGames.Scripts.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace JMGames.Framework
{
    public class JMBehaviour : MonoBehaviour
    {
        #region Wrapping Default Methods
        void Awake()
        {
            try
            {
                DoAwake();
            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }

        void OnGUI()
        {
            try
            {
                DoOnGUI();
            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }

        void Start()
        {
            if (!LanguageManager.Ready)
            {
                StartCoroutine(WaitForResources());
            }
            else
            {
                InvokeDoStart();
            }
        }

        void Update()
        {
            try
            {
                DoUpdate();
            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }

        void FixedUpdate()
        {
            try
            {
                DoFixedUpdate();

            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }

        void LateUpdate()
        {
            try
            {
                DoLateUpdate();

            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }
        #endregion
        #region Wrapper Virtual Methods
        public virtual void DoStart()
        {

        }


        public virtual void DoUpdate()
        {

        }

        public virtual void DoFixedUpdate()
        {

        }

        public virtual void DoAwake()
        {

        }

        public virtual void DoOnGUI()
        {

        }

        public virtual void DoLateUpdate()
        {

        }

        #endregion
        private IEnumerator WaitForResources()
        {
            yield return null;
            if (!(this is LanguageManager) && (!LanguageManager.Ready))
            {
                yield return new WaitForSeconds(1f);
                WaitForResources();
            }
            else
            {
                InvokeDoStart();
            }
        }

        private void InvokeDoStart()
        {
            try
            {
                DoStart();
            }
            catch (Exception e)
            {
                //Exception Handling
                ExceptionHandler.HandleException(e);
            }
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}