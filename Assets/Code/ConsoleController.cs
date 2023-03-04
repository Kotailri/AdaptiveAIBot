using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using System.Reflection;

public class ConsoleController : MonoBehaviour
{
    public Canvas consoleCanvas;
    public TextMeshProUGUI consoleText;
    private bool isConsoleOpen = false;
    private string inputString = "";
    private List<string> commandHistory = new List<string>();
    private int historyIndex = -1;

    private PlayerTracker tracker;

    void Start()
    {
        consoleCanvas.enabled = false;
        consoleText.text = "";
        tracker = Global.playertracker;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isConsoleOpen = !isConsoleOpen;
            consoleCanvas.enabled = isConsoleOpen;
            if (isConsoleOpen)
            {
                inputString = "";
                consoleText.text = "";
                historyIndex = -1;
                consoleText.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                consoleText.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (isConsoleOpen)
        {
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                consoleText.text = consoleText.text.Remove(consoleText.text.Length-1);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isConsoleOpen = false;
                consoleCanvas.enabled = false;
                consoleText.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (commandHistory.Count > 0 && historyIndex < commandHistory.Count - 1)
                {
                    historyIndex++;
                    inputString = commandHistory[historyIndex];
                    consoleText.text = inputString;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (commandHistory.Count > 0 && historyIndex >= 0)
                {
                    historyIndex--;
                    if (historyIndex == -1)
                    {
                        inputString = "";
                    }
                    else
                    {
                        inputString = commandHistory[historyIndex];
                    }
                    consoleText.text = inputString;
                }
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                string command = inputString.Trim();
                if (!string.IsNullOrEmpty(command))
                {
                    commandHistory.Insert(0, command);
                    historyIndex = -1;
                    inputString = "";
                    consoleText.text = inputString;
                    RunCommand(command);
                }
            }
            else if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Backspace))
            {
                consoleText.text += Input.inputString;
                inputString += Input.inputString;
            }
        }
    }

    private void RunCommand(string command)
    {
        // Add code to run the command here
        string[] tokens = command.Split(' ');

        switch (tokens[0])
        {
            case "fillItems":
                FillItems(tokens);
                break;

            case "state":
                State(tokens);
                break;

            case "difficulty":
                Difficulty(tokens);
                break;

            case "kill":
                Kill(tokens);
                break;

            default:
                Utility.PrintCol("Unknown command: " + tokens[0], "FF0000");
                break;
        }
    }

    #region Command Functions
    private void FillItems(string[] tokens)
    {
        tracker.Player.GetComponent<Inventory>().AddItems(ItemName.BurstConsumable, 64);
        tracker.Player.GetComponent<Inventory>().AddItems(ItemName.PoisonConsumable, 64);

        tracker.Bot.GetComponent<Inventory>().AddItems(ItemName.BurstConsumable, 64);
        tracker.Bot.GetComponent<Inventory>().AddItems(ItemName.PoisonConsumable, 64);
        Utility.PrintCol("Filled items", "00FF00");
    }

    private void State(string[] tokens)
    {
        if (tokens.Length > 1)
        {
            ActionManager am = tracker.Bot.GetComponent<ActionManager>();
            switch (tokens[1])
            {
                case "wander":
                    am.ChangeStates(ActionState.Wander);
                    Utility.PrintCol("Updated state to " + tokens[1], "00FF00");
                    break;

                case "attack":
                    am.ChangeStates(ActionState.Attack);
                    Utility.PrintCol("Updated state to " + tokens[1], "00FF00");
                    break;

                case "flee":
                    am.ChangeStates(ActionState.Flee);
                    Utility.PrintCol("Updated state to " + tokens[1], "00FF00");
                    break;

                case "useItem":
                    am.ChangeStates(ActionState.UseItem);
                    Utility.PrintCol("Updated state to " + tokens[1], "00FF00");
                    break;

                case "collectItem":
                    am.ChangeStates(ActionState.CollectItem);
                    Utility.PrintCol("Updated state to " + tokens[1], "00FF00");
                    break;

                default:
                    Utility.PrintCol("State not found: " + tokens[1], "ff0000");
                    break;
            }

        }
        else
        {
            Utility.PrintCol("Current state: " + tracker.Bot.GetComponent<ActionManager>().currentState, "00FF00");
        }
    }

    private void Difficulty(string[] tokens)
    {
        if (tokens.Length > 1)
        {
            Global.difficultyLevel = (float)Convert.ToDouble(tokens[1]);
            Utility.PrintCol("Updated difficulty to " + tokens[1], "00FF00");
        }
        else
        {
            Utility.PrintCol("Current difficulty: " + Global.difficultyLevel, "00FF00");
        }
    }

    private void Kill(string[] tokens)
    {
        if (tokens.Length > 1)
        {
            if (tokens[1] == "bot")
            {
                tracker.Bot.GetComponent<Health>().Kill();
                Utility.PrintCol("Killed " + tokens[1], "00FF00");
            }
            else if (tokens[1] == "player")
            {
                tracker.Player.GetComponent<Health>().Kill();
                Utility.PrintCol("Killed " + tokens[1], "00FF00");
            }
            else
            {
                Utility.PrintCol("Target not found: " + tokens[1], "FF0000");
            }
        }
        else
        {
            Utility.PrintCol("No target specified", "FF0000");
        }
    }

    #endregion
}
