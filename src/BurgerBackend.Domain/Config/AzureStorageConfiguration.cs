using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerBackend.Domain.Config
{
    public class AzureStorageConfiguration
    {
        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string ImageContainer { get; set; }
    }
}
