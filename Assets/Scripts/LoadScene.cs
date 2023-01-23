using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoading = 79f;
    [SerializeField] private string sceneNameToLoad;

    private float timeElapsed;
    // Update is called once per frame
    private void Update()
    {
        timeElapsed += Time.deltaTime;
        if (timeElapsed > delayBeforeLoading) 
        {
            SceneManager.LoadScene(sceneNameToLoad);
        }
    }
}
