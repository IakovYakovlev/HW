namespace HW_17.Services
{
    internal interface IDataRepostitory<T> where T : class
    {
        public string GetConnectionString();

        public string ConnectionsTest(string conStr);
    }
}
