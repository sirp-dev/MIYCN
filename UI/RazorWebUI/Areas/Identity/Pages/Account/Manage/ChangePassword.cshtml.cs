// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using Application.Commands.IdentityCommand;
using Application.Queries.IdentityQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace RazorWebUI.Areas.Identity.Pages.Account.Manage
{
    [Microsoft.AspNetCore.Authorization.Authorize]

    public class ChangePasswordModel : PageModel
    {
        private readonly IMediator _mediator;

        public ChangePasswordModel(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var currentUser = User.Identity.Name;

            GetUserByNameQuery usercommand = new GetUserByNameQuery(currentUser);
            var userdata = await _mediator.Send(usercommand);

            if (userdata != null)
            {
                ChangePasswordCommand passcommand = new ChangePasswordCommand(userdata.Id, Input.OldPassword, Input.NewPassword);
                var result = await _mediator.Send(passcommand);
                if (result)
                {
                    StatusMessage = "Your password has been changed.";
                    if (User.Identity.IsAuthenticated && User.IsInRole("Admin") || User.IsInRole("mSuperAdmin"))
                    {
                        return RedirectToPage("");

                    }
                    else if (User.Identity.IsAuthenticated && User.IsInRole("Participant") || User.IsInRole("Facilitator"))
                    {

                        return RedirectToPage("");
                    }
                    else
                    {
                        return RedirectToPage();
                    }

                }

            }
            else
            {
                StatusMessage = "Invalid";
                return RedirectToPage();
            }


            StatusMessage = "Invalid";
            return RedirectToPage();
        }
    }
}
