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

            var z = sorting.GetStructure(2);
            //var x = sorting.GetListOfOlderUsers();
        }
    }
}
