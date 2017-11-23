using UnityEngine;
using System.Collections;

public abstract class IAttrFactory
{

	public abstract SoldierAttr GetSoldierAttr( int AttrID );

	public abstract SoldierAttr GetEliteSoldierAttr(ENUM_AttrDecorator emType,int AttrID, SoldierAttr theSoldierAttr);
	    
	public abstract EnemyAttr GetEnemyAttr(int AttrID);

    public abstract WeaponAttr GetWeaponAttr(int AttrID);

	public abstract AdditionalAttr GetAdditionalAttr( int AttrID );
}
