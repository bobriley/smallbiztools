namespace SBToolsService.POCOs
{
    public class SmallBusinessReport
    {
        public SmallBusinessInfo? SmallBusinessInfo { get; set; }

        public float SDE { get; set; }
        public float HealthRatio { get; set; }
        public float SDEValuation { get; set; }
        public float PriceDelta { get; set; }
    }
}