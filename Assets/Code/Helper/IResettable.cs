/// <summary>
/// Interface for objects that reset every round.
/// </summary>
public interface IResettable
{
    /// <summary>
    /// Reset object
    /// </summary>
    void ResetObject();

    /// <summary>
    /// Add resettable object to resettable list, call on start.
    /// Global.resettables.Add(this);
    /// </summary>
    void InitResettable();

    /// <summary>
    /// Remove resettable object from resettable list, call on destroy.
    /// Global.resettables.Remove(this);
    /// </summary>
    void OnDestroyAction();
}

