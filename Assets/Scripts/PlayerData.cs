using System;
using UniRx;

[System.Serializable]
public class PlayerData {

    public Gold gold;
    public PasiiveGold passiveGold;

    public ReactiveCollection<Upgrade> upgrades;

    public void Inicialization()
    {
        gold = new Gold(0);
        passiveGold = new PasiiveGold(0);
    }
}
