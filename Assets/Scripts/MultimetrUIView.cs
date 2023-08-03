using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MultimetrUIView : ViewBase
{
    [SerializeField] private TextMeshProUGUI _VoltageText;
    [SerializeField] private TextMeshProUGUI _AmperageText;
    [SerializeField] private TextMeshProUGUI _ACVoltageText;
    [SerializeField] private TextMeshProUGUI _ResistanceText;


    public override void DisplayCurrentData(Dictionary<DataType, float> data)
    {
        foreach (var item in data)
        {
            switch (item.Key)
            {
                case DataType.Resistance:
                    _ResistanceText.text = item.Value.ToString();
                    break;
                case DataType.Voltage:
                    _VoltageText.text = item.Value.ToString();
                    break;
                case DataType.Amperage:
                    _AmperageText.text = item.Value.ToString();
                    break;
                case DataType.ACVoltage:
                    _ACVoltageText.text = item.Value.ToString();
                    break;
            }
        }
    }

    
}
