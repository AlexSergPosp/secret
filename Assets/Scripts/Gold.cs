using UniRx;
using UnityEngine;

public interface IReward
{
    void Apply(long count);
}

public interface IConsume
{
    bool IsCanConsume(long count);
    bool TryConsume(long count);
}

[System.Serializable]
public class Resources : IReward, IConsume
{
    [SerializeField] public ReactiveProperty<long> count;
    public void Apply(long count)
    {
        this.count.Value += count;
    }

    public bool IsCanConsume(long count)
    {
        return this.count.Value >= count;
    }

    public bool TryConsume(long count)
    {
        if (this.count.Value < count) return false;
        this.count.Value -= count;
        return true;
    }
}
[System.Serializable]
public class Gold : Resources
{
    public Gold(long count)
    {
        this.count = new LongReactiveProperty(count);
    }
}

[System.Serializable]
public class PasiiveGold : Resources
{
    public PasiiveGold(long count)
    {
        this.count = new LongReactiveProperty(count);
    }
}