using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;


public class EnterProduct : MonoBehaviour
{
    [SerializeField] private TMP_InputField _barcodeInputField;
    [SerializeField] private TMP_InputField _productNameInputField;
    [SerializeField] private TMP_InputField _productPriceInputField;

    public void SaveProduct()
    {
        if (String.IsNullOrEmpty(_barcodeInputField.text) || String.IsNullOrEmpty(_productNameInputField.text) ||
            String.IsNullOrEmpty(_productPriceInputField.text))
        {
            UIController.Instance.DisplayErrorMessage("One of the fields is empty!");
            
            return;
        }

        GameController.Instance.SaveToJson(_barcodeInputField.text, _productNameInputField.text, _productPriceInputField.text);
    }
    
    public void SetBarcodeNumber(string barcodeNumber)
    {
        _barcodeInputField.text = barcodeNumber;
    }

    public void ResetEnteredProduct()
    {
        _barcodeInputField.text = String.Empty;
        _productNameInputField.text = String.Empty;
        _productPriceInputField.text = String.Empty;
    }
}