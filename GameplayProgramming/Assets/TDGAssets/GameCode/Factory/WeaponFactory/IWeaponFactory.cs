using UnityEngine;
using System.Collections;

public abstract class IWeaponFactory
{
	public abstract IWeapon CreateWeapon( ENUM_Weapon emWeapon);
}

