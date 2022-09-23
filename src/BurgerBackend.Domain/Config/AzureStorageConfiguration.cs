namespace BurgerBackend.Domain.Config
{
    public class AzureStorageConfiguration
    {
        public string StorageUri { get; set; }

        public string AccountName { get; set; }

        public string AccountKey { get; set; }

        public string ImageContainerName { get; set; }
    }
}
