using System.ComponentModel.DataAnnotations;

namespace Api.BusinessModels.SharedModels
{
    public class ModelBase
    {
        public string Result { get; set; } = "fail";
        public string? Message { get; set; } = null;
    }
    public class ResponseModel : ModelBase
    {
        public ResponseModel()
        {
        }
        public ResponseModel(string message)
        {
            this.Message = message;
        }
    }
}
