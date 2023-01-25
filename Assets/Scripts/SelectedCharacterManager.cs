using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectedCharacterManager : NetworkBehaviour
{
    public Character selectedCharacter = Character.Null;
    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        // Retrieve the name of t$$anonymous$$s scene.
        string sceneName = currentScene.name;
        DontDestroyOnLoad(gameObject);
        Debug.Log(sceneName);
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            if(selectedCharacter == Character.Sivion)
            {
                NetworkManager.Singleton.StartHost();
            }
            if(selectedCharacter == Character.Donus)
            {
                NetworkManager.Singleton.StartClient();
            }
        }

    }

    public void SetDonus()
    {
        selectedCharacter = Character.Donus;
    }
    public void SetSivion()
    {
        selectedCharacter = Character.Sivion;
    }
    public void SetNull()
    {
        selectedCharacter = Character.Null;
    }
}
