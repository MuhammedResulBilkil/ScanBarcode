using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;

public class ScanBarcode : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private EnterProduct _enterProduct;

    private IBarcodeReader _barCodeReader = new BarcodeReader();
    private WebCamTexture _webCamTexture;
    private Texture2D _snap; 
    
    
    private string _qrCode = String.Empty;

    // Start is called before the first frame update
    void Start()
    {
        _webCamTexture = new WebCamTexture(512, 512);
        _rawImage.texture = _webCamTexture;
        
        _webCamTexture.Play();
    }

    public void ReadBarcode()
    {
        _snap = new Texture2D(_webCamTexture.width, _webCamTexture.height, TextureFormat.ARGB32, false);
        _snap.SetPixels32(_webCamTexture.GetPixels32());
        _snap.Apply();
        Result result = _barCodeReader.Decode(_snap.GetRawTextureData(), _webCamTexture.width, _webCamTexture.height,
            RGBLuminanceSource.BitmapFormat.ARGB32);
                
        if (result != null)
        {
            _qrCode = result.Text;
            if (!string.IsNullOrEmpty(_qrCode) && _qrCode.Length == 13)
            {
                Debug.Log("DECODED TEXT FROM QR: " + _qrCode);
                        
                _enterProduct.SetBarcodeNumber(_qrCode);
                        
                _qrCode = String.Empty;
            }
        }
    }

    public void StartWebcam()
    {
        _webCamTexture.Play();
    }

    public void StopWebcam()
    {
        _webCamTexture.Stop();
    }
}