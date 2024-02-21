using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Models
{
    public interface IValueRange<T>
    {
        T Start { get; set; }

        T End { get; set; }
    }
}
