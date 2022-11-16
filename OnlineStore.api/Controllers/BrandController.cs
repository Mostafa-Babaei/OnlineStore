using Application.Common.Model;
using Application.Constant.Message;
using Application.Model;
using infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }
        /// <summary>
        /// دریافت برند ها
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult Get()
        {
            try
            {
                return ApiResult.ToSuccessModel(BrandMessages.ReceivedBrandsSuccess, brandService.GetAllBrand());
            }
            catch (Exception)
            {
                return ApiResult.ToErrorModel(BrandMessages.ReceivedBrandsFailure);
            }
        }

        /// <summary>
        /// افزودن برند جدید
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult AddCategory([FromBody] InsertBrandDto brandmo)
        {
            return brandService.InsertBrand(brandmo);
        }

        /// <summary>
        /// ویرایش برند
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ApiResult EditCategory([FromBody] EditBrandDto brand)
        {
            return brandService.UpdateBrand(brand);
        }

        /// <summary>
        /// حذف برند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteCategory(int id)
        {
            return brandService.DeleteBrand(id);
        }
    }
}
