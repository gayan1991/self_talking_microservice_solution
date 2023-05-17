using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace BoilerPlate.Service.DTO.Response
{
    public class ErrorMessageDto
    {
        public string FunctionName { get; private set; }
        public string Message { get; private set; }
        private readonly StackTrace _stackTrace = new StackTrace();

        public ErrorMessageDto(string message)
        {
            Message = message;
        }
    }

    [Serializable]
    public class ErrorDTO : ISerializable
    {
        protected int Status { get; private set; }
        protected string Title { get; private set; }
        IEnumerable<ErrorMessageDto> ErrorMessages { get; set; }

        public ErrorDTO(int status, string title)
        {
            Status = status;
            Title = title;
            ErrorMessages = Enumerable.Empty<ErrorMessageDto>();
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("status", Status);
            info.AddValue("title", Title);
            info.AddValue("errors", ErrorModelhelper.Serialize(ErrorMessages));
        }
    }

    public static class ErrorModelhelper
    {
        public static Dictionary<string, List<string>> Serialize(IEnumerable<ErrorMessageDto> errors)
        {
            var errordic = new Dictionary<string, List<string>>();

            foreach (var error in errors)
            {
                var key = error.FunctionName;

                if (errordic.ContainsKey(key))
                {
                    errordic[error.FunctionName].Add(error.Message);
                }
                else
                {
                    errordic.Add(key, new List<string> { error.Message });
                }
            }

            return errordic;
        }
    }
}
