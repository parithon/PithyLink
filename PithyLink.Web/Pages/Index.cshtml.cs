using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PithyLink.Web.Attributes;

namespace PithyLink.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory httpClientFactory;

        public const string UrlRedirectNotSupported = "The URL provided is not supported. The URL redirects to another website.";
        public const string UrlLoopbackNotSupported = "The URL provided cannot point back to our service.";

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        [Required]
        [IsValidUrl]
        [BindProperty]
        [Display(Name = "Url")]
        public string ShortenUrl { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await Task.Delay(0);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var client = this.httpClientFactory.CreateClient();

            var response = await client.GetAsync(ShortenUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.Redirect ||
                response.StatusCode == System.Net.HttpStatusCode.PermanentRedirect)
            {
                ModelState.AddModelError(nameof(ShortenUrl), UrlRedirectNotSupported);
            }

            if (response.RequestMessage.RequestUri.DnsSafeHost == HttpContext.Request.Host.Host)
            {
                ModelState.AddModelError(nameof(ShortenUrl), UrlLoopbackNotSupported);
            }

            if (!ModelState.IsValid)
                return Page();

            await Task.Delay(0);
            return RedirectToPage("/Index");
        }
    }
}
