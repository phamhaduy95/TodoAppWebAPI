using DataAccessLayer.Entity;

namespace BussinessLayer.Service.Interface
{
    public interface ITokenGenerateService
    {
        public string GenerateToken(UserEntity user);
    }
}