using System.Collections.Generic;


public class BaseBehaviour : Singleton<BaseBehaviour>
{
    #region PrivateFields

    private List<IUpdateble> _updatebles = new List<IUpdateble>();
    private List<IFixedUpdateble> _fixedUpdatebles = new List<IFixedUpdateble>();
    private List<ILateUpdateble> _lateUpdatebles = new List<ILateUpdateble>();

    #endregion


    #region UnityMethods

    void Update()
    {
        foreach (var item in _updatebles) item.DoUpdate();
    }

    private void FixedUpdate()
    {
        foreach (var item in _fixedUpdatebles) item.DoFixedUpdate();
    }

    private void LateUpdate()
    {
        foreach (var item in _lateUpdatebles) item.DoLastUpdate();
    }

    #endregion


    #region Methods

    public void AddToUpdateble(IUpdateble item)
    {
        _updatebles.Add(item);
    }

    public void AddToFixedUpdateble(IFixedUpdateble item)
    {
        _fixedUpdatebles.Add(item);
    }

    public void AddToLateUpdateble(ILateUpdateble item)
    {
        _lateUpdatebles.Add(item);
    }

    public void RemoveFromUpdateble(IUpdateble item)
    {
        _updatebles.Remove(item);
    }

    public void RemoveFromFixedUpdateble(IFixedUpdateble item)
    {
        _fixedUpdatebles.Remove(item);
    }

    public void RemoveFromLateUpdateble(ILateUpdateble item)
    {
        _lateUpdatebles.Remove(item);
    }

    #endregion
}
