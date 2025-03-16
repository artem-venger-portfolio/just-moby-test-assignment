namespace BlockTower
{
    public interface ITower
    {
        bool IsEmpty();
        TowerBlockBase GetLastBlock();
        void Add(TowerBlockBase block);
        void Remove(TowerBlockBase block);

        TowerBlockBase this[int i] { get; }
    }
}