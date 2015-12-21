namespace Plaid.Contracts
{
    public enum ResponseCode
    {
        /*
        200: Success
        201: MFA Required
        400: Bad Request
        401: Unauthorized
        402: Request Failed
        404: Cannot be Found
        50X: Server Error
        */
        
        ServerError,
        Success = 200,
        MFARequired = 201,
        BadRequest = 400,
        Unauthorized = 401,
        RequestFailed = 402,
        CannotBeFound = 404
    }
}