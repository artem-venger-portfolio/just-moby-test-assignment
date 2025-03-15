namespace BlockTower.Building
{
    public interface ICondition
    {
        bool Check(BlockBase block);
    }
}