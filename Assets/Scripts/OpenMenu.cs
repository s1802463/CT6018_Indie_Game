using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenMenu : MonoBehaviour
{
    //public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject mainMenu;
    public GameObject storeMenu;
    public GameObject doubleCheckMenu;
    public GameObject quitReturnMenu;
    public GameObject optionsReturnMenu;
    public GameObject storeReturnMenu;

    public string doubleCheckButtonName;

    public static bool gamePaused = false;

    private void Update()
    {
        //if (gamePaused)
        //{
        //    Resume();
        //}
        //else
        //{
        //    GamePause();
        //}
    }

    void Resume()
    {
        //if (pauseMenu != null)
        //{
        //    pauseMenu.SetActive(false);
        //}
        //Time.timeScale = 1f;
        //gamePaused = false;
    }

    //public void GamePause()
    //{
    //    if (pauseMenu == null)
    //    {
    //        pauseMenu.SetActive(true);
    //    }
    //    Time.timeScale = 0f;
    //    gamePaused = true;
    //}

    public void OptionsMenu()
    {
        optionsReturnMenu = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        optionsReturnMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void StoreMenu()
    {
        storeReturnMenu = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        storeReturnMenu.SetActive(false);
        storeMenu.SetActive(true);
    }

    public void MainMenu()
    {
        if (mainMenu == null)
        {
            mainMenu.SetActive(true);
        }
        optionsMenu.SetActive(false);
    }

    public void Quit()
    {
        quitReturnMenu = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        quitReturnMenu.SetActive(false);
        doubleCheckMenu.SetActive(true);
    }

    public void Back()
    {
        GameObject.Find("Canvas").GetComponent<OpenMenu>().optionsReturnMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void BackStore()
    {
        GameObject.Find("Canvas").GetComponent<OpenMenu>().storeReturnMenu.SetActive(true);
        storeMenu.SetActive(false);
    }

    public void QuitCheck()
    {
        doubleCheckButtonName = EventSystem.current.currentSelectedGameObject.name;
        Debug.Log(doubleCheckButtonName);

        if (doubleCheckButtonName == "YesBtn")
        {
            Application.Quit();
        }
        else
        {
            doubleCheckMenu.SetActive(false);
            GameObject.Find("Canvas").GetComponent<OpenMenu>().quitReturnMenu.SetActive(true);
        }

    }

}
