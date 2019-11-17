using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseCalendarCreator
{
    class CourseContent
    {
        public string Topic { get; set; }
        public string Period { get; set; }
        public string Preparation { get; set; }

        public CourseContent()
        {

        }

        public CourseContent (string topic, string period, string preparation)
        {
            topic = string.Empty;
            period = string.Empty;
            preparation = string.Empty;
        }
    }
}
