using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Request.Feedback
{
    public class FeedbackRequest
    {

        public int Id { get; set; }
        public string FeedbackContent { get; set; } = string.Empty;
        public int? FeedbackStars { get; set; }

    }
}
