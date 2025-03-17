namespace BlockTower
{
    public interface ITowerBuilder
    {
        bool IsPlacementAnimationActive();
        void CompletePlacementAnimation();
        void Start();
        void Stop();
    }
}