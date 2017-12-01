using UnityEngine;
using System.Collections.Generic;


public class AttackAIState : IAIState 
{

	private ICharacter m_AttackTarget = null;

	public AttackAIState( ICharacter AttackTarget )
	{
		m_AttackTarget = AttackTarget;
	}			


	public override void Update( List<ICharacter> Targets )
	{

		if(m_AttackTarget == null || m_AttackTarget.IsKilled() || Targets == null || Targets.Count==0 )
		{
			m_CharacterAI.ChangeAIState( new IdleAIState()); 
			return ;
		}


		if( m_CharacterAI.TargetInAttackRange( m_AttackTarget) ==false)
		{
			m_CharacterAI.ChangeAIState( new ChaseAIState(m_AttackTarget)); 
			return ;
		}


		m_CharacterAI.Attack( m_AttackTarget );
	}


	public override void RemoveTarget(ICharacter Target)
	{
		if( m_AttackTarget.GetGameObject().name == Target.GetGameObject().name )
			m_AttackTarget = null;
	}

}
