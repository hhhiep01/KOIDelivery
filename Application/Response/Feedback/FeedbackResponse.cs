using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response.Feedback
{
    public class FeedbackResponse
    {
        public string FeedbackContent { get; set; } = string.Empty;
        public int? FeedbackStars { get; set; }
    }
}
