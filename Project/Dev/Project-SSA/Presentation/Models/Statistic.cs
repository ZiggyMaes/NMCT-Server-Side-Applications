using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class Statistic
    {
        public string Title { get; set; }
        public string Value { get; set; }

        public Statistic(string title, string value)
        {
            Title = title;
            Value = value;
        }
    }
}
