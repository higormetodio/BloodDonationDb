using BloodDonationDb.Comunication.Responses;
using BloodDonationDb.Domain.Repositories.User;
using BloodDonationDb.Domain.Security.Tokens;
using BloodDonationDb.Exceptions;
using BloodDonationDb.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace BloodDonationDb.API.Filter;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserReadOnlyRepository userReadOnlyRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);

            var userIdentifier = _accessTokenValidator.ValidateAndGetUserIdentifier(token);

            var exist = await _userReadOnlyRepository.ExistsActiveUserWithIdentifier(userIdentifier);

            if (!exist)
            {
                throw new UnreachableException(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("Token expired"));
        }
        catch (BloodDonationDbException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(ResourceMessageException.USER_WITHOUT_PERMISSION_ACCESS_RESOURCE));
        }
    }

    private static string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
        {
            throw new UnreachableException(ResourceMessageException.NO_TOKEN);
        }

        return authentication["Bearer".Length..].ToString().Trim();
    }
}
