using UnityEngine;
using System.Collections.Generic;


public class SoldierAI : ICharacterAI 
{	
	public SoldierAI(ICharacter Character):base(Character)
	{
		ChangeAIState(new IdleAIState());
	}

	public override bool CanAttackHeart()
	{
		return false;
	}
}

