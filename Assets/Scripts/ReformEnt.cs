using UnityEngine;

namespace ReformEnt
{
    public class Utilities
    {
        public class UI
        {
            public static TextMesh CreateWorldText(string gameObjectName, string textToDisplay, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 12, Color color = default(Color), TextAnchor textAnchor = default(TextAnchor), TextAlignment textAlignment = default(TextAlignment), int sortingOrder = 0)
            {
                if (color == default(Color))
                    color = Color.white;
                return CreateWorldText(parent, gameObjectName, textToDisplay, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
            }
            public static TextMesh CreateWorldText(Transform parent, string gameObjectName, string textToDisplay, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
            {
                GameObject newText = new GameObject(gameObjectName, typeof(TextMesh));
                Transform newTextTransform = newText.transform;
                newTextTransform.SetParent(parent, false);
                newTextTransform.localPosition = localPosition;
                TextMesh newTextMesh = newText.GetComponent<TextMesh>();
                newTextMesh.anchor = textAnchor;
                newTextMesh.alignment = textAlignment;
                newTextMesh.text = textToDisplay;
                newTextMesh.fontSize = fontSize;
                newTextMesh.color = color;
                newTextMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
                return newTextMesh;
            }
        }
       
    }
   
}
