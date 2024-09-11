using Application.Queries.CertificationQueries;
using Application.Queries.TrainingQueries;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class CertificateViewModel : PageModel
    {
        private readonly IMediator _mediator;

        public CertificateViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Domain.Models.Certificate Certificate { get; set; }
        public Training Training { get; private set; }
        public byte[] BarcodeImage { get; set; }
        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            GetByIdCertificateQuery Command = new GetByIdCertificateQuery(id);
            Certificate = await _mediator.Send(Command);
            if(Certificate == null)
            {
                return NotFound();
            }
            long tid = Certificate.TrainingId ?? 0;
            GetByIdTrainingQuery xCommand = new GetByIdTrainingQuery(tid);
            Training = await _mediator.Send(xCommand);


            Zen.Barcode.CodeQrBarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
            string userinfo = "";
            try
            {
               var CertificateLink = "https://miycnportal.com/certificate/" + Certificate.CerificateNumber;
                System.Drawing.Image img = barcode.Draw(CertificateLink, 100);

                BarcodeImage = AppServices.TurnImageToByteArray(img);
            }
            catch (Exception c)
            {

            }
            return Page();
        }
    }
}
