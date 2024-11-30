using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Half_Checkmate.Pages
{
    public class IndexModel : PageModel
    {
        // 'ILogger<T>' is an interface for logging events.
        private readonly ILogger<IndexModel> _logger;        

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

    }
}