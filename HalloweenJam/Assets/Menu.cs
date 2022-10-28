using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject upgradesMenu;

    public GameObject pauseFirstButton;
    public GameObject upgradesFirstButton;
    public GameObject upgradesClosedButton;

    public void PauseUnpause()
    {
        if (!pauseMenu.activeInHierarchy)
        {

            //Clear selected object

            EventSystem.current.SetSelectedGameObject(null);

            //Set new selected object

            EventSystem.current.SetSelectedGameObject(pauseFirstButton);

        }
    }

    public void OpenOptions()
    {
        upgradesMenu.SetActive(true);

        //Clear selected object

        EventSystem.current.SetSelectedGameObject(null);

        //Set new selected object

        EventSystem.current.SetSelectedGameObject(upgradesFirstButton);
    }

    public void CloseOptions()
    {
        upgradesMenu.SetActive(false);

        //Clear selected object

        EventSystem.current.SetSelectedGameObject(null);

        //Set new selected object

        EventSystem.current.SetSelectedGameObject(upgradesFirstButton);
    }



}
