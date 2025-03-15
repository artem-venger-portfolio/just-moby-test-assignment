namespace BlockTower.Building
{
    public interface ITower
    {
        bool CanAdd(BlockBase block);
        void Add(BlockBase block);
        void Remove(BlockBase block);
        BlockBase GetLastBlock();
        bool IsEmpty();
    }
}