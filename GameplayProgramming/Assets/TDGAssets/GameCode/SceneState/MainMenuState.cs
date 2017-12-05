using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuState : ISceneState
{
	public MainMenuState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "MainMenuState";
	}

	public override void StateBegin()
	{
		Button tmpBtn = UITool.GetUIComponent<Button>("StartGameBtn");

		if(tmpBtn!=null)
			tmpBtn.onClick.AddListener( ()=> OnStartGameBtnClick(tmpBtn) );

        Button endBtn = UITool.GetUIComponent<Button>("EndGameBtn");

        if (endBtn != null)
            endBtn.onClick.AddListener(() => OnEndGameBtnClick(endBtn));
    }
			
	private void OnStartGameBtnClick(Button theButton)
	{
		//Debug.Log ("OnStartBtnClick:"+theButton.gameObject.name);
		m_Controller.SetState(new BattleState(m_Controller), "BattleScene" );
	}

    private void OnEndGameBtnClick(Button theButton)
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
