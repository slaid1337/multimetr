using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    [SerializeField] protected ViewBase _viewBase;
    [SerializeField] protected ModelBase _modelBase;

    protected int _state;

    public virtual void NextState()
    {
        _state++;
    }

    public virtual void PreviousState()
    {
        _state--;
    }

    public abstract void UpdateModelData();
}
