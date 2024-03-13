

namespace CreditCardValidator.Data.Support
{
    public class Constants
    {
        #region " SP Names "
        public const string SP_GET_CARD_VALIDATION = "GetCardValidation";
        public const string SP_LOG_SAVE = "LogSave";
        #endregion

        #region " SP Param Names "
        public const string PARAM_CARDID = "@CardId";
        public const string PARAM_REQUEST_ID = "@RequestId";
        public const string PARAM_STEP = "@Step";
        public const string PARAM_DATA = "@Data";
        public const string PARAM_REQUEST_DATA = "@RequestData";
        public const string PARAM_EXCEPTION_DATA = "@ExceptionData";
        public const string PARAM_RESPONSE_DATA = "@ResponseData";
        #endregion
    }
}
