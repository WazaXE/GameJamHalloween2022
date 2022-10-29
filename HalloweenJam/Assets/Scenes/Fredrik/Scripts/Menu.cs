using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{

    public GameObject pauseMenu;
    public GameObject upgradesMenu;

    public GameObject upgradesFirstButton;
    public GameObject upgradesClosedButton;


    public void OpenUpgrades()
    {
        pauseMenu.SetActive(false);
        upgradesMenu.SetActive(true);

        //Clear selected object

        EventSystem.current.SetSelectedGameObject(null);

        //Set new selected object

        EventSystem.current.SetSelectedGameObject(upgradesFirstButton);
    }

    public void CloseUpgrades()
    {
        upgradesMenu.SetActive(false);
        pauseMenu.SetActive(true);

        //Clear selected object

        EventSystem.current.SetSelectedGameObject(null);

        //Set new selected object

        EventSystem.current.SetSelectedGameObject(upgradesClosedButton);
    }



}
