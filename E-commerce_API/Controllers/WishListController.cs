using E_commerce_API.DTO;
using E_commerce_API.Models;
using E_commerce_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWishListRepository wishListRepository;

        public WishListController(UserManager<ApplicationUser> userManager, IWishListRepository wishListRepository)
        {
            this.userManager = userManager;
            this.wishListRepository = wishListRepository;
        }

        /*[HttpGet]
        [Authorize]
        public ActionResult<GeneralResponse> GetAll() 
        { 
            
        }*/
    }
}
