namespace Mozart
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.UI;
    using UnlimitedScrollUI;
    using TMPro;
    /// <summary>
    /// This is the code that controls how a Grid Cell renders itself.
    /// You may want to override this class and use your own custom cell logic.
    /// It is a good idea to override this class so that if the SDK is updated you will not lose
    /// your changes.
    /// </summary>
    public class NFTGridCellController : RegularCell
    {
        public TextMeshProUGUI text;
        public RawImage image;
        public MozartInventoryController inventoryController;
        public NFTItem cellData;
        public Toggle selection;
        public virtual void SetText(string title)
        {
            text.text = $"{title}";
        }

        public virtual void SetupCell(MozartInventoryController inventory, NFTItem data)
        {
            inventoryController = inventory;
            cellData = data;
            SetupClickHandler();
            Redraw();
        }

        public virtual void Redraw()
        {
            SetImage(cellData.image);
            SetText(cellData.name);
        }

        public void OnDestroy()
        {
            selection.onValueChanged.RemoveListener(CellClickHandler);
        }

        protected virtual void SetImage(string url)
        {
            image.CrossFadeAlpha(0f, 0f, true);
            StartCoroutine(GetTexture(url));
        }

        protected virtual void CellClickHandler(bool on)
        {
            if (on) inventoryController.CellClicked(this);
        }

        private void SetupClickHandler()
        {
            selection.onValueChanged.AddListener(CellClickHandler);
        }

        IEnumerator GetTexture(string url)
        {
            UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
            yield return www.SendWebRequest();

            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
            image.CrossFadeAlpha(1f, 0.5f, true);
            SizeToParent(image);
        }

        public Vector2 SizeToParent(RawImage image, float padding = 0)
        {
            float w = 0, h = 0;
            var parent = image.GetComponentInParent<RectTransform>();
            var imageTransform = image.GetComponent<RectTransform>();

            // check if there is something to do
            if (image.texture != null)
            {
                if (!parent) { return imageTransform.sizeDelta; } //if we don't have a parent, just return our current width;
                padding = 1 - padding;
                float ratio = image.texture.width / (float)image.texture.height;
                var bounds = new Rect(0, 0, parent.rect.width, parent.rect.height);
                if (Mathf.RoundToInt(imageTransform.eulerAngles.z) % 180 == 90)
                {
                    //Invert the bounds if the image is rotated
                    bounds.size = new Vector2(bounds.height, bounds.width);
                }
                //Size by height first
                h = bounds.height * padding;
                w = h * ratio;
                if (w > bounds.width * padding)
                { //If it doesn't fit, fallback to width;
                    w = bounds.width * padding;
                    h = w / ratio;
                }
            }
            imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
            imageTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
            return imageTransform.sizeDelta;
        }

    }
}