using AutoMapper;
using HRLeaveManagement.MVC.Contracts;
using HRLeaveManagement.MVC.Models;
using HRLeaveManagement.MVC.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRLeaveManagement.MVC.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {

        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public LeaveTypeService(IMapper mapper, IClient httpClient, ILocalStorageService localStorageService): base(httpClient, localStorageService)
        {
            this._localStorageService = localStorageService;
            this._mapper = mapper;
            this._httpClient = httpClient;
        }

        public async Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType)
        {
            try
            {
                var response = new Response<int>();
                CreateLeaveTypeDto createLeaveType = _mapper.Map<CreateLeaveTypeDto>(leaveType);

                //bearer token - JWT authentication
                AddBearerToken();

                var apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveType);
                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
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

        public async Task<Response<int>> DeleteLeaveType(int id)
        {
            try
            {
                //bearer token - JWT authentication
                AddBearerToken();
                await _client.LeaveTypesDELETEAsync(id);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }

        public async Task<LeaveTypeVM> GetLeaveTypeDetails(int id)
        {
            //bearer token - JWT authentication
            AddBearerToken();
            var leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeVM>(leaveType);
        }

        public async Task<List<LeaveTypeVM>> GetLeaveTypes()
        {
            //bearer token - JWT authentication
            AddBearerToken();
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeVM>>(leaveTypes);
        }

        public async Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType)
        {
            try
            {
                //bearer token - JWT authentication
                AddBearerToken();
                LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.LeaveTypesPUTAsync(id.ToString(), leaveTypeDto);
                return new Response<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<int>(ex);
            }
        }
    }
}
