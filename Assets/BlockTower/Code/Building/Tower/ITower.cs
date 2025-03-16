namespace BlockTower.Building
{
    public interface ITower
    {
        void AddCondition(ICondition condition);
        bool CanAdd(BlockBase block);
        void Add(BlockBase block);
        void Remove(BlockBase block);
        BlockBase GetLastBlock();
        bool IsEmpty();

        BlockBase this[int i] { get; }
    }
}