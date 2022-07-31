using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework11
{
    public interface IChangingRecords
    {
        string Name { get; }
        string Surname { get; } 
        string PhoneNumber { get; }
        string PassportNumber { get; }
        string PassportSeries { get; }
        DataOfRecord DataRecord { get; }
    }
}
