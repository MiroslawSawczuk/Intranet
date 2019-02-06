namespace Intranet.Users.DependencyInjection
{
    public class SqlServerUsersOptions
    {
        public string ConnectionString { get; set; }
        public bool AutoRegister { get; set; }
        public string RepositoriesProjectName { get; set; }
    }
}
