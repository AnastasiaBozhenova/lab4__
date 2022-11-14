using System.Collections.Generic;

namespace Customs.WebAPI.Models.StorageWorkloads
{
    public class TotalInfoAboutProduct
    {
        public string StorageName { get; set; }

        public int TotalQuantityOfProducts { get; set; }

        public List<string> ProductsNamesWithCustomNumber { get; set; }

        public double TotalDuty { get; set; }
    }
}