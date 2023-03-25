using UnityEngine;

public interface ActionStateCriteria
{
    public ActionState ActionState();
    public int PriorityLevel();
    public bool PassesCriteria();
    public float StateStayTime();
    public Color GetStateColor();
}

public interface IUpdatableStatePriority : ActionStateCriteria
{
    public void UpdatePriorityLevel();
}
