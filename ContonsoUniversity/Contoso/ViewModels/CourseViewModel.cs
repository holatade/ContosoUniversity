using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.ViewModels
{
    public class CourseViewModel
    {
        public virtual int CourseCode { get; set; }
        public virtual string Title { get; set; }
        public virtual int Credits { get; set; }
    }
}
