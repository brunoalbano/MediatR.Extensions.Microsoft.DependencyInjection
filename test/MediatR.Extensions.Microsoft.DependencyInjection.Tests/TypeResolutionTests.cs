﻿using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Extensions.Microsoft.DependencyInjection.Tests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Shouldly;
    using Xunit;

    public class TypeResolutionTests
    {
        private readonly IServiceProvider _provider;

        public TypeResolutionTests()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddMediatR(typeof(Ping));
            _provider = services.BuildServiceProvider();
        }

        [Fact]
        public void ShouldResolveMediator()
        {
            _provider.GetService<IMediator>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveRequestHandler()
        {
            _provider.GetService<IRequestHandler<Ping, Pong>>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveAsyncRequestHandler()
        {
            _provider.GetService<IAsyncRequestHandler<PingAsync, Pong>>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveCancelableAsyncRequestHandler()
        {
            _provider.GetService<ICancellableAsyncRequestHandler<PingCancellableAsync, Pong>>().ShouldNotBeNull();
        }

        [Fact]
        public void ShouldResolveNotificationHandlers()
        {
            _provider.GetServices<INotificationHandler<Pinged>>().Count().ShouldBe(3);
        }

        [Fact]
        public void ShouldResolveAsyncNotificationHandlers()
        {
            _provider.GetServices<IAsyncNotificationHandler<PingedAsync>>().Count().ShouldBe(3);
        }
    }
}