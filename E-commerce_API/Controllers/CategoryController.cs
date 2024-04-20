using E_commerce_API.DTO;
using E_commerce_API.Models;
using E_commerce_API.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<GeneralResponse> GetAllCategory() 
        { 
            List<Category> categories = categoryRepository.GetAll();
            List<CategoryWithProduct> categoryWithProducts = categories.Select(category =>
                new CategoryWithProduct
                {
                    Id = category.Id,
                    Category_Name = category.Name,
                    ProductNames = category.products?.Select(p => p.Name).ToList()
                }).ToList();
            //return ok(categoriesWithProduct);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = categoryWithProducts
            };
            return response;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public ActionResult<GeneralResponse> GetById(int id) 
        { 
            var category = categoryRepository.GetById(id);

            if(category  == null)
            {
                //return NotFound
                GeneralResponse localResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Not Found!"
                };

                return localResponse;
            }

            var categoryWithProducts = new CategoryWithProduct
            {
                Id = category.Id,
                Category_Name = category.Name,
                ProductNames = category.products.Select(p => p.Name).ToList()
            };

            //return Ok(categoryWithProducts);
            GeneralResponse response = new GeneralResponse()
            {
                IsPass = true,
                Message = categoryWithProducts
            };
            return response;
        }

        [HttpPost]
        [Authorize]
        public ActionResult<GeneralResponse> AppCategory(CatDTO catDTO)
        {
            if(ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = catDTO.Name
                    //  products = catDTO.ProductNames.Select(name => new Product { Name = name }).ToList()
                };
                categoryRepository.Insert(category);
                categoryRepository.Save();
                // return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
                return new GeneralResponse
                {
                    IsPass = true,
                    Message = new
                    {
                        category.Id,
                        category.Name,
                    }
                };
            }
            else
            {
                GeneralResponse localResponse = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Cannot Add Category"
                };
                return localResponse;
            }
        }

        [HttpPut]
        [Authorize]
        public ActionResult<GeneralResponse> Edit(int id, CatDTO updatedDTO) 
        { 
            Category oldCategory = categoryRepository.GetById(id);
            if (oldCategory == null)
            {
                GeneralResponse response = new GeneralResponse()
                {
                    IsPass = false,
                    Message = "Not Found"
                };
                return response;
            }

            oldCategory.Name = updatedDTO.Name;
            categoryRepository.Update(oldCategory);
            categoryRepository.Save();

            GeneralResponse generalResponse = new GeneralResponse()
            {
                IsPass = true,
                Message = new
                {
                    oldCategory.Id,
                    oldCategory.Name,
                }
            };
            return generalResponse;
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public IActionResult Remove(int id)
        {
            try
            {
                categoryRepository.Delete(id);
                categoryRepository.Save();
                GeneralResponse response = new GeneralResponse()
                {
                    IsPass = true,
                    Message = "Done"
                };

                return Ok(response);
            }
            catch(Exception ex) 
            {
                GeneralResponse response = new GeneralResponse()
                {
                    IsPass = false,
                    Message = ex.Message
                };
                return StatusCode(500, response);
            }
        }
    }
}
