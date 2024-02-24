using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : Menu
{
    
  

    public void QuitGame()
    {
        // This would be linked to your "Quit" button
        // Here you would typically load the main menu or exit the application
        Time.timeScale = 1; // Don't forget to reset the time scale before leaving the scene

        // For quitting the game or going back to the main menu:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Only works in the Unity Editor
#else
        Application.Quit(); // Use this when building the game
        // Or for going back to the main menu:
        // SceneManager.LoadScene("MainMenuSceneName");
#endif
    }
}
