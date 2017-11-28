using UnityEngine;
using System.Collections;

public class APSystem :  IGameSystem
{
	public const int	MAX_AP = 100;
	private const float AP_COOLD_DOWN = 3f;
	private const int   AP_RESTORE = 10;

	private int			m_NowAP	  = MAX_AP;
	private float 		m_ApTimer = AP_COOLD_DOWN;

	public APSystem(TowerDefenseGame TDGame):base(TDGame)
	{
		Initialize();
	}

	public override void Update ()
	{
		base.Update ();

		m_ApTimer -= Time.deltaTime;

		if( m_ApTimer > 0)
			return;

		m_ApTimer = AP_COOLD_DOWN;

		m_NowAP += AP_RESTORE;
		m_NowAP = Mathf.Min (m_NowAP , MAX_AP);

		m_TDGame.APChange( m_NowAP );
	}

	public bool CostAP(int ApValue)
	{
		if( (m_NowAP - ApValue) < 0 )
			return false;

		m_NowAP -= ApValue;

		m_TDGame.APChange( m_NowAP );
		return true;
	}

}
