using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Product : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _barcodeText;
    [SerializeField] private TextMeshProUGUI _productNameText;
    [SerializeField] private TextMeshProUGUI _priceText;

    public void SetProductDetails(string barcode, string productName, string price)
    {
        _barcodeText.text = barcode;
        _productNameText.text = productName;
        _priceText.text = price;
    }
}