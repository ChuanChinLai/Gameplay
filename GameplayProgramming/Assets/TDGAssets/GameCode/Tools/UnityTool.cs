using UnityEngine;
using System.Collections.Generic;
	
public static class UnityTool
{
	public static void Attach( GameObject ParentObj, GameObject ChildObj, Vector3 Pos )
	{
		ChildObj.transform.parent = ParentObj.transform;
		ChildObj.transform.localPosition = Pos;
	}

	public static void AttachToRefPos( GameObject ParentObj, GameObject ChildObj,string RefPointName,Vector3 Pos )
	{
		// Search 
		bool bFinded = false;
		Transform[] allChildren = ParentObj.transform.GetComponentsInChildren<Transform>();
		Transform RefTransform = null;
		foreach (Transform child in allChildren)
		{            
			if (child.name == RefPointName)
			{                
				if (bFinded == true)
				{
					continue;
				}
				bFinded = true;
				RefTransform = child;
			}
		}
		
		if (bFinded == false)
		{
			Attach( ParentObj,ChildObj,Pos);
			return ;
		}

		ChildObj.transform.parent = RefTransform;
		ChildObj.transform.localPosition = Pos;
		ChildObj.transform.localScale = Vector3.one;
		ChildObj.transform.localRotation = Quaternion.Euler( Vector3.zero);				
	}
	

	public static GameObject FindGameObject(string GameObjectName)
	{

		GameObject pTmpGameObj = GameObject.Find(GameObjectName);

		if(pTmpGameObj==null)
		{
			return null;
		}
		return pTmpGameObj;
	}

	public static GameObject FindChildGameObject(GameObject Container, string gameobjectName)
	{
		if (Container == null)
		{
			Debug.LogError("NGUICustomTools.GetChild : Container =null");
			return null;
		}
		
		Transform pGameObjectTF=null; //= Container.transform.FindChild(gameobjectName);											
			
		if(Container.name == gameobjectName)			
			pGameObjectTF = Container.transform;
		else
		{					
			Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
			foreach (Transform child in allChildren)			
			{            
				if (child.name == gameobjectName)
				{
					if(pGameObjectTF==null)					
						pGameObjectTF=child;
					else
						Debug.LogWarning("ERROR");
				}
			}
		}
		
		if(pGameObjectTF==null)			
		{	
			return null;
		}
		
		return pGameObjectTF.gameObject;			
	}
}
