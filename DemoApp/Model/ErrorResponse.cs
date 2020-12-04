using System;
using System.Collections.Generic;

namespace DemoApp.Model
{
    internal class ErrorResponse
    {
        public ErrorResponse()
        {
            RequestId = Guid.NewGuid().ToString();

            Errors = new List<ErrorModel>();
        }

        /// <summary>
        /// Date time of error occurred ISO format
        /// </summary>
        /// <example>2019-11-07T15:07:00.000+000</example>
        public DateTime Timestamp => DateTime.Now;

        /// <summary>
        /// Request Id to track issue 
        /// </summary>
        /// <example>2153e140-264e-4cd6-b1e1-9e01786102a2</example>
        public string RequestId { get; set; }

        /// <summary>
        /// List of error occurred 
        /// </summary>
        public List<ErrorModel> Errors { get; set; }

        public override string ToString()
        {
            return $"[{Timestamp}]-[{RequestId}] : {string.Join(",\r\n ", Errors)}";
        }
    }
}