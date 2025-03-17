using R3;

namespace BlockTower
{
    public interface ITower
    {
        Observable<TowerBlockBase> BlockAdded { get; }
        bool IsEmpty();
        TowerBlockBase GetLastBlock();
        void Add(TowerBlockBase block);
        void Remove(TowerBlockBase block);

        TowerBlockBase this[int i] { get; }
    }
}