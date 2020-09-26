﻿namespace Application.IntegrationTests.Cities.Commands
{
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Delete;
    using Common.Models;
    using Domain.Entities;
    using FluentAssertions;
    using NUnit.Framework;
    using static Testing;

    public class DeleteCityTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidCityId()
        {
            var command = new DeleteCityCommand { Id = 99 };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Test]
        public async Task ShouldDeleteCity()
        {
            var city = await SendAsync(new CreateCityCommand
            {
                Name = "Kayseri"
            });

            await SendAsync(new DeleteCityCommand
            {
                Id = city.Data.Id
            });

            var list = await FindAsync<City>(city.Data.Id);

            list.Should().BeNull();
        }
    }
}
