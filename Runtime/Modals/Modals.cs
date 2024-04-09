using System;
using UnityEngine;

namespace Illumate.Kit
{
    public class Modals : MonoBehaviour
    {
        public static void Info(string message, Action OnDismissCallback = null)
        {
            throw new NotImplementedException();
        }

        public static void Confirm(string message, Action OnConfirmCallback = null, Action OnCancelCallback = null)
        {
            throw new NotImplementedException();
        }

        public static void Alert(string message, Action OnDismissCallback = null)
        {
            // not implemented yet
            Debug.LogWarning("Alert: " + message);
            OnDismissCallback?.Invoke();
        }
    }
}
