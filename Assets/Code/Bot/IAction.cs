using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAction
{
    /// <summary>
    /// Returns true if the action can be executed
    /// </summary>
    /// <returns></returns>
    public bool CheckAction();

    /// <summary>
    /// Execute the action
    /// </summary>
    public void ExecuteAction();
    
    /// <summary>
    /// Return the chance of the action being executed
    /// </summary>
    /// <returns></returns>
    public float GetActionChance();

    /// <summary>
    /// Returns the action state associated with the action
    /// </summary>
    /// <returns></returns>
    public ActionState GetActionState();

    /// <summary>
    /// Cleans up for when action state is changed
    /// </summary>
    public void Cleanup();
}
