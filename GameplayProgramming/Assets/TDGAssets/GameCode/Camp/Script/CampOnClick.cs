using UnityEngine;
using System.Collections;

public class CampOnClick : MonoBehaviour 
{
	public ICamp theCamp = null;

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	public void OnClick()
	{
        Debug.Log("Click");
		TowerDefenseGame.Instance.ShowCampInfo( theCamp );
	}
}
