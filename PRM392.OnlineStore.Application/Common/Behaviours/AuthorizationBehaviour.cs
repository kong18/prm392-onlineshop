
using MediatR;
using Microsoft.AspNetCore.Authorization;
using PRM392.OnlineStore.Application.Exceptions;
using PRM392.OnlineStore.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PRM392.OnlineStore.Application.Behaviours
{
    public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly ICurrentUserService _currentUserService;

        public AuthorizationBehaviour(
            ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                // Must be authenticated user
                if (_currentUserService.UserId == null)
                {
                    throw new UnauthorizedAccessException();
                }

                // Role-based authorization
                var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));

                if (authorizeAttributesWithRoles.Any())
                {
                    foreach (var roles in authorizeAttributesWithRoles.Select(a => a.Roles.Split(',')))
                    {
                        var authorized = false;
                        foreach (var role in roles)
                        {
                            var isInRole = await _currentUserService.IsInRoleAsync(role.Trim());
                            if (isInRole)
                            {
                                authorized = true;
                                break;
                            }
                        }

                        // Must be a member of at least one role in roles
                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }

                // Policy-based authorization
                var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
                if (authorizeAttributesWithPolicies.Any())
                {
                    foreach (var policy in authorizeAttributesWithPolicies.Select(a => a.Policy))
                    {
                        var authorized = await _currentUserService.AuthorizeAsync(policy);

                        if (!authorized)
                        {
                            throw new ForbiddenAccessException();
                        }
                    }
                }
            }

            // User is authorized / authorization not required
            return await next();
        }
    }
}
