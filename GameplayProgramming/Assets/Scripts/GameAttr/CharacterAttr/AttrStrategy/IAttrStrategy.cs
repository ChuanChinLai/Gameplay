using UnityEngine;
using System.Collections;


public abstract class IAttrStrategy
{
	
	public abstract void InitAttr( ICharacterAttr CharacterAttr );
	
	
	public abstract int GetAtkPlusValue( ICharacterAttr CharacterAttr );
	
	
	public abstract int GetDmgDescValue( ICharacterAttr CharacterAttr );
}
