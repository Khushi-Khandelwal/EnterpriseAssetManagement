using AssetManagementApi.Logging;
using AssetManagementApi.Models;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AssetManagementApi.Controllers
{
	[ApiController]
	[Route("[controller]/[action]")]
  
    public class BookController : ControllerBase
	{
       
        private readonly IBook _bookRepository;
        private readonly IUser _userRepository;
		private readonly ILog _logger;
		private readonly IAssign _assetRepository;

		public BookController(IBook bookRepository , ILog logger, IUser userRepository , IAssign assetRepository)
		{
			_bookRepository = bookRepository;
			_logger = logger;
			_userRepository = userRepository;
			_assetRepository = assetRepository;
		}

		[HttpGet]
		public List<Asset> GetAll()
		{
			_logger.Information("Book GetAll is logged");
			return _bookRepository.GetAll().ToList();
		}
		[HttpPost]
		[Authorize(Roles = "Admin")]
		public IActionResult Add(BookModel bookModel)
		{
			_logger.Information("Book Add is logged");
			if (ModelState.IsValid)
			{
				Asset asset = new Asset();
				asset.Source = bookModel.AuthorName;
				asset.Quantity = bookModel.Quantity;
			    asset.Date = bookModel.DateOfPublishing;
				asset.Name = bookModel.BookName;
				asset.Type = bookModel.Genre;
				asset.Price = bookModel.Price;
				asset.CategoryId = _bookRepository.GetCategoryId("Book");
				_bookRepository.Insert(asset);
				_bookRepository.Save();
				_logger.Information("Book is Added");
				return Ok(bookModel);
			}
			else
			{
				_logger.Information("Book is Not Added");
				return BadRequest(ModelState);
			}
		}


		[HttpDelete("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Delete(int id)
		{
			_logger.Information("Book delete is logged");
			if (id == 0)
			{
				_logger.Information("Book delete id is not valid");
				return NotFound("Id not found");
			}
			if (_bookRepository.Delete(id))
			{
				_bookRepository.Save();
				_logger.Information("Book is deleted");
				return Ok("Deleted Successfully");
			}
			_logger.Information("Book delete id is not found");
			return NotFound("Id not found");
		}

    


        [HttpPut("{id}")]
		[Authorize(Roles = "Admin")]
		public IActionResult Update(int id, AssetClass book)
        {
            _logger.Information("Book Update is logged");
            if (id == 0)
            {
                _logger.Information("Book update id is not found");
                return BadRequest("id not found");
            }
            if (ModelState.IsValid)
            {
                var updateBook = _bookRepository.GetById(id);
                if (updateBook != null)
                {

                    updateBook.Source = book.Source;
                    updateBook.Quantity = book.Quantity;
                    updateBook.Date = book.Date;
                    updateBook.Name = book.Name;
                    updateBook.Type = book.Type;
                    updateBook.Price = book.Price;
                    updateBook.CategoryId = _bookRepository.GetCategoryId("Book");
                    _bookRepository.Save();
                    _logger.Information("Book detail is updated");
                    return Ok(updateBook);
                }
                else
                {
                    _logger.Information("Book update id is not found");
                    return BadRequest("id not found");
                }
            }
            else
            {
                _logger.Information("Book update detail is not Valid");
                return BadRequest(ModelState);
            }
        }



        [HttpGet]
		public ActionResult<List<Asset>> Search(int id)
		{
			_logger.Information("Book search is logged");
			if (id == 0)
			{
				_logger.Information("Book search id is not found");
				return BadRequest("Invalid ID");
			}
			var book = _bookRepository.GetById(id);
			if (book == null)
			{
				_logger.Information("Book search id is not found");
				return NotFound("No booklets found");
			}
			_logger.Information("Book search id is found");
			return Ok(book);
		}


		[HttpPost]
		public IActionResult AssignBookAsset(AssetModel asset)
		{
			_logger.Information("Book Assign is logged");
			int userId = asset.UserId;
			int bookId = asset.AssetId;
			if (userId == 0)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("Please Enter UserID");
			}
			if (bookId == 0)
			{
				_logger.Information("Book Id is not found");
				return BadRequest("Please Enter BookID");
			}
			
			AssetAssignment assetAssignment = new AssetAssignment();
			var book = _bookRepository.GetById(bookId);
			var user = _userRepository.GetById(userId);
			if (book == null)
			{
				_logger.Information("Book Id is not found");
				return BadRequest("book not found");
			}
			if (user == null)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("User not found");
			}

			if (book.Quantity <= 0)
			{
				_logger.Information("Book Quantity is Zero");
				return BadRequest("Book Quantity is Zero");
			}
			book.Quantity = book.Quantity - 1;

			assetAssignment.AssignDate = DateTime.Today;
			assetAssignment.AssetId = asset.AssetId;
			assetAssignment.UserId = asset.UserId;
			assetAssignment.Status = "Issued";
			assetAssignment.UnAssignDate = null;

		

			_assetRepository.Insert(assetAssignment);

			_assetRepository.Save();
			_bookRepository.Save();
			_logger.Information("Book Issued");
			return Ok("Book Issued");
		}



        [HttpPost]
        public IActionResult AcceptBookAsset(AssetModel asset)
        {
            _logger.Information("Book Assign is logged");
            int userId = asset.UserId;
            int bookId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (bookId == 0)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("Please Enter BookID");
            }
          
            AssetAssignment assetAssignment = new AssetAssignment();
			var book = _bookRepository.GetById(bookId);
			var user = _userRepository.GetById(userId);
			assetAssignment = _assetRepository.GetRequestAsset(bookId, userId);
			if (book == null)
			{
				_logger.Information("Book Id is not found");
				return BadRequest("book not found");
			}
			if (user == null)
			{
				_logger.Information("User Id is not found ");
				return BadRequest("User not found");
			}

			if (book.Quantity <= 0)
            {
                _logger.Information("Book Quantity is Zero");
                return BadRequest("Book Quantity is Zero");
            }
            book.Quantity = book.Quantity - 1;

            assetAssignment.AssignDate = DateTime.Today;
            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Issued";
            assetAssignment.UnAssignDate = null;

            _assetRepository.Save();
            _bookRepository.Save();
            _logger.Information("Book Issued");
            return Ok("Book Issued");
        }



        [HttpPost]
        public IActionResult ReturnBookAsset(AssetModel asset)
        {
            _logger.Information("Book Assign is logged");
            int userId = asset.UserId;
            int bookId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (bookId == 0)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("Please Enter BookID");
            }
           
            AssetAssignment assetAssignment = new AssetAssignment();
            var book = _bookRepository.GetById(bookId);
            var user = _userRepository.GetById(userId);
            assetAssignment = _assetRepository.GetIssuedAsset(bookId, userId);
            if (book == null)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("book not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            book.Quantity = book.Quantity + 1;

            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Return";
            assetAssignment.UnAssignDate = DateTime.Today;

            _assetRepository.Save();
            _bookRepository.Save();
            _logger.Information("Book Returned");
            return Ok("Book Returned");
        }



        [HttpPost]
        public IActionResult RejectBookAsset(AssetModel asset)
        {
            _logger.Information("Book Assign is logged");
            int userId = asset.UserId;
            int bookId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (bookId == 0)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("Please Enter BookID");
            }
          
            AssetAssignment assetAssignment = new AssetAssignment();
           /* var book = _bookRepository.GetById(bookId);
            var user = _userRepository.GetById(userId);*/
            assetAssignment = _assetRepository.GetRequestAsset(bookId, userId);
			_assetRepository.Delete(assetAssignment.Id);
         

            _assetRepository.Save();
          
            _logger.Information("Book Returned");
            return Ok("Book Returned");
        }



        [HttpPost]
        public IActionResult RequestBookAsset(AssetModel asset)
        {
            _logger.Information("Book Assign is logged");
            int userId = asset.UserId;
            int bookId = asset.AssetId;
            if (userId == 0)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("Please Enter UserID");
            }
            if (bookId == 0)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("Please Enter BookID");
            }
           
            AssetAssignment assetAssignment = new AssetAssignment();
            var book = _bookRepository.GetById(bookId);
            var user = _userRepository.GetById(userId);
            if (book == null)
            {
                _logger.Information("Book Id is not found");
                return BadRequest("book not found");
            }
            if (user == null)
            {
                _logger.Information("User Id is not found ");
                return BadRequest("User not found");
            }

            if (book.Quantity <= 0)
            {
                _logger.Information("Book Quantity is Zero");
                return BadRequest("Book Quantity is Zero");
            }
           
            assetAssignment.AssignDate = null;
            assetAssignment.AssetId = asset.AssetId;
            assetAssignment.UserId = asset.UserId;
            assetAssignment.Status = "Request";
            assetAssignment.UnAssignDate = null;

            _assetRepository.Insert(assetAssignment);
            _assetRepository.Save();
            _bookRepository.Save();
            _logger.Information("Book Requested");
            return Ok("Book Requested");
        }

        [HttpPost]
		public IActionResult UnassignBookAsset(AssetModel asset)
		{
			_logger.Information("Book Unassign is logged ");
			int userId = asset.UserId;
			int bookId = asset.AssetId;
			if (userId == 0)
			{
				_logger.Information("UserID is not found");
				return BadRequest("Please Enter UserID");
			}
			if (bookId == 0)
			{
				_logger.Information("BookID is not found");
				return BadRequest("Please Enter BookID");
			}
			AssetAssignment assetAssignment = new AssetAssignment();
			var book = _bookRepository.GetById(bookId);
			var user = _userRepository.GetById(userId);
			if (book == null)
			{
				_logger.Information("BookID is not found");
				return BadRequest("book not found");
			}
			if (user == null)
			{
				_logger.Information("UserID is not found");
				return BadRequest("User not found");
			}
			var assignId = _assetRepository.GetById(bookId);
			if (assignId == null)
			{
				_logger.Information("Asset Not Found");
				return BadRequest("Asset Not Found");
			}
			book.Quantity = book.Quantity + 1;
			assignId.UnAssignDate = DateTime.Today;
			assignId.Status = "Unassigned";
			
			_assetRepository.Save();
			_bookRepository.Save();
			_logger.Information("Book Unassigned Successfully");
			return Ok("Book Unassigned Successfully");
		}

	}

	}
