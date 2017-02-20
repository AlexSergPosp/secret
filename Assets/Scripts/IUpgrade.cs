using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum UpgradeState
{
    NotAvailable,
    Available,
    Buyed,
}

public enum UpgradeType
{
    Active,
    Passive,
    All
}

public class Upgrade
{
    public string id;
    public string name;
    public string desc;

    public Resources cost;
    public Resources addResources;
    public UpgradeType upgradeType;

    public ReactiveProperty<int> level = new IntReactiveProperty(0);
    public ReactiveProperty<UpgradeState> state = new ReactiveProperty<UpgradeState>(UpgradeState.Available);

    public virtual void Apply()
    {
        state.Value = UpgradeState.Buyed;
    }
}

public class UpgradeController
{
    public List<Upgrade> upgrades = new List<Upgrade>();
    public ConnectionCollector collector = new ConnectionCollector();
    
    public void Inicialization()
    {
        foreach (var upgrade in upgrades)
        {
            collector.add = upgrade.state.Where(val => val == UpgradeState.Buyed).Subscribe(val =>
            {
                if (upgrade.upgradeType == UpgradeType.Active)
                    AddResourse(GameController.inst.playerData.gold, upgrade.addResources);

                if (upgrade.upgradeType == UpgradeType.Active)
                    AddResourse(GameController.inst.playerData.gold, upgrade.addResources);
            });
            collector.add = upgrade.level.Subscribe(val =>
            {
                if (upgrade.upgradeType == UpgradeType.Active)
                    AddResourse(GameController.inst.playerData.gold, upgrade.addResources);

                if (upgrade.upgradeType == UpgradeType.Active)
                    AddResourse(GameController.inst.playerData.gold, upgrade.addResources);
            });
        }

    }

    public void AddResourse(Gold gold, Resources res)
    {
        gold.Apply(res.count.Value);
    }
}