using Application.Queries.CertificationQueries;
using Application.Queries.TrainingQueries;
using Domain.DTOs;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorWebUI.Pages
{
    public class CertificateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CertificateModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        [BindProperty]
        public Certificate Certificate { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            GetCertificateByNumberQuery Command = new GetCertificateByNumberQuery(id);
            Certificate = await _mediator.Send(Command);
            return Page();
        }
    }
}
