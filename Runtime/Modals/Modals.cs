using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Illumate.Kit
{
    public class Modals : MonoBehaviour
    {
        [SerializeField] private GameObject modal;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Button okButton;
        [SerializeField] private Button cancelButton;

        private static Modals Instance => IllumateKitSingleton.Instance.modals;

        private void Start()
        {
            modal.SetActive(false);
        }


        public static void Info(string message, Action OnDismissCallback = null)
        {
            Instance.text.text = message;
            Instance.okButton.gameObject.SetActive(true);
            SetListener(Instance.okButton.onClick, OnDismissCallback);
            Instance.cancelButton.gameObject.SetActive(false);
            Instance.modal.SetActive(true);
        }

        public static void Confirm(string message, Action OnConfirmCallback = null, Action OnCancelCallback = null)
        {
            throw new NotImplementedException();
        }


        public static void BlockInfo(string message)
        {
            throw new NotImplementedException();
        }

        private static void SetListener(UnityEvent unityEvent, Action action)
        {
            unityEvent.RemoveAllListeners();
            unityEvent.AddListener(action.Invoke);
        }
    }
}
