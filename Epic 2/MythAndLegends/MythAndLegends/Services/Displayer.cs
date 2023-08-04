using MythAndLegends.Services.Interface;

namespace MythAndLegends.Services;

public class Display : IDisplay
{
    private readonly ILegendService _legendService;
    private readonly IMythService _mythService;
    
    public Display()
    {
        _legendService = new LegendService();
        _mythService = new MythService();
    }

    public void DisplayByCode(string code)
    {
        var legend = _legendService.GetLegendByCode(code);
        var myth = _mythService.GetMythByCode(code);

        if (legend is not null)
        {
            legend.Tell();
        }
        else if (myth is not null)
        {
            myth.Tell();
        }
        else
        {
            Console.WriteLine($"No myth or legend with code {code}");
        }
    }

}