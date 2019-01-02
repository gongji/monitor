using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPointTitleAdaption : MonoBehaviour {

    public Transform grid;

    Vector2 size =  Vector2.zero;
	void Update () {
        size = grid.GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(grid.GetComponent<RectTransform>().anchoredPosition.x- size.x, 
            grid.GetComponent<RectTransform>().anchoredPosition.y+size.y);

    }
}
