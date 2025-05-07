using TMPro;
using UnityEngine;
using UnityEngine.Analytics;

public class GUIControl : MonoBehaviour
{
    private TextMeshProUGUI _countTextReference;
    private static readonly string _defaultCountText = "Total: ";
    private int _countValue = 0;
    void Start()
    {
        _countTextReference = GameObject.Find("CountText").GetComponent<TextMeshProUGUI>();        
    }

    public void CountTextUpdate() {
        _countTextReference.text = _defaultCountText + 0;
    }
    public void CountTextUpdate(int value) {     
        Debug.Log(_countValue + " " + value);   
        switch (_countValue) {
            case < 0:
                _countValue = 0;
                goto case 0;
            case 0:
                if (value < 0) {
                    _countValue = 0;
                } else {
                    _countValue += value;
                }
                break;
            case > 10:
                _countValue = 10;
                goto case 10;
            case 10:
                if (value > 0) {
                    _countValue = 10;
                } else {
                    _countValue += value;
                }
                break;
            default:
                _countValue += value;
                break;
        }

        _countTextReference.text = _defaultCountText + _countValue; 
    }
}
