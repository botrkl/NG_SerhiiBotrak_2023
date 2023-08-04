using MythAndLegends.Data.Entity;

namespace MythAndLegends.Services.Interface
{
    internal interface IMythService
    {
        public void AddMyth(Myth myth);
        public Story? GetMythByCode(string code);

    }
}
