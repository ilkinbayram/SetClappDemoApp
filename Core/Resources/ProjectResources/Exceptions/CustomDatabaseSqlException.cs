namespace Core.Resources.ProjectResources.Exceptions
{
    public class CustomDatabaseSqlException : Exception
    {
        private readonly string _message;

        public CustomDatabaseSqlException()
        {
            _message = " appsettings.json : \"SqlServerName\" and \"SqlDatabaseName\" are not settled.";
        }

        public CustomDatabaseSqlException(int type)
        {
            if (type == 1)
                _message = " appsettings.json : \"SqlServerName\" and \"SqlDatabaseName\" are not settled.";
        }

        public override string Message => _message;
    }
}
