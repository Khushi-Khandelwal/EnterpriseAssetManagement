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

    public class SoftwareController : ControllerBase
	{
        private readonly ISoftware _softwareRepository;
        private readonly ILog _logger;
        private readonly IUser _userRepository;
        private readonly IAssign _assetRepository;

        public SoftwareController(ISoftware SoftwareRepository, ILog logger, IUser user, IAssign assetRepository)
		{
			_softwareRepository = SoftwareRepository;
			_logger = logger;
			_userRepository = user;
			_assetRepository = assetRepository;
		}


		[HttpGet]
		public List<Asset> GetAll()
		{
			_logger.Information("Software GetAll is logged");
			return _softwareRepository.GetAll().ToList();
		}
		[HttpPost]
       /* [Authorize(Roles = "Admin")]*/
        public IActionResult Add(SoftwareModel softwareModel)
		{
			_logger.Information("Software Add is logged");
			if (ModelState.IsValid)
			{
				Asset asset = new Asset();
				asset.Source = softwareModel.LicenceNumber;
				asset.Quantity = softwareModel.Quantity;
				asset.Date = softwareModel.DateOfExpiry;
				asset.Name = softwareModel.SoftwareName;
				asset.Type = softwareModel.TypeOfSoftware;
				asset.Price = softwareModel.Price;
				asset.CategoryId = _softwareRepository.GetCategoryId("Software");
				_softwareRepository.Insert(asset);
				_softwareRepository.Save();
				_logger.Information("Software is Added");
				return Ok(softwareModel);
			}
			else
			{
				_logger.Information("Software is Not Added");
				return BadRequest(ModelState);
			}
		}


		[HttpDelete("{id}")]
       /* [Authorize(Roles = "Admin")]*/
        public IActionResult Delete(int id)
		{
			_logger.Information("Software delete is logged");
			if (id == 0)
			{
				_logger.Information("Software delete id is not valid");
				return NotFound("Id not found");
			}
			if (_softwareRepository.Delete(id))
			{
				_softwareRepository.Save();
				
				_logger.Information("Software is deleted");
				return Ok("Deleted Successfully");
			}
			_logger.Information("Software delete id is not found");
			return NotFound("Id not found");
		}

		[HttpPut("{id}")]
       /* [Authorize(Roles = "Admin")]*/
        public IActionResult Update(int id, AssetClass software)
		{
			_logger.Information("Software Update is logged");
			if (id == 0)
			{
				_logger.Information("Software update id is not found");
				return BadRequest("id not found");
			}
			if (ModelState.IsValid)
			{
				var updateSoftware = _softwareRepository.GetById(id);
				if (updateSoftware != null)
				{

					updateSoftware.Source = software.Source;
					updateSoftware.Quantity = software.Quantity;
					updateSoftware.Date = software.Date;
					updateSoftware.Name = software.Name;
					updateSoftware.Type = software.Type;
					updateSoftware.Price = software.Price;
					updateSoftware.CategoryId = _softwareRepository.GetCategoryId("Software");
					_softwareRepository.Save();
					_logger.Information("Software detail is updated");
					return Ok(updateSoftware);
				}
				else
				{
					_logger.Information("Software update id is not found");
					return BadRequest("id not found");
				}
			}
			else
			{
				_logger.Information("Software update detail is not Valid");
				return BadRequest(ModelState);
			}
		}

		[HttpGet]
		public ActionResult<List<Asset>> Search(int id)
		{
			_logger.Information("Software search is logged");
			if (id == 0)
			{
				_logger.Information("Software search id is not found");
				return BadRequest("Invalid ID");
			}
			var software = _softwareRepository.GetById(id);
			if (software == null)
			{
				_logger.Information("Software search id is not found");
				return NotFound("No Software found");
			}
			_logger.Information("Software search id is found");
			return Ok(software);
		}




        [HttpPost]
        public IActionResult AssignSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Assign is logged");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Please Enter SoftwareID");
            }
            //AssetDetail assetDetail = new AssetDetail();
            AssetAssignment assetAssignment = new AssetAssignment();
            var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);
            if (software == null)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Software not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            if (software.Quantity <= 0)
            {
                _logger.Information("Software Quantity is Zero");
                return BadRequest(" Software Quantity is Zero");
            }
            software.Quantity = software.Quantity - 1;

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
            _softwareRepository.Save();
            _logger.Information("Book Issued");
            return Ok("Book Issued");
        }



        [HttpPost]
        public IActionResult RequestSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Assign is logged");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("software Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("software Id is not found");
                return BadRequest("Please Enter softwareID");
            }
            //AssetDetail assetDetail = new AssetDetail();
            AssetAssignment assetAssignment = new AssetAssignment();
            var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);
            if (software == null)
            {
                _logger.Information("software Id is not found");
                return BadRequest("software not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            if (software.Quantity <= 0)
            {
                _logger.Information("software Quantity is Zero");
                return BadRequest("software Quantity is Zero");
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
            _softwareRepository.Save();
            _logger.Information("Software Issued");
            return Ok("Software Issued");
        }

        [HttpPost]
        public IActionResult UnassignSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Unassign is logged ");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("UserID is not found");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("SoftwareID is not found");
                return BadRequest("Please Enter SoftwareID");
            }
            AssetAssignment assetAssignment = new AssetAssignment();
            var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);
            if (software == null)
            {
                _logger.Information("SoftwareID is not found");
                return BadRequest("Software not found");
            }
            if (user == null)
            {
                _logger.Information("UserID is not found");
                return BadRequest("User not found");
            }
            var assignId = _assetRepository.GetById(softwareId);
            if (assignId == null)
            {
                _logger.Information("Asset Not Found");
                return BadRequest("Asset Not Found");
            }
            software.Quantity = software.Quantity + 1;
            assignId.UnAssignDate = DateTime.Today;
            assignId.Status = "Unassigned";
            //_assetRepository.Delete(assignId.Id);
            _assetRepository.Save();
            _softwareRepository.Save();
            _logger.Information("Book Unassigned Successfully");
            return Ok("Software Unassigned Successfully");
        }



        [HttpPost]
        public IActionResult AcceptSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Assign is logged");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Please Enter SoftwareID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
            var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);
            assetAssignment = _assetRepository.GetRequestAsset(softwareId , userId);
            if (software == null)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Software not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            if (software.Quantity <= 0)
            {
                _logger.Information("Software Quantity is Zero");
                return BadRequest("Software Quantity is Zero");
            }
            software.Quantity = software.Quantity - 1;

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
            _softwareRepository.Save();
            _logger.Information("Software Issued");
            return Ok("Book Issued");
        }

        [HttpPost]
        public IActionResult ReturnSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Assign is logged");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Please Enter SoftwareID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
            var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);
            assetAssignment = _assetRepository.GetIssuedAsset(softwareId, userId);
            if (software == null)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Software not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }
           
            software.Quantity = software.Quantity + 1;

            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Return";
            assetAssignment.UnAssignDate = DateTime.Today;

            _assetRepository.Save();
            _softwareRepository.Save();
            _logger.Information("software Issued");
            return Ok("software Issued");
        }

        [HttpPost]
        public IActionResult RejectSoftwareAsset(AssetModel asset)
        {
            _logger.Information("Software Assign is logged");
            int userId = asset.UserId;
            int softwareId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (softwareId == 0)
            {
                _logger.Information("Software Id is not found");
                return BadRequest("Please Enter BookID");
            }

            AssetAssignment assetAssignment = new AssetAssignment();
        /*    var software = _softwareRepository.GetById(softwareId);
            var user = _userRepository.GetById(userId);*/
            assetAssignment = _assetRepository.GetRequestAsset(softwareId, userId);
            _assetRepository.Delete(assetAssignment.Id);


            _assetRepository.Save();

            _logger.Information("Software Issued");
            return Ok("Software Issued");
        }

    }

}





	
	
