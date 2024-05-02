using Illumate.Kit;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Illumate.RuntimeDebugPanel
{
    public class TextPanel : RDPPanelBase
    {
        [SerializeField] private TMP_Text tmpText;

        private void Start()
        {
            tmpText.text = IllumateConfigSC.Instance.rdpSettings.infoText;
        }
    }
}
