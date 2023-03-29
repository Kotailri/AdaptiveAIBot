# Adaptive AI Opponent

A 2D top-down tank-like player vs player game made in Unity C#. The player can utilize the environment, stat bonuses, consumable items, and shooting projectiles to defeat their opponent. The opponent in this game is a computer-controlled opponent that adapts to how the the human-controlled player plays, in order to keep them in the flow channel.

The bot adapts by utilizing 5 different playstyles and a difficulty level against the player. The playstyles are aggression, counterattack, item collection, item usage strategy, and player position strategy.

Play the game here!
https://kotailri.itch.io/adaptiveaibot

Controls:

Movement - WASD
Light Shoot - Left Click
Heavy Shoot - Right Click
Poison - Q
Burst Shield - E
Main Menu - Esc

Modes:

Full - Full access to sliders with playstyle and difficulty levels.
Demo - Normal gameplay mode

Compare test mode 0 to test modes 1-5, 0 has default playstyle levels and 1-5 each have a different playstyle level at the max level. Try to tell the difference!

Compare test mode D0 and D1, Try to tell the difference in difficulty!

Debug Commands: (LCTRL on Full mode)
\- fillItems : Gives 64 consumables to both players
\- difficulty [-] [number] : Sets the difficulty
\- kill [bot/player] : Kills a player
\- debug [true/false] : Toggles debug mode