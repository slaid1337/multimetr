using System;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    public Dictionary<DataType, float> Data;
    [SerializeField] private DataStruct[] _dataStruct; 

    private void Awake()
    {
        Data = new Dictionary<DataType, float>();

        foreach (var data in _dataStruct)
        {
            Data.Add(data.dataType, data.value);
        }
    }
}

[Serializable]
public struct DataStruct
{
    public DataType dataType;
    public int value;
}