﻿namespace Api.BusinessModels.UserModels
{
    public interface IUserService
    {
        public Task<UserLoginResponseModel> Login(UserLoginRequestModel request);
    }
}