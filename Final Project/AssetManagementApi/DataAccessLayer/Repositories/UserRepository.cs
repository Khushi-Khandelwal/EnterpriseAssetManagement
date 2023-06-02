using DataAccessLayer.Domain;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUser
    {

        private readonly AssetManagmentContext database;
        public UserRepository(AssetManagmentContext database)
        {
            this.database = database;
        }

        public bool Delete(int userId)
        {
            UserDetail user = database.UserDetails.Find(userId);
            if (user != null)
            {
                database.UserDetails.Remove(user);
                return true;
            }
            return false;
        }

        public IEnumerable<UserDetail> GetAll()
        {
            return database.UserDetails.ToList();
            //return database.Users.Include(x=>x.AssetDetails).ToList();
        }

        public UserDetail GetById(int userId)
        {
            return database.UserDetails.Find(userId);
        }

        public void Insert(UserDetail user)
        {
            database.UserDetails.Add(user);
        }
        public void Save()
        {
            database.SaveChanges();
        }

        public List<UserDTO> GetDetail()
        {

            var users = (from assign in database.AssetAssignments
                         join user in database.UserDetails
                         on assign.UserId equals user.UserId
                         join asset in database.Assets
                         on assign.AssetId equals asset.AssetId

                         select new UserDTO()
                         {
                             UserId = user.UserId,
                             AssetId = asset.AssetId,
                             Status = assign.Status,
                             Price = (int)asset.Price,
                             UserName = user.Name,
                             AssetType = (from category in database.AssetCategories
                                          where category.CategoryId == asset.CategoryId
                                          select category.Name).FirstOrDefault(),
                             AssetName = asset.Name,
                             PhoneNumber = user.PhoneNumber,
                             AssignDate = (DateTime)(assign.AssignDate != null ? assign.AssignDate : null),
                             UnAssignDate = (DateTime)(assign.UnAssignDate != null ? assign.UnAssignDate : null),

                         }).ToList();

            return users;
        }

        public List<UserDTO> GetUserAsset(int UserId)
        {

            var users = (from assign in database.AssetAssignments
                         join user in database.UserDetails
                         on assign.UserId equals user.UserId
                         join asset in database.Assets
                         on assign.AssetId equals asset.AssetId

                         select new UserDTO()
                         {
                             UserId = user.UserId,
                             AssetId = asset.AssetId,
                             Status = assign.Status,
                             Price = (int)asset.Price,
                             UserName = user.Name,
                             AssetType = (from category in database.AssetCategories
                                          where category.CategoryId == asset.CategoryId
                                          select category.Name).FirstOrDefault(),
                             AssetName = asset.Name,
                             PhoneNumber = user.PhoneNumber,
                             AssignDate = (DateTime)(assign.AssignDate != null ? assign.AssignDate : null),
                             UnAssignDate = (DateTime)(assign.UnAssignDate != null ? assign.UnAssignDate : null),

                         }).Where(i => i.UserId == UserId).ToList();

            return users;
        }

        public List<UserDTO> GetUserRequestAsset()
        {

            var users = (from assign in database.AssetAssignments
                         join user in database.UserDetails
                         on assign.UserId equals user.UserId
                         join asset in database.Assets
                         on assign.AssetId equals asset.AssetId

                         select new UserDTO()
                         {
                             UserId = user.UserId,
                             AssetId = asset.AssetId,
                             Status = assign.Status,
                             Price = (int)asset.Price,
                             UserName = user.Name,
                             AssetType = (from category in database.AssetCategories
                                          where category.CategoryId == asset.CategoryId
                                          select category.Name).FirstOrDefault(),
                             AssetName = asset.Name,
                             PhoneNumber = user.PhoneNumber,
                             AssignDate = (DateTime)(assign.AssignDate != null ? assign.AssignDate : null),
                             UnAssignDate = (DateTime)(assign.UnAssignDate != null ? assign.UnAssignDate : null),

                         }).Where(i => i.Status == "Request").ToList();

            return users;
        }

        public bool AdminLogin(string Email, string password)
        {
            var result = (from admin in database.UserDetails
                          where admin.Email == Email && admin.Password == password && admin.IsAdmin
                          select admin);
            if (result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UserLogin(string Email, string password)
        {
            var result = (from user in database.UserDetails
                          where user.Email == Email && user.Password == password && !(user.IsAdmin)
                          select user);
            if (result.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<Asset> GetAllAsset()
        {
            var result = (from asset in database.Assets
                          join category in database.AssetCategories
                          on asset.CategoryId equals category.CategoryId
                          where asset.Quantity > 0
                          select asset).ToList();
            return result;

        }


        public int GetId(string Email, string password)
        {
            var result = (from user in database.UserDetails
                          where user.Email == Email && user.Password == password && !(user.IsAdmin)
                          select user.UserId).FirstOrDefault();
            return result;
        }

    }
}
