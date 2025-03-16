namespace BlockTower
{
    public interface IBuildCondition
    {
        bool CanBuild(BuildConditionData data);
    }
}