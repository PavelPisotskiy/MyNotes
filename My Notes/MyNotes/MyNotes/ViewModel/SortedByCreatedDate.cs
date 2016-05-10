using MyNotes.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.ViewModel
{
    class SortedByCreatedDate : IComparer
    {
        public int Compare(object x, object y)
        {
            return -1 * DateTime.Compare(((Note)x).CreatedDate, ((Note)y).CreatedDate);
        }
    }
}
