using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GamePauseUI : IUserInterface 
{
	private Text m_EnemyKilledCountText = null;
	private Text m_SoldierKilledCountText = null;
	private Text m_StageLvCountText = null;
	

	public GamePauseUI( TowerDefenseGame TDGame ):base(TDGame)
	{
		Initialize();
	}
	
	public override void Initialize()
	{
		m_RootUI = UITool.FindUIGameObject( "GamePauseUI" );

		m_EnemyKilledCountText 	= UITool.GetUIComponent<Text>(m_RootUI,"EnemyKilledCountText");
		m_SoldierKilledCountText = UITool.GetUIComponent<Text>(m_RootUI,"SoldierKilledCountText");
		m_StageLvCountText 		= UITool.GetUIComponent<Text>(m_RootUI,"StageLvCountText");

		// Continue
		Button btn  = UITool.GetUIComponent<Button>(m_RootUI, "ContinueBtn");
		btn.onClick.AddListener( ()=> OnContinueBtnClick() );

		// Continue
		btn  = UITool.GetUIComponent<Button>(m_RootUI, "ExitBtn");
		btn.onClick.AddListener( ()=> OnExitBtnClick() );

		Hide ();
	}

	public override void Hide ()
	{
		Time.timeScale = 1;
		base.Hide ();
	}

	public override void Show ()
	{
		Time.timeScale = 0;
		base.Show ();
	}
	
	public void ShowGamePause(  AchievementSaveData SaveData )
	{
		m_EnemyKilledCountText.text = string.Format("EnemyKilledCount:{0}", SaveData.EnemyKilledCount);
		m_SoldierKilledCountText.text = string.Format("SoldierKilledCount:{0}", SaveData.SoldierKilledCount);
		m_StageLvCountText.text = string.Format("StageLvCount:{0}", SaveData.StageLv); 		
		Show();
	}

	// Continue
	private void OnContinueBtnClick()
	{
		Hide();
	}

	// Exit
	private void OnExitBtnClick()
	{
		Hide ();
		m_TDGame.ChangeToMainMenu ();
	}

}
