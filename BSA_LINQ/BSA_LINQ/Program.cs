using BSA_LINQ.Models.DTOmodels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSA_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqService sorting = new LinqService();
            DataProvider data = DataProvider.GetInstance();
            data.LoadData();
            var x = sorting.GetStructure(6);
            var g = sorting.GetStructure(8);
            var f = sorting.GetStructure(3);
            var d = sorting.GetStructure(4);
            var s = sorting.GetStructure(1);
            var z = sorting.GetStructure(2);
            //var x = sorting.GetListOfOlderUsers();
        }
    }
}
