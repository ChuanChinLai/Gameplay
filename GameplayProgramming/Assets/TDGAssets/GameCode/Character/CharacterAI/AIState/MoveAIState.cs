using UnityEngine;
using System.Collections.Generic;

public class MoveAIState : IAIState 
{
	private const float MOVE_CHECK_DIST = 1.5f;
	bool 	m_bOnMove = false;
	Vector3	m_AttackPosition = Vector3.zero;
	
	public MoveAIState()
	{}	

	public override void SetAttackPosition( Vector3 AttackPosition )
	{
		m_AttackPosition = AttackPosition;
	}

	public override void Update( List<ICharacter> Targets )
	{
		if(Targets != null &&  Targets.Count>0)
		{
			m_CharacterAI.ChangeAIState( new IdleAIState() );
			return ;
		}

		if( m_bOnMove)
		{
			float dist = Vector3.Distance( m_AttackPosition, m_CharacterAI.GetPosition());

			if( dist < MOVE_CHECK_DIST )
			{
				m_CharacterAI.ChangeAIState( new IdleAIState()); 

				if( m_CharacterAI.IsKilled()==false)
					m_CharacterAI.CanAttackHeart();

				m_CharacterAI.Killed();
			}
			return ;
		}

		m_bOnMove = true;
		m_CharacterAI.MoveTo( m_AttackPosition );
	}

}
