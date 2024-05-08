using TMPro;
using Illumate.API;
using UnityEngine.UI;
using Illumate.Kit;

namespace Illumate.RuntimeDebugPanel
{
    public class SendReportPanel : RDPPanelBase
    {
        public TMP_InputField inputField;
        public Button sendButton;
        public TMP_Text sendButtonText;

        private string log;

        private void Start()
        {
            sendButton.onClick.AddListener(SendReport);
        }

        private void SendReport()
        {
            string reportText = inputField.text == "" ? "No report text provided." : "- Report Text: " + inputField.text;
            Reporter.Log(reportText);
            inputField.interactable = false;
            sendButton.interactable = false;
            sendButtonText.text = "Sending...";
            log = Reporter.GenerateReport();
            IApi.SendRequest(new SendReport(UnityEngine.Application.identifier, log), OnReportResult);
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
                UnityEngine.GUIUtility.systemCopyBuffer = log;
                Modals.Alert("Failed to send report! Logs has been copied to your clipboard. You can send the logs to the developers.");
                UnityEngine.Debug.LogError("Failed to send report: " + result.message);
            }
        }
    }
}