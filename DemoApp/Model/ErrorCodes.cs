using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Model
{
    public enum ErrorCode
    {

        [Description("Error due to unknown reason")]
        [Display(Name = "UNKNOWN_REASON")]
        UnknownReason = 1000,

        [Description("Fail to retrieve data")]
        [Display(Name = "FAIL_TO_GET_DATA")]
        FailToGetData = 1001,

        [Description("Invalid parameter in request")]
        [Display(Name = "INVALID_REQUEST_PARAMETERS")]
        InvalidRequestParameters = 1003,

        [Description("Data not found")]
        [Display(Name = "DATA_NOT_FOUND")]
        DataNotFound = 1004,

        [Description("Record already exist")]
        [Display(Name = "RECORD_ALREADY_EXIST")]
        RecordAlreadyExist = 1006,
    }
}
