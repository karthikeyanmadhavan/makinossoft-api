using System;

namespace Common
{
    public class ApiException : Exception
    {
        public int Code { get; private set; }

        public ApiException(int code) : base()
        {
            this.Code = code;
        }
    }
}
