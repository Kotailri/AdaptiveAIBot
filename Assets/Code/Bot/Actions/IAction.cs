using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction {}

public interface IActionHasActionCheck : IAction
{
    /// <summary>
    /// Returns true if the action can be executed
    /// </summary>
    /// <returns></returns>
    public bool CheckAction();
}

public interface IActionHasInitialAction : IActionRequiredState
{
    /// <summary>
    /// Executes an action for when the state actions start
    /// </summary>
    public void ExecuteInitialAction();
}

public interface IActionHasUpdateAction : IAction
{
    /// <summary>
    /// Execute the action
    /// </summary>
    public void ExecuteAction();
}

public interface IActionHasActionChance : IAction
{
    /// <summary>
    /// Return the chance of the action being executed
    /// </summary>
    /// <returns></returns>
    public float GetActionChance();
}

public interface IActionRequiredState : IAction
{
    /// <summary>
    /// Returns the action state associated with the action
    /// </summary>
    /// <returns></returns>
    public List<ActionState> GetActionStates();
}

public interface IActionHasCleanup : IAction
{
    /// <summary>
    /// Cleans up for when action state actions end
    /// </summary>
    public void Cleanup();
}

public interface IActionExcludeState : IAction
{
    public List<ActionState> GetExcludedActionStates();
}