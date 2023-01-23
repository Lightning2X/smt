using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Server_Destroy : MonoBehaviour
{
    private bool playGuide = false;

    private void GuideStart()
    {
        StartCoroutine(PlayLoop()); 
    }

    IEnumerator PlayLoop()
    {
        while(true){
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(5);  // delay between loops
    }
}

    private void OnTriggerEnter(Collider other) 
    {
        if (other.tag == "Bullet") 
        {
            Debug.Log("Boom, now door should be ready to open");
            Destroy(gameObject);
        }
    }
}
