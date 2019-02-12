

namespace ProjectTemplate_v2.Models.SensorTypes
{
    public interface IHasRangeValue
    {
        decimal MinValue { get; set; }
        decimal MaxValue { get; set; }
    }
}
