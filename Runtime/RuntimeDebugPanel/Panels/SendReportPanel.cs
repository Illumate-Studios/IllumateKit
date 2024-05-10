using TMPro;
using Illumate.API;
using UnityEngine.UI;
using Illumate.Kit;
using System.Collections;
using UnityEngine;

namespace Illumate.RuntimeDebugPanel
{
    public class SendReportPanel : RDPPanelBase
    {
        public TMP_InputField inputField;
        public Button sendButton;
        public Button copyButton;
        public TMP_Text sendButtonText;

        private string log;

        private void Start()
        {
            sendButton.onClick.AddListener(SendReport);
            copyButton.onClick.AddListener(CopyReport);
            
        }

        private void CopyReport()
        {
            log = Reporter.GenerateReport();
            GUIUtility.systemCopyBuffer = log;
            StartCoroutine(CopyButtonTextCR());
        }

        private void SendReport()
        {
            string reportText = inputField.text == "" ? "No report text provided." : "- Report Text: " + inputField.text;
            Reporter.Log(reportText);
            inputField.interactable = false;
            sendButton.interactable = false;
            sendButtonText.text = "Sending...";
            log = Reporter.GenerateReport();
            IApi.SendRequest(new SendReport(Application.identifier, log), OnReportResult);
        }


        private void OnReportResult(RequestResult result)
        {
            if (result.result)
            {
                sendButtonText.text = "Sent!";
            }
            else
            {
                sendButtonText.text = "Sending failed!";
                GUIUtility.systemCopyBuffer = log;
                Modals.Alert("Failed to send report! Logs has been copied to your clipboard. You can send the logs to the developers manually.");
                Reporter.LogError("SendReportPanel.OnReportResult | Failed to send report: " + result.message);
            }
        }

        private IEnumerator CopyButtonTextCR()
        {
            var txt = copyButton.GetComponentInChildren<TMP_Text>();
            txt.text = "Done!";
            yield return new WaitForSeconds(1);
            txt.text = "Copy";
        }
    }
}