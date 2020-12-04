using DemoApp.Extensions;

namespace DemoApp.Model
{
    internal class ErrorModel
    {
        /// <summary>
        /// Error code for each error
        /// </summary>
        /// <example>FailToGetData</example>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// Code for each error
        /// </summary>
        /// <example>1002</example>
        public int Code => (int)ErrorCode;

        /// <summary>
        /// User friendly error display
        /// </summary>
        /// <example>FAIL_TO_GET_DATA</example>
        public string Display { get; set; }

        /// <summary>
        /// developer friendly error message
        /// </summary>
        /// <example>Fail to retrieve data</example>
        public string Message { get; set; }


        public override string ToString()
        {
            return $"{Display}[{Code}] - {Message}";
        }

        public static ErrorModel FromErrorCode(ErrorCode error, string message = null)
        {
            return new ErrorModel
            {
                ErrorCode = error,
                Display = error.GetDisplayText(),
                Message = message ?? error.GetDescription()
            };
        }
    }
}