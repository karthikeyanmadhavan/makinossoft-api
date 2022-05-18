namespace JSONFileAPI.Common
{
    /// <summary>
    /// Api Response Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
        }
        public ApiResponse(T content)
        {
            Data = content;
        }

        public ApiResponse(T content, string errorMessage)
        {
            Data = content;
            Message = errorMessage;
        }

        public ApiResponse(T content, int messageCode)
        {
            Data = content;
            Code = messageCode;
        }

        /// <summary>
        /// gets or sets error codes
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Get or sets Message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// get or sets Result
        /// </summary>
        public T Data { get; set; }
    }

    public class ApiResponse : ApiResponse<object>
    {
    }
}


