using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public float scrollSpeed = 50f;
    public float fadeStartY = -80f;
    public float fadeEndY = -120f;
    public Image FotoPibes;
    public float Wait;
    public Button Return;
    public TextMeshProUGUI JimmyGvng;
    void Update()
    {
        foreach (Transform child in transform)
        {
            RectTransform rt = child.GetComponent<RectTransform>();
            Graphic graphic = child.GetComponent<Graphic>(); // Para Text o TextMeshProUGUI

            if (rt == null || graphic == null)
                continue;

            // Mover hacia abajo
            rt.anchoredPosition += Vector2.down * scrollSpeed * Time.deltaTime;

            float y = rt.anchoredPosition.y;

            // Calcular opacidad individual
            float alpha = 1f;

            if (y >= fadeStartY)
            {
                alpha = 1f;
            }
            else if (y <= fadeEndY)
            {
                alpha = 0f;
            }
            else
            {
                float t = (fadeStartY - y) / (fadeStartY - fadeEndY);
                alpha = Mathf.Lerp(1f, 0f, t);
            }

            Color c = graphic.color;
            c.a = alpha;
            graphic.color = c;
        }
        Invoke(nameof (Foto), Wait);
    }

    private void Foto()
    {
        FotoPibes.gameObject.SetActive(true);
        JimmyGvng.gameObject.SetActive(true);
        Return.gameObject.SetActive(true);
    }
}
