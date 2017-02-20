using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class VisualController : MonoBehaviour
{

    public GoldView gold;
    public PassiveGoldView passiveGold;
    private ConnectionCollector connection = new ConnectionCollector();
    private GameController gameController;

    public const string postfix = "/sec";
    void Start () {

        gameController = GameController.inst;
        gold.count.text = gameController.playerData.gold.count.Value.ToString();
        passiveGold.count.text = gameController.playerData.passiveGold.count.Value.ToString() + postfix;

        connection.add = gameController.playerData.gold.count.Subscribe(val =>
        {
            gold.count.text = val.ToString();
        });
        connection.add = gameController.playerData.passiveGold.count.Subscribe(val =>
        {
            passiveGold.count.text = val.ToString() + postfix; ;
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void CloseWindow(PopupWindow popupWindow)
    {
        throw new System.NotImplementedException();
    }
}
