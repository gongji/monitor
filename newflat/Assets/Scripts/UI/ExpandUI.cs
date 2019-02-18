using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ExpandUI : MonoBehaviour {

    public Sprite sprite;

    private Sprite defaultSprite;
	void Start () {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        defaultSprite = GetComponent<Image>().sprite;
    }
	
	// Update is called once per frame
	

   private bool isExpand = false;

    private float xOffersetValue = 250.0f;
    public void ButtonClick()
    {
       
        if (isExpand)
        {
            transform.parent.GetComponent<RectTransform>().DOAnchorPosX(transform.parent.GetComponent<RectTransform>().anchoredPosition.x + xOffersetValue, 1.0f).OnComplete(()=> {

               // GetComponent<Image>().sprite = defaultSprite;
            }); 
        }
        else
        {
            transform.parent.GetComponent<RectTransform>().DOAnchorPosX(transform.parent.GetComponent<RectTransform>().anchoredPosition.x - xOffersetValue, 1.0f).OnComplete(()=>{
                //GetComponent<Image>().sprite = sprite;

            });
        }
       // transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
        isExpand = !isExpand;
    }

}
