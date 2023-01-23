using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Sound_riddle : NetworkBehaviour
{
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
        StartCoroutine(PlayLoop()); 
    }

    IEnumerator PlayLoop()
    {
        while(true)
        {
            GetComponent<AudioSource>().Play();;
            yield return new WaitForSeconds(15);  // delay between loops
        }
    }

    // Update is called once per frame, answer is 3
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha4))
        {/*spawn enemies, suspend riddle*/}
        if (Input.GetKey(KeyCode.Alpha3))
        {
            Destroy(gameObject);
            //victory sound
        } 
    }
}
