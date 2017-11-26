using UnityEngine;
using System.Collections;

public abstract class ITrainCost 
{
	public abstract int GetTrainCost( ENUM_Soldier emSoldier,int CampLv, ENUM_Weapon emWeapon);
}
