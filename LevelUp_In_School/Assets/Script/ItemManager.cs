using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    
    public PurchaseCheak purchaseCheck;
    
    public int price;
    public Sprite buttonImg;

    public string item_name;
    public string item_tag;//���� �̸�


    public void ItemButton()
    {
        purchaseCheck.gameObject.SetActive(true);
        purchaseCheck.itemPrice.text = price.ToString()+"����Ʈ";
        purchaseCheck.itemImg.sprite = buttonImg;
        purchaseCheck.itemName.text = item_name;
        purchaseCheck.price = price;
        //purchaseCheck.buyItem = this.gameObject;
        purchaseCheck.itemTag = item_tag;

    }
     
}
