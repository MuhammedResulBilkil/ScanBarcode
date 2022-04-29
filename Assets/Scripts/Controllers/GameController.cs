using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public GameObject productListPrefab;
    
    private Dictionary<string, ScannedProduct> _scannedProducts = new Dictionary<string, ScannedProduct>();
    
    private string _jsonData;
    private string _readJson;
    
    private void Awake()
    {
        Instance = this;
        
        RetrieveAllProducts();
    }

    public void RetrieveAllProducts()
    {
        _readJson = String.Empty;
        _readJson = File.ReadAllText(Application.dataPath + "/Products.json");

        if (!String.IsNullOrEmpty(_readJson))
        {
            _scannedProducts = JsonConvert.DeserializeObject<Dictionary<string, ScannedProduct>>(_readJson);
            
            Debug.Log("Successfully read from JSON!");
        }
    }

    public void SaveToJson(string barcode, string pName, string pPrice)
    {
        if (_scannedProducts.TryGetValue(barcode, out ScannedProduct sProduct))
        {
            UIController.Instance.DisplayErrorMessage($"Product: {pName} is already in the list! Updating the product!");
                
            sProduct.productName = pName;
            sProduct.productPrice = pPrice;
               
            _jsonData = JsonConvert.SerializeObject(_scannedProducts, Formatting.Indented);

            Debug.Log("JSON Data: " + _jsonData);

            _readJson = JsonConvert.DeserializeObject(_jsonData)?.ToString();

            Debug.Log("Read JSON Data: " + _readJson);
                
#if UNITY_EDITOR
            File.WriteAllText(Application.dataPath + "/Products.json",_jsonData);
#else
            File.WriteAllText(Application.persistentDataPath + "/Products.json",jsonData);
#endif

            return;
        }

        ScannedProduct scannedProduct = new ScannedProduct
        {
            productBarcode = barcode,
            productName = pName,
            productPrice = pPrice
        };

        _scannedProducts.Add(barcode, scannedProduct);

        _jsonData = JsonConvert.SerializeObject(_scannedProducts, Formatting.Indented);
        
        Debug.Log("JSON Data: " + _jsonData);

        _readJson = JsonConvert.DeserializeObject(_jsonData)?.ToString();
        //_readJson = JsonConvert.DeserializeObject<List<ScannedProduct>>(_jsonData)?.ToString();
        
        Debug.Log("Read JSON Data: " + _readJson);

#if UNITY_EDITOR
        File.WriteAllText(Application.dataPath + "/Products.json",_jsonData);
#else
        File.WriteAllText(Application.persistentDataPath + "/Products.json",jsonData);
#endif

        UIController.Instance.DisplaySuccessMessage("Saved!");
    }
    
}