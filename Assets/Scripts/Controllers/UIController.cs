using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private RectTransform _productListParent;

    [SerializeField] private TextMeshProUGUI successMessageText;
    [SerializeField] private TextMeshProUGUI errorMessageText;

    private ScanBarcode _scanBarcode;

    private int _successDOTweenID;
    private int _errorDOTweenID;

    private void Awake()
    {
        Instance = this;

        _scanBarcode = FindObjectOfType<ScanBarcode>();

        _successDOTweenID = Random.Range(0, 100000);
        _errorDOTweenID = Random.Range(0, 100000);
    }

    public void DisplaySuccessMessage(string message)
    {
        if (DOTween.IsTweening(_successDOTweenID))
            DOTween.Kill(_successDOTweenID);

        successMessageText.text = message;
        successMessageText.alpha = 1f;
        DOTween.To(() => successMessageText.alpha, x => successMessageText.alpha = x, 0f, 2f)
            .SetId(_successDOTweenID).SetEase(Ease.InOutSine).OnComplete(() => successMessageText.text = "")
            .OnKill(() =>
            {
                successMessageText.text = "";
                successMessageText.alpha = 0;
            });
    }

    public void DisplayErrorMessage(string message)
    {
        if (DOTween.IsTweening(_errorDOTweenID))
            DOTween.Kill(_errorDOTweenID);

        errorMessageText.text = message;
        errorMessageText.alpha = 1f;
        DOTween.To(() => errorMessageText.alpha, x => errorMessageText.alpha = x, 0f, 2f)
            .SetId(_errorDOTweenID).SetEase(Ease.InOutSine).OnComplete(() => errorMessageText.text = "")
            .OnKill(() =>
            {
                errorMessageText.text = "";
                errorMessageText.alpha = 0;
            });
    }
}