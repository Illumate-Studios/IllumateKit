using UnityEngine;

namespace Illumate
{
    public static class Clipboard
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void CopyToClipboard(string text);
#endif

        public static void CopyToClipboard(string text)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            CopyToClipboard(text);
#else
            GUIUtility.systemCopyBuffer = text;
#endif
        }
    }
}
