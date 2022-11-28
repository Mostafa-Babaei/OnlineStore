using Application.Common.Model;
using Application.Model;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service
{
    public interface IBrandService
    {
        /// <summary>
        /// دریافت برند ها
        /// </summary>
        /// <returns></returns>
        List<BrandDto> GetAllBrand();

        /// <summary>
        /// ثبت برند
        /// </summary>
        /// <param name="insertBrand"></param>
        /// <returns></returns>
        ApiResult InsertBrand(InsertBrandDto insertBrand);

        /// <summary>
        /// حذف برند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ApiResult DeleteBrand(int id);

        /// <summary>
        /// دریافت برند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Brand GetBrand(int id);

        /// <summary>
        /// دریافت مدل برند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BrandDto GetBrandDto(int id);

        /// <summary>
        /// بروزرسانی برند
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        ApiResult UpdateBrand(EditBrandDto brand);

    }
}
