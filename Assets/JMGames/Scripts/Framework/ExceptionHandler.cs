using System;
using System.IO;
using UnityEngine;

namespace JMGames.Scripts.Framework
{
    public class ExceptionHandler
    {
        public static void HandleException(Exception exception)
        {
            if (!Application.isEditor)
            {
                File.AppendAllText("LOGS\\" + DateTime.Now.ToString("yyyyMMdd") + "_Exceptions.txt", exception.ToString());
            }
            else
            {
                Debug.LogException(exception);
            }
        }
    }
}
