using Application.Queries.TrainingQueries;
using Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Models;
using Application.Queries.CertificationQueries;


namespace RazorWebUI.Areas.ITrainings.Pages.Admin
{
    public class TrainingCertificateModel : PageModel
    {
        private readonly IMediator _mediator;

        public TrainingCertificateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public List<Domain.Models.Certificate> Certificate { get; set; }
        public Training Training { get; private set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            ListCertificationByTrainingQuery Command = new ListCertificationByTrainingQuery(id);
            Certificate = await _mediator.Send(Command);

            GetByIdTrainingQuery xCommand = new GetByIdTrainingQuery(id);
            Training = await _mediator.Send(xCommand);
            return Page();
        }
    }
}
