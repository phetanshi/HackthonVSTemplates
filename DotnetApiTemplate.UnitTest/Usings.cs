global using DotnetApiTemplate.Data;
global using DotnetApiTemplate.Data.Models;

global using DotnetApiTemplate.Api;
global using DotnetApiTemplate.Api.Auth;
global using DotnetApiTemplate.Api.AppStart;
global using DotnetApiTemplate.Api.Services;
global using DotnetApiTemplate.Api.Services.Definitions;
global using DotnetApiTemplate.Api.Controllers;
global using DotnetApiTemplate.Api.Constants;
global using DotnetApiTemplate.Api.Util;
global using DotnetApiTemplate.Api.AutoMapperProfiles;


global using Application.Dtos;
global using DotnetApiTemplate.UnitTest.TestHelpers;
global using Microsoft.Data.Sqlite;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Moq;
global using Xunit;
global using AutoMapper;
global using Microsoft.AspNetCore.Http;
global using Ps.EfCoreRepository.SqlServer;

global using DotnetApiTemplate.UnitTest.TestHelpers.DbAsyncQueryProvider;