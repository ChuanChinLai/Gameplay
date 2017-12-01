using UnityEngine;
using System.Collections.Generic;


public class IdleAIState : IAIState 
{
	bool	m_bSetAttackPosition = false;

	public IdleAIState()
	{}			


	public override void SetAttackPosition( Vector3 AttackPosition )		
	{
		m_bSetAttackPosition = true;
	}

	public override void Update( List<ICharacter> Targets )
	{
		if(Targets == null ||  Targets.Count==0)
		{
			if( m_bSetAttackPosition )
				m_CharacterAI.ChangeAIState( new MoveAIState());
			return ;
		}


		Vector3 NowPosition = m_CharacterAI.GetPosition();
		ICharacter theNearTarget = null;
		float MinDist = 999f;

		foreach(ICharacter Target in  Targets)
		{
			if( Target.IsKilled())
				continue;

			float dist = Vector3.Distance( NowPosition, Target.GetGameObject().transform.position);
			if( dist < MinDist)
			{
				MinDist = dist;
				theNearTarget = Target;
			}
		}

		if( theNearTarget==null)
			return;

		if( m_CharacterAI.TargetInAttackRange( theNearTarget ))
			m_CharacterAI.ChangeAIState( new AttackAIState( theNearTarget ));
		else
			m_CharacterAI.ChangeAIState( new ChaseAIState( theNearTarget ));
	}
}
