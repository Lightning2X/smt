using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
    public GameObject startMenu, optionsMenu;

    public GameObject characterSelectionButtonFirst, optionsMenuButtonFirst, optionsMenuButtonClosed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {

        }
    }

    public void StartMenuSelection()
    {
        if(!startMenu.activeInHierarchy)
        {
            startMenu.SetActive(true);

            EventSystem.current.SetSelectedGameObject(null);

            EventSystem.current.SetSelectedGameObject(characterSelectionButtonFirst);

        }
        else
        {
            startMenu.SetActive(false);
            optionsMenu.SetActive(false);
        }
    }

    public void OpenOptions()
    {

    }
}
