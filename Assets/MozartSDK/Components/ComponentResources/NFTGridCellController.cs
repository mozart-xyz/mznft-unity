namespace Mozart
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    using UnlimitedScrollUI;
    using TMPro;
    public class NFTGridCellController : RegularCell
    {
        public TextMeshProUGUI text;
        public RawImage image;
        public MozartInventoryController inventoryController;
        public NFTItem cellData;
        public Toggle selection;

        public void SetText(string title)
        {
            text.text = $"{title}";
        }

        public void SetupCell(MozartInventoryController inventory, NFTItem data)
        {
            inventoryController = inventory;
            cellData = data;
            Redraw();
        }

        public void Redraw()
        {
            SetImage(cellData.image);
            SetText(cellData.name);
            SetupClickHandler();
        }

        public void OnDestroy()
        {
            GetComponent<Button>().onClick.RemoveListener(CellClickHandler);
        }

        private void SetImage(string url)
        {
            image.CrossFadeAlpha(0f, 0f, true);
            StartCoroutine(GetTexture(url));
        }

        private void CellClickHandler()
        {
            selection.isOn = true;
            inventoryController.CellClicked(this);
        }

        private void SetupClickHandler()
        {
            GetComponent<Button>().onClick.AddListener(CellClickHandler);
        }

        IEnumerator GetTexture(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
            image.CrossFadeAlpha(1f, 0.5f, true);
        }
    }
}