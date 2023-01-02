namespace Core.Resources.OperationResources.Model
{
    public class DataMessage
    {
        public DataMessage()
        {
            TextContent = string.Empty;
            Name = string.Empty;
            Token = string.Empty;
            IsSucceesful = false;
            StreamContent = new byte[0];
        }

        public string TextContent { get; set; }
        public byte[] StreamContent { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public bool IsSucceesful { get; set; }
    }
}
