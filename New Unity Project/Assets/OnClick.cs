using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnClick : MonoBehaviour
{
    Color def;
	// Use this for initialization
	void Start ()
    {
        def = gameObject.GetComponent<Renderer>().material.color;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Click(GameObject UI)
    {
        Dropdown dp = UI.GetComponent<Dropdown>();

        switch(dp.value)
        {
            case 0:
                gameObject.GetComponent<Renderer>().material.color = Color.green;
                return;
            case 1:
                gameObject.GetComponent<Renderer>().material.color = Color.red;
                return;
            default:
                gameObject.GetComponent<Renderer>().material.color = def;
                return;
        }


//        Debug.Log(dp.options[dp.value].text);
    }

}
