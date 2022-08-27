using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.SharedModels
{
    public class RequestModel
    {
        [Required]
        public string Method { get; set; } = string.Empty;
    }
    public class RequestModel<T> : RequestModel where T : class, new()
    {
        public T Params { get; set; } = new();
    }
    public class RequestModelPage : RequestModelsBase
    {
        public int? Page_index { get; set; }
        public int? Page_size { get; set; }
    }
    public class RequestModelsBase
    {
    }
}
