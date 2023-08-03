using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ModelBase : MonoBehaviour
{
    [SerializeField] protected ViewBase _viewBase;
    [SerializeField] protected Device _device;
    protected Dictionary<DataType, float> _data;

    public Dictionary<DataType, float> Data
    {
        get
        {
            return _data;
        }
    }

    [HideInInspector] public UnityEvent<Dictionary<DataType, float>> OnDataUpdate;

    protected virtual void Awake()
    {
        OnDataUpdate.AddListener(_viewBase.DisplayCurrentData);
    }

    protected virtual void Start()
    {
        _data = new Dictionary<DataType, float>();
        _data.Add(DataType.Resistance, 0);
        _data.Add(DataType.Voltage, 0);
        _data.Add(DataType.ACVoltage, 0);
        _data.Add(DataType.Power , 0);
        _data.Add(DataType.Amperage, 0);

        UpdateData(_device.Data);
    }

    public virtual void UpdateData(Dictionary<DataType, float> data)
    {
        foreach (var item in _device.Data)
        {
            _data[item.Key] = item.Value;
        }
    }

    public virtual void UpdateData(DataType dataType, float value)
    {
        foreach (var item in _data)
        {
            if (item.Key == dataType)
            {
                _data[item.Key] = value;
                break;
            }
        }

        OnDataUpdate.Invoke(_data);
    }
}
