using UnityEngine;
using System.Collections;

public class TrainCost : ITrainCost
{
	public TrainCost(){}

	public override int GetTrainCost( ENUM_Soldier emSoldier,int CampLv, ENUM_Weapon emWeapon)
	{
		int Cost = 0;

		switch( emSoldier )
		{
		case ENUM_Soldier.Rookie:
			Cost = 5;
			break;
		case ENUM_Soldier.Sergeant:
			Cost = 7;
			break;
		case ENUM_Soldier.Captain:
			Cost = 10;
			break;
		default:
			break;
		}

		switch( emWeapon) 
		{
		case ENUM_Weapon.Gun:
			Cost += 5;
			break;
		case ENUM_Weapon.Rifle:
			Cost += 7;
			break;
		case ENUM_Weapon.Rocket:
			Cost += 10;
			break;			
		}

		Cost += (CampLv -1 ) * 2;

		return Cost;
	}

}
