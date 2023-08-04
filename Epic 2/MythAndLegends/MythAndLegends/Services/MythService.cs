using MythAndLegends.Data;
using MythAndLegends.Data.Entity;
using MythAndLegends.Services.Interface;

namespace MythAndLegends.Services;

public class MythService : IMythService
{
    public void AddMyth(Myth myth)
    {
        if (string.IsNullOrEmpty(myth.StoryCode))
        {
            myth.StoryCode = CreateCode(myth.Name);
        }

        Storage.MythsAndLegends.Add(myth);
    }

    public Story? GetMythByCode(string code)
    {
        return Storage.MythsAndLegends.FirstOrDefault(x => x.StoryCode.Equals(code)&&x is Myth);
    }

    private string CreateCode(string name)
    {
        var code = $"{name.First()}{name.Last()}-{name.Length}";

        return code;
    }
}