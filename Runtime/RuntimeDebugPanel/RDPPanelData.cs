using UnityEngine;

namespace Illumate.RuntimeDebugPanel
{
    [System.Serializable]
    internal class RDPPanelsSettings
    {
        public RDPPanelBase[] panels;
        [TextArea(minLines: 2, maxLines: 5)]
        public string infoText = "Illumate Studios";
    }
}
