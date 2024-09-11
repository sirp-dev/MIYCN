using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.Dashboard.Pages.Admin
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class DataDto
    {
        public string DataColor { get; set; }
        public int DataCount { get; set; }
        public string DataTitle { get; set; }
    }



    public class StateModel : PageModel
    {
        public List<DataDto> DataListDto { get; set; }

        public void OnGet()
        {
            DataListDto = new List<DataDto>
    {
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Abia" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Adamawa" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "AkwaIbom" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Anambra" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Bauchi" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Bayelsa" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Benue" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Borno" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Cross River" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Delta" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Ebonyi" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Edo" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Ekiti" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Enugu" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "FCT" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Gombe" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Imo" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Jigawa" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Kaduna" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Kano" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Katsina" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Kebbi" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Kogi" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Kwara" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Lagos" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Nasarawa" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Niger" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Ogun" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Ondo" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Osun" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Oyo" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Plateau" },
        new DataDto { DataColor = "bg-success", DataCount = 0, DataTitle = "Rivers" },
        new DataDto { DataColor = "bg-warning", DataCount = 0, DataTitle = "Sokoto" },
        new DataDto { DataColor = "bg-info", DataCount = 0, DataTitle = "Taraba" },
        new DataDto { DataColor = "bg-danger", DataCount = 0, DataTitle = "Yobe" },
        new DataDto { DataColor = "bg-primary", DataCount = 0, DataTitle = "Zamfara" }

            };

        }
    }
}
