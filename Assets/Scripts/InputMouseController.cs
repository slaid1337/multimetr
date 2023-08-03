using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMouseController : InputControllerBase
{
    public override void SetData()
    {
        _inputData.Scroll = Input.GetAxis("Mouse ScrollWheel");
    }
}
