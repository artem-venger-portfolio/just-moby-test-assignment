namespace BlockTower
{
    public interface ILocalizer
    {
        Language Language { get; set; }
        string Localize(string key);
    }
}