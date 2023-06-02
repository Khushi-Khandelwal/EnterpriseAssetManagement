using DataAccessLayer.Domain;
using DataAccessLayer.Repositories;

namespace AssetManagementApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
	public class UserController : ControllerBase
	{
		private readonly IUser _userRepository;
		
		public UserController(IUser userRepository)
		{
			_userRepository = userRepository;
			
		}

		[HttpGet]
		public List<UserDetail> GetAll()
		{
			return _userRepository.GetAll().ToList();
		}
        [HttpGet]
        public List<UserDTO> GetAssetDetail()
        {
            return _userRepository.GetDetail().ToList();
        }

        [HttpGet]
        public List<UserDTO> GetUserAssetDetail(int id)
        {
            return _userRepository.GetUserAsset(id).ToList();
        }

        [HttpGet]
		public ActionResult<List<UserDetail>> Search(int id)
		{
			if (id == 0)
			{
				return BadRequest("Invalid ID");
			}
			var user = _userRepository.GetById(id);
			if (user == null)
			{
				return NotFound("No User found");
			}
			return Ok(user);
		}

		[HttpPost]
		public IActionResult Add(UserModel userModel)
		{
			if (ModelState.IsValid)
			{
				UserDetail user = new UserDetail();
				user.Name = userModel.Name;
				user.PhoneNumber = userModel.PhoneNumber;
				user.DateOfBirth = userModel.DateOfBirth;
				user.Email = userModel.Email;
				user.Password = userModel.Password;
				user.IsAdmin = false;
				_userRepository.Insert(user);
				_userRepository.Save();
				return Ok(userModel);
				//return Ok ("Added");
			}
			else
			{
				return BadRequest(ModelState);
			}

		}
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			if (id == 0)
			{
				return NotFound("Id not found");
			}
			if (_userRepository.Delete(id))
			{
				_userRepository.Save();
			
				return Ok("Deleted Successfully");
			}
			return NotFound("Id not found");
		}

		[HttpPut]
		public IActionResult Update(int id, UserModel userModel)
		{
			if (id == 0) { return BadRequest("id not found"); }
			if (ModelState.IsValid)
			{
				var updateUser = _userRepository.GetById(id);
				if (updateUser != null)
				{
					updateUser.Name = userModel.Name;
					updateUser.PhoneNumber = userModel.PhoneNumber;
					updateUser.DateOfBirth = userModel.DateOfBirth;
					updateUser.Email = userModel.Email;
					updateUser.Password = userModel.Password;
					updateUser.IsAdmin = false;
					_userRepository.Save();
					return Ok(updateUser);
				}
				else { return BadRequest("id not found"); }
			}
			else
			{
				return BadRequest(ModelState);
			}
		}

        [HttpGet]
        public List<Asset> GetAllAsset()
        {
          
            return _userRepository.GetAllAsset().ToList();
        }

		[HttpPost]
		public int GetUserId(Login user)
		{
			return _userRepository.GetId(user.Email,user.Password);

        }

		[HttpGet]
		public List<UserDTO> GetRequestAsset()
		{
			return _userRepository.GetUserRequestAsset().ToList();
		}

		
    }
}
	
	
