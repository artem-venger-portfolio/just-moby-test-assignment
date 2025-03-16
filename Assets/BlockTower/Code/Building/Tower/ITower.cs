namespace BlockTower.Building
{
    public interface ITower
    {
        int Count { get; }

        BlockBase this[int i] { get; }

        void AddCondition(ICondition condition);
        bool CanAdd(BlockBase block);
        void Add(BlockBase block);
        void Remove(BlockBase block);
        BlockBase GetLastBlock();
        bool IsEmpty();
    }
}