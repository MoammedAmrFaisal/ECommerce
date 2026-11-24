using Microsoft.AspNetCore.Mvc;
using Onion.Api.Errors;
using Onion.Api.Helper;
using Onion.Core.Repositories;
using Onion.Repository;

namespace Onion.Api.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            // Add Generic Repository
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            
            // Add Auto Mapper
            Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
           
            //Add API Errors 
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                    .SelectMany(p => p.Value.Errors)
                    .Select(E => E.ErrorMessage)
                    .ToList();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });

            return Services;
        }
    }
}
