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
}
