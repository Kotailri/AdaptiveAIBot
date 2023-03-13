public interface ActionStateCriteria
{
    public ActionState ActionState();
    public int PriorityLevel();
    public bool PassesCriteria();
    public float StateStayTime();
}

public interface IUpdatableStatePriority : ActionStateCriteria
{
    public void UpdatePriorityLevel();
}
