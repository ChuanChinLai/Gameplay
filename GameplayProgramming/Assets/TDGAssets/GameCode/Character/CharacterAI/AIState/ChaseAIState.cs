using UnityEngine;
using System.Collections.Generic;


public class ChaseAIState : IAIState 
{
	private ICharacter m_ChaseTarget = null;

	private const float CHASE_CHECK_DIST = 0.2f;
	private Vector3 m_ChasePosition = Vector3.zero;
	private bool m_bOnChase = false;

	public ChaseAIState(ICharacter ChaseTarget)
	{
		m_ChaseTarget = ChaseTarget;
	}			


	public override void Update( List<ICharacter> Targets )
	{

		if(m_ChaseTarget == null || m_ChaseTarget.IsKilled() )
		{
			m_CharacterAI.ChangeAIState( new IdleAIState());
			return ;
		}


		if( m_CharacterAI.TargetInAttackRange( m_ChaseTarget ))
		{
			m_CharacterAI.StopMove();
			m_CharacterAI.ChangeAIState( new AttackAIState(m_ChaseTarget)); 
			return ;
		}


		if( m_bOnChase)
		{

			float dist = Vector3.Distance( m_ChasePosition, m_CharacterAI.GetPosition());
			if( dist < CHASE_CHECK_DIST )
				m_CharacterAI.ChangeAIState( new IdleAIState()); 
			return ;
		}


		m_bOnChase = true;
		m_ChasePosition = m_ChaseTarget.GetPosition();
		m_CharacterAI.MoveTo( m_ChasePosition );
	}


	public override void RemoveTarget(ICharacter Target)
	{
		if( m_ChaseTarget.GetGameObject().name == Target.GetGameObject().name )
			m_ChaseTarget = null;
	}
}
