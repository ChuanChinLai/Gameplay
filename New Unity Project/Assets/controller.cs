using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public GameObject UI;

    Dictionary<KeyValuePair<int, int>, int> map;

	// Use this for initialization
	void Start ()
    {
        map = new Dictionary<KeyValuePair<int, int>, int>();

        map.Add(new KeyValuePair<int, int>(1, 2), 3);
        map.Add(new KeyValuePair<int, int>(4, 5), 9);
    }
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(map[new KeyValuePair<int, int>(4, 5)]);


        if (Input.GetMouseButtonUp(0) == false)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Hit");

            OnClick CampClickScript = hit.transform.gameObject.GetComponent<OnClick>();

            if (CampClickScript != null)
            {
                Debug.Log("Hit2");
                CampClickScript.Click(UI);
                return;
            }
        }
    }
}
