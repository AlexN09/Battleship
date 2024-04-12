using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
   
    public void ChangeScene(int sceneID) 
    {
        SceneManager.LoadScene(sceneID);
        ManualPlacement.isPlacementDone = false;
    }
    public void Exit()
    {
        Application.Quit();
    }

}
