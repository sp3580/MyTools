namespace Api.BusinessModels.UserModels
{
    public interface IUserService
    {
        public Task<UserLoginResponseModel> Login(UserLoginRequestModel request);
        public Task<UserSearchResponseModel> Search(UserSearchRequestModel request);
        public Task<UserDetailResponseModel> Detail(UserDetailRequestModel request);
        public Task<UserCheckExistResponseModel> CheckExist(UserCheckExistRequestModel request);
        public Task<UserCreateResponseModel> Create(UserCreateRequestModel request);
        public Task<UserModifyResponseModel> Modify(UserModifyRequestModel request);
        public Task<UserDeleteResponseModel> Delete(UserDeleteRequestModel request);
        public Task<UserExportResponseModel> Export(UserExportRequestModel request);
    }
}
