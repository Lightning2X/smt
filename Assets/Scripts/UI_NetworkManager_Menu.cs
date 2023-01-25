using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Unity.Netcode.Transports.UTP;

public class UI_NetworkManager_Menu : MonoBehaviour
{
    [SerializeField] private GameObject networkMenu;
    private bool menuActive = false;
    //[SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private TextMeshProUGUI ipAddressText;
    [SerializeField] private TextMeshProUGUI joinIPAddressText;
    string ipAddress;

    public string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ipAddressText.text = ip.ToString();
                ipAddress = ip.ToString();
                return ip.ToString();
            }
        }
        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    void Awake()
    {
        //serverButton.onClick.AddListener(() => { NetworkManager.Singleton.StartServer(); });
        GetLocalIPAddress();
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
        hostButton.onClick.AddListener(() => { NetworkManager.Singleton.StartHost(); });
        //clientButton.onClick.AddListener(() => { NetworkManager.Singleton.StartClient(); });
    }
    public void joinHost()
    {
        if(joinIPAddressText.text != "")
        ipAddress = joinIPAddressText.text;
        Debug.Log(ipAddress);
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipAddress;
        NetworkManager.Singleton.StartClient();
    }
    public void ToggleMenu()
    {
        menuActive = !menuActive;
        networkMenu.SetActive(menuActive);
    }
}
