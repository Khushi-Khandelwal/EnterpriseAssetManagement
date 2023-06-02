using DataAccessLayer.Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IUser
    {


		bool Delete(int userId);
		IEnumerable<UserDetail> GetAll();
		UserDetail GetById(int userId);
		void Insert(UserDetail user);
		void Save();

        List<UserDTO> GetDetail();

        List<UserDTO> GetUserAsset(int id);

        
        bool AdminLogin(string email , string password);
        bool UserLogin(string email , string password);

        IEnumerable<Asset> GetAllAsset();

        int GetId(string email, string password);

        List<UserDTO> GetUserRequestAsset();

       
    }
}
