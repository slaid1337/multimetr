using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;
using System;

public class MultimetrStandartController : ControllerBase, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private InputData _inputData;
    [SerializeField] private Color _activeColor;
    [SerializeField] private Color _calmColor;
    [SerializeField] private GameObject _switcher;
    [SerializeField] private GameObject _switcherContainer;
    [SerializeField] private TextMeshPro _screenText;

    private bool _isActive = false;
    private bool _isSwitchable = false;

    private Vector3 _rotation;

    private void Awake()
    {
        _rotation = _switcherContainer.transform.eulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _switcher.GetComponent<Renderer>().material.color = _activeColor;
        _isSwitchable = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _switcher.GetComponent<Renderer>().material.color = _calmColor;
        _isSwitchable = false;
        StopAllCoroutines();
    }

    public override void UpdateModelData()
    {
        float R = _modelBase.Data[DataType.Resistance];
        float P = _modelBase.Data[DataType.Power];
        switch (_state)
        {
            case 0:
                _modelBase.UpdateData(DataType.Resistance, _modelBase.Data[DataType.Resistance]);
                UpdateScreen(_modelBase.Data[DataType.Resistance].ToString());
                break;
            case 3:
                float V = (float) Math.Round(Mathf.Sqrt(P * R), 2);
                _modelBase.UpdateData(DataType.Voltage, V);
                UpdateScreen(V.ToString());
                break;
            case 2:
                float AC = 0.01f;
                _modelBase.UpdateData(DataType.ACVoltage, AC);
                UpdateScreen(AC.ToString());
                break;
            case 1:
                float A = (float)Math.Round(Mathf.Sqrt(P / R), 2);
                _modelBase.UpdateData(DataType.Amperage, A);
                UpdateScreen(A.ToString());
                break;
        }
    }

    private void Update()
    {
        if (_inputData.Scroll > 0  && _isSwitchable)
        {
            NextState();
        }
        else if (_inputData.Scroll < 0  && _isSwitchable)
        {
            PreviousState();
        }
    }

    public override void NextState()
    {
        base.NextState();

        if (_state > 3)
            {
                _state = 0;
            }
            else if (_state < 0)
            {
                _state = 3;
            }

        if (!_isActive)
        {
            _rotation.z -= 45f;
            _state = 0;
        }
        else _rotation.z -= 90f;
        
        
        _isActive = true;

        UpdateModelData();
        _switcherContainer.transform.DORotate(_rotation, 0.3f);
        StartCoroutine(RefreshSwitchable());
    }

    public override void PreviousState()
    {
        base.PreviousState();

        if (_state > 3)
        {
            _state = 0;
        }
        else if (_state < 0)
        {
            _state = 3;
        }

        if (!_isActive) _rotation.z += 45f;
        else _rotation.z += 90f;
        
        _isActive = true;

        UpdateModelData();
        _switcherContainer.transform.DORotate(_rotation, 0.3f);
        StartCoroutine(RefreshSwitchable());
    }

    private IEnumerator RefreshSwitchable()
    {
        _isSwitchable = false;
        yield return new WaitForSeconds(0.3f);

        _isSwitchable = true;
    }

    public void UpdateScreen(string text)
    {
        _screenText.text = text;
    }
}
