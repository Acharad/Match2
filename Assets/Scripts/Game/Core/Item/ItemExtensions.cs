namespace Game.Core.Item
{
    public static class ItemExtensions
    {
        public static MatchType ToMatchType(this ItemColor color)
        {
            return color switch
            {
                ItemColor.Green  => MatchType.Green,
                ItemColor.Yellow => MatchType.Yellow,
                ItemColor.Blue   => MatchType.Blue,
                ItemColor.Red    => MatchType.Red,
                ItemColor.Pink   => MatchType.Pink,
                ItemColor.Purple => MatchType.Purple,
                _ => MatchType.None
            };
        }
    }
}