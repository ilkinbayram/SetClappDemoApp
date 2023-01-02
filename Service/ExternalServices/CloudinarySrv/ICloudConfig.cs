namespace Service.ExternalServices.CloudinarySrv
{
    public interface ICloudConfig
    {
        string CLOUDINARYAPIKEY { get; set; }
        string CLOUDINARYCLOUDNAME { get; set; }
        string CLOUDINARYAPISECRET { get; set; }
    }
}
