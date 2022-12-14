using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UI_NetworkManager_Menu : MonoBehaviour
{
    [SerializeField] private GameObject networkMenu;
    private bool menuActive = false;
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;

    void Awake()
    {
        serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
    public void ToggleMenu()
    {
        menuActive = !menuActive;
        networkMenu.SetActive(menuActive);
    }
}
