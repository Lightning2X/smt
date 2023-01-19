using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light _light;
    public int pressNumber;
    public GameObject Obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        MeshRenderer m = Obj.GetComponent<MeshRenderer>();

        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            if (pressNumber >= 3)
                pressNumber = 0;
            pressNumber += 1;
            if (pressNumber == 1)
            {
                m.enabled = true;
                On();
            }
            else if (pressNumber == 2)
                Scale();
            else if (pressNumber == 3)
            { 
                Off();
                m.enabled = false;
            }

            Debug.Log(pressNumber);
        }
    }
    public void On()
    {
        _light.enabled = true;
    }
    public void Scale()
    {
        _light.type = LightType.Directional;
        _light.intensity = 2;
    }
    public void Off()
    {
        _light.enabled = false;
        _light.type = LightType.Spot;
        _light.intensity = 4;
    }
}
