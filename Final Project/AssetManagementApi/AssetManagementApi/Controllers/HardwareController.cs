using AssetManagementApi.Logging;
using AssetManagementApi.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace AssetManagementApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]

	public class HardwareController : ControllerBase
	{
		private readonly IHardware _hardwareRepository;
		private readonly ILog _logger;
		private readonly IUser _userRepository;
		private readonly IAssign _assetRepository;

		public HardwareController(IHardware hardwareRepository, ILog logger, IUser userRepository, IAssign assetRepository)
		{
			_hardwareRepository = hardwareRepository;
			_logger = logger;
			_userRepository = userRepository;
			_assetRepository = assetRepository;
		}

		[HttpGet]
		public List<Asset> GetAll()
		{
			_logger.Information("Hardware GetAll is logged");
			return _hardwareRepository.GetAll().ToList();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Add(HardwareModel hardwareModel)
		{
			_logger.Information("Hardware Add is logged");
			if (ModelState.IsValid)
			{
				Asset asset = new Asset();
				asset.Source = hardwareModel.Manufacturer;
				asset.Quantity = hardwareModel.Quantity;
				asset.Date = hardwareModel.DateOfPurchase;
				asset.Name = hardwareModel.Name;
				asset.Type = hardwareModel.Category;
				asset.Price = hardwareModel.Price;
				asset.CategoryId = _hardwareRepository.GetCategoryId("Hardware");
				_hardwareRepository.Insert(asset);
				_hardwareRepository.Save();
				_logger.Information("Hardware is Added");
				return Ok(hardwareModel);
			}
			else
			{
				_logger.Information("Hardware is Not Added");
				return BadRequest(ModelState);
			}
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			_logger.Information("Hardware delete is logged");
			if (id == 0)
			{
				_logger.Information("Hardware delete id is not valid");
				return NotFound("Id not found");
			}
			if (_hardwareRepository.Delete(id))
			{
				_hardwareRepository.Save();
				/*List<int> bookIds = _assetRepository.GetByBook(id);
				if (bookIds.Count > 0)
				{
					foreach (var book in bookIds)
					{
						_assetRepository.Delete(book);
						_assetRepository.Save();
					}
				}*/
				_logger.Information("Hardware is deleted");
				return Ok("Deleted Successfully");
			}
			_logger.Information("Hardware delete id is not found");
			return NotFound("Id not found");
		}

		[HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int id, AssetClass hardware)
		{
			_logger.Information("Hardware Update is logged");
			if (id == 0)
			{
				_logger.Information("Hardware update id is not found");
				return BadRequest("id not found");
			}
			if (ModelState.IsValid)
			{
				var updateHardware = _hardwareRepository.GetById(id);
				if (updateHardware != null)
				{

					updateHardware.Source = hardware.Source;
					updateHardware.Quantity = hardware.Quantity;
					updateHardware.Date = hardware.Date;
					updateHardware.Name = hardware.Name;
					updateHardware.Type = hardware.Type;
					updateHardware.Price = hardware.Price;
					updateHardware.CategoryId = _hardwareRepository.GetCategoryId("Hardware");
					_hardwareRepository.Save();
					_logger.Information("Hardware detail is updated");
					return Ok(updateHardware);
				}
				else
				{
					_logger.Information("Hardware update id is not found");
					return BadRequest("id not found");
				}
			}
			else
			{
				_logger.Information("Hardware update detail is not Valid");
				return BadRequest(ModelState);
			}
		}

		[HttpGet]
		public ActionResult<List<Asset>> Search(int id)
		{
			_logger.Information("Hardware search is logged");
			if (id == 0)
			{
				_logger.Information("Hardware search id is not found");
				return BadRequest("Invalid ID");
			}
			var hardware = _hardwareRepository.GetById(id);
			if (hardware == null)
			{
				_logger.Information("Hardware search id is not found");
				return NotFound("No Hardware found");
			}
			_logger.Information("Hardware search id is found");
			return Ok(hardware);
		}




		[HttpPost]
		public IActionResult AssignHardwareAsset(AssetModel asset)
		{
			_logger.Information("Hardware Assign is logged");
			int userId = asset.UserId;
			int hardwareId = asset.AssetId;
			if (userId == 0)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("Please Enter UserID");
			}
			if (hardwareId == 0)
			{
				_logger.Information("Hardware Id is not found");
				return BadRequest("Please Enter HardwareID");
			}
			//AssetDetail assetDetail = new AssetDetail();
			AssetAssignment assetAssignment = new AssetAssignment();
			var hardware = _hardwareRepository.GetById(hardwareId);
			var user = _userRepository.GetById(userId);
			if (hardware == null)
			{
				_logger.Information("Hardware Id is not found");
				return BadRequest("Hardware not found");
			}
			if (user == null)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("User not found");
			}

			if (hardware.Quantity <= 0)
			{
				_logger.Information("Book Quantity is Zero");
				return BadRequest("Book Quantity is Zero");
			}
			hardware.Quantity = hardware.Quantity - 1;

			assetAssignment.AssignDate = DateTime.Today;
			assetAssignment.AssetId = asset.AssetId;
			assetAssignment.UserId = asset.UserId;
			assetAssignment.Status = "Issued";
			assetAssignment.UnAssignDate = null;

			/*assetDetail.UserName = user.Name;
			assetDetail.UserId = user.UserId;
			assetDetail.AssetType = "Book";
			assetDetail.AssetId = bookId;
			assetDetail.Status = "Issued";*/
			_assetRepository.Insert(assetAssignment);
			_assetRepository.Save();
			_hardwareRepository.Save();
			_logger.Information("Book Issued");
			return Ok("Book Issued");
		}



		[HttpPost]
		public IActionResult RequestHardwareAsset(AssetModel asset)
		{
			_logger.Information("Book Assign is logged");
			int userId = asset.UserId;
			int hardwareId = asset.AssetId;
			if (userId == 0)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("Please Enter UserID");
			}
			if (hardwareId == 0)
			{
				_logger.Information("Book Id is not found");
				return BadRequest("Please Enter BookID");
			}
			//AssetDetail assetDetail = new AssetDetail();
			AssetAssignment assetAssignment = new AssetAssignment();
			var hardware = _hardwareRepository.GetById(hardwareId);
			var user = _userRepository.GetById(userId);
			if (hardware == null)
			{
				_logger.Information("Book Id is not found");
				return BadRequest("book not found");
			}
			if (user == null)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("User not found");
			}

			if (hardware.Quantity <= 0)
			{
				_logger.Information("Book Quantity is Zero");
				return BadRequest("Book Quantity is Zero");
			}
			// book.Quantity = book.Quantity - 1;

			assetAssignment.AssignDate = null;
			assetAssignment.AssetId = asset.AssetId;
			assetAssignment.UserId = asset.UserId;
			assetAssignment.Status = "Request";
			assetAssignment.UnAssignDate = null;

			/*assetDetail.UserName = user.Name;
			assetDetail.UserId = user.UserId;
			assetDetail.AssetType = "Book";
			assetDetail.AssetId = bookId;
			assetDetail.Status = "Issued";*/
			_assetRepository.Insert(assetAssignment);
			_assetRepository.Save();
			_hardwareRepository.Save();
			_logger.Information("Book Issued");
			return Ok("Book Issued");
		}

		[HttpPost]
		public IActionResult UnassignHardwareAsset(AssetModel asset)
		{
			_logger.Information("Hardware Unassign is logged ");
			int userId = asset.UserId;
			int hardwareId = asset.AssetId;
			if (userId == 0)
			{
				_logger.Information("UserID is not found");
				return BadRequest("Please Enter UserID");
			}
			if (hardwareId == 0)
			{
				_logger.Information("HardwareID is not found");
				return BadRequest("Please Enter HardwareID");
			}
			AssetAssignment assetAssignment = new AssetAssignment();
			var hardware = _hardwareRepository.GetById(hardwareId);
			var user = _userRepository.GetById(userId);
			if (hardware == null)
			{
				_logger.Information("HardwareID is not found");
				return BadRequest("Hardware not found");
			}
			if (user == null)
			{
				_logger.Information("UserID is not found");
				return BadRequest("User not found");
			}
			var assignId = _assetRepository.GetById(hardwareId);
			if (assignId == null)
			{
				_logger.Information("Asset Not Found");
				return BadRequest("Asset Not Found");
			}
			hardware.Quantity = hardware.Quantity + 1;
			assignId.UnAssignDate = DateTime.Today;
			assignId.Status = "Unassigned";
			//_assetRepository.Delete(assignId.Id);
			_assetRepository.Save();
			_hardwareRepository.Save();
			_logger.Information("Hardware Unassigned Successfully");
			return Ok("Hardware Unassigned Successfully");
		}

        [HttpPost]
        public IActionResult AcceptHardwareAsset(AssetModel asset)
        {
            _logger.Information("hardware Accept is logged");
            int userId = asset.UserId;
            int hardwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (hardwareId == 0)
            {
                _logger.Information("Hardware Id is not found");
                return BadRequest("Please Enter HardwareID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
            var hardware = _hardwareRepository.GetById(hardwareId);
            var user = _userRepository.GetById(userId);
            assetAssignment = _assetRepository.GetRequestAsset(hardwareId , userId);
            if (hardware == null)
            {
                _logger.Information("Hardware Id is not found");
                return BadRequest("Hardware not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            if (hardware.Quantity <= 0)
            {
                _logger.Information("Hardware Quantity is Zero");
                return BadRequest("Hardware Quantity is Zero");
            }
            hardware.Quantity = hardware.Quantity - 1;

            assetAssignment.AssignDate = DateTime.Today;
            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Issued";
            assetAssignment.UnAssignDate = null;

            /*assetDetail.UserName = user.Name;
			assetDetail.UserId = user.UserId;
			assetDetail.AssetType = "Book";
			assetDetail.AssetId = bookId;
			assetDetail.Status = "Issued";*/

            _assetRepository.Save();
            _hardwareRepository.Save();
            _logger.Information("hardware Issued");
            return Ok("hardware Issued");
        }

        [HttpPost]
        public IActionResult ReturnHardwareAsset(AssetModel asset)
        {
            _logger.Information("Hardware return is logged");
            int userId = asset.UserId;
            int hardwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (hardwareId == 0)
            {
                return BadRequest("Please Enter HardwareID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
            var hardware = _hardwareRepository.GetById(hardwareId);
            var user = _userRepository.GetById(userId);
            assetAssignment = _assetRepository.GetIssuedAsset(hardwareId, userId);
            if (hardware == null)
            {
                _logger.Information("Hardware Id is not found");
                return BadRequest("Hardware not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }
           
            hardware.Quantity = hardware.Quantity + 1;

            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Return";
            assetAssignment.UnAssignDate = DateTime.Today;

            _assetRepository.Save();
            _hardwareRepository.Save();
            _logger.Information("Hardware Returned");
            return Ok("Hardware Return");
        }

        [HttpPost]
        public IActionResult RejectHardwareAsset(AssetModel asset)
        {
            _logger.Information("Hardware Reject is logged");
            int userId = asset.UserId;
            int hardwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (hardwareId == 0)
            {
                _logger.Information("Hardware Id is not found");
                return BadRequest("Please Enter BookID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
            /* var book = _bookRepository.GetById(bookId);
             var user = _userRepository.GetById(userId);*/
            assetAssignment = _assetRepository.GetRequestAsset(hardwareId, userId);
            _assetRepository.Delete(assetAssignment.Id);


            _assetRepository.Save();

            _logger.Information("Hardware Rejected");
            return Ok("Hardware Rejected");
        }


    }


}

