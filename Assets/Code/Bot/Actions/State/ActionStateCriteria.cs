public interface ActionStateCriteria
{
    public ActionState ActionState();
    public int PriorityLevel();
    public bool PassesCriteria();
}

public interface IUpdatableStatePriority : ActionStateCriteria
{
    public void UpdatePriorityLevel();
}
