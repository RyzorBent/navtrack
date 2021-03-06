﻿using System;
using System.Threading.Tasks;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Navtrack.Api.Services.CommandHandler;

namespace Navtrack.Api.Hubs
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    public class BaseHub : Hub
    {
        private readonly IServiceProvider serviceProvider;

        public BaseHub(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private protected async Task HandleRequest<T>(T input)
        {
            ICommandHandler<T> commandHandler =
                serviceProvider.GetRequiredService<ICommandHandler<T>>();
            
            await commandHandler.Authorize(input);

            if (!commandHandler.ApiResponse.IsValid || commandHandler.ApiResponse.HttpStatusCode.HasValue)
            {
                return;
            }

            await commandHandler.Validate(input);

            if (!commandHandler.ApiResponse.IsValid)
            {
                return;
            }

            await commandHandler.Handle(input);
        }

        private protected async Task<TResponse> HandleRequest<TSource, TResponse>(TSource input)
        {
            ICommandHandler<TSource, TResponse> commandHandler =
                serviceProvider.GetRequiredService<ICommandHandler<TSource, TResponse>>();

            await commandHandler.Authorize(input);

            if (!commandHandler.ApiResponse.IsValid)
            {
                return default;
            }
            if (commandHandler.ApiResponse.HttpStatusCode.HasValue)
            {
                return default;
            }

            await commandHandler.Validate(input);

            if (!commandHandler.ApiResponse.IsValid)
            {
                return default;
            }
            
            TResponse response = await commandHandler.Handle(input);

            return response ?? default;
        }
    }
}