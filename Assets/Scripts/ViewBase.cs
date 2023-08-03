using UnityEngine;
using System.Collections.Generic;

public abstract class ViewBase : MonoBehaviour
{
    public abstract void DisplayCurrentData(Dictionary<DataType, float> data);
}