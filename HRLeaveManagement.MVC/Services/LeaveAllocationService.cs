using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _httpclient;

        public LeaveAllocationService(IClient httpClient, ILocalStorageService localStorageService):
            base(httpClient, localStorageService)
        {
            _localStorageService = localStorageService;
            _httpclient = httpClient;
        }

        public async Task<Response<int>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveAllocationDto createLeaveAllocation = new CreateLeaveAllocationDto() { LeaveTypeId = leaveTypeId };
                AddBearerToken();
                var apiResponse = await _client.LeaveAllocationsPOSTAsync(createLeaveAllocation);
                if (apiResponse.Success)
                {
                    response.Success = true;
                }
                else
                {
                    foreach (var error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }
                return response;

            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
