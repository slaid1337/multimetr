using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputControllerBase : MonoBehaviour
{
    [SerializeField] protected InputData _inputData;

    protected virtual void Update()
    {
        SetData();
    }

    public abstract void SetData();
}
