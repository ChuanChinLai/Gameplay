using UnityEngine;
using System.Collections;

public class WeaponAttr
{
	public int 		Atk 	{get; private set;}
	public float 	AtkRange{get; private set;}
	public string 	AttrName{get; private set;}

	public 	WeaponAttr(int AtkValue,float Range,string AttrName)
	{
		this.Atk = AtkValue;
		this.AtkRange = Range;
		this.AttrName = AttrName;
	}
}
