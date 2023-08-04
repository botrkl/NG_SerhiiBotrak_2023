using MythAndLegends.Data.Entity;

namespace MythAndLegends.Services.Interface
{
    internal interface ILegendService
    {
        public void AddLegend(Legend legend);
        public Story? GetLegendByCode(string code);
    }
}
