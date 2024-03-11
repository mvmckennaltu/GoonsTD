using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseText;
    public GameObject mainMenuButton;
    public GameObject exitButton;
  
    public static bool gameIsPaused = false;
    
    // Start is called before the first frame update
  
   
    
    private void OnPause()
    {
        
        
            if (gameIsPaused == false)
            {
                PauseGame();
            }
            else
            {
                gameIsPaused = false;
                UnpauseGame();
            }
        
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseText.SetActive(true); mainMenuButton.SetActive(true);  exitButton.SetActive(true);
        gameIsPaused = true;
        AudioListener.pause = true;
    
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1.0f;
        pauseText.SetActive(false); mainMenuButton.SetActive(false); exitButton.SetActive(false); 
        gameIsPaused = false;
        AudioListener.pause = false;
        
    }
    public void Update()
    {
       
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
        
    }
}
