using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace UnlimitedScrollUI.Example {
    public class CellTest : MonoBehaviour {
        public Text text;
        public GameObject popup;
        public RawImage image;
        private InfoDisplay infoDisplay;
        private int index;
        
        public void SetText(int newIndex) {
            index = newIndex;
            text.text = $"{index}";
        }

        public void SetupPopup(int newIndex) {
            index = newIndex;
            image.CrossFadeAlpha(0f, 0f, true);
            StartCoroutine(GetTexture());
            /*GetComponent<Button>().onClick.AddListener(() => {
                var instance = Instantiate(popup, GameObject.Find("Canvas").transform);
                instance.GetComponent<Popup>().text.text = $"You just clicked the cell {index}!";
            });*/
        }

        IEnumerator GetTexture()
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture("https://mozart.xyz/favicon.png");
            yield return www.SendWebRequest();

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
            image.CrossFadeAlpha(1f, 0.5f, true);
        }

        public void GetInfoDisplayer() {
            infoDisplay = FindObjectOfType<InfoDisplay>();
        }

        public void ChangeCount(int count) {
            if (!infoDisplay) return;
            
            infoDisplay.UpdateCellCount(count);
        }

        public void DisplayVisibleText(ScrollerPanelSide side) {
            if (!infoDisplay) return;
            
            var sideName = Enum.GetName(typeof(ScrollerPanelSide), side);
            infoDisplay.UpdateVisibleDisplay($"Cell {index} visible from {sideName}.");
        }
        
        public void DisplayInvisibleText(ScrollerPanelSide side) {
            if (!infoDisplay) return;
            
            var sideName = Enum.GetName(typeof(ScrollerPanelSide), side);
            infoDisplay.UpdateInvisibleDisplay($"Cell {index} invisible to {sideName}.");
        }
    }
}
