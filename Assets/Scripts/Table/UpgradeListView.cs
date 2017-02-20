using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeListView : PopupWindow
{
    private GameController controller;
    

    void Awake()
    {
        controller = GameController.inst;

        foreach (var data in controller.playerData.upgrades)
        {
            var prefab = Instantiate(UnityEngine.Resources.Load("Prefabs/UpgradeView")) as GameObject;
            var view = prefab.GetComponent<UpgradeView>();

            view.name.text = data.name;
            view.lvl.text = data.level.Value.ToString();
            view.info.onClick.AddListener(() =>
            {
                //PopupController infoVindow
            });

        }
    }
}
