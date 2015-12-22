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
        
        Success = 200,
        MFARequired = 201,
        BadRequest = 400,
        Unauthorized = 401,
        RequestFailed = 402,
        CannotBeFound = 404,
        
        InternalServerError = 500,
        NotImplemented = 501,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        HttpVersionNotSupported = 505
    }
}