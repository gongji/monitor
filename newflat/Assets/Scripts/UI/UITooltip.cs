using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UITooltip : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    private GameObject Text = null;
    public void Init(string name)
    {
        Text = TransformControlUtility.CreateItem("Text", transform);
        Text.GetComponent<RectTransform>().pivot = new Vector2(0f, 1.0f);
        Text.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 30);
        Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = name;
        Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().fontSize = 10;
        Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().alignment = TMPro.TextAlignmentOptions.MidlineLeft;
        Text.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = Color.black;
        Text.SetActive(false);
        Text.transform.SetSiblingIndex(0);
    }
    private bool isPointerEnter = false;
    private float time = 0;
    private Vector2 screenPos = Vector2.zero;
    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerEnter = true;
        screenPos = eventData.position;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerEnter = false;
        time = 0;
        Text.SetActive(false);
    }
    void Update()
    {
        if (isPointerEnter)
        {
            time += Time.deltaTime;
        }
    
        if (time > 0.5f)
        {
            Vector2 vec;
            Text.SetActive(true);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenPos, null, out vec);
            Text.transform.localPosition = new Vector2(vec.x, vec.y+20);
            isPointerEnter = false;
            time = 0;

        }
    }
}
