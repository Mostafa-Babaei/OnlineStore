using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Entities;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.Service
{
    public class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> brandRepository;
        private readonly IMapper mapper;

        public BrandService(IGenericRepository<Brand> brandRepository, IMapper mapper)
        {
            this.brandRepository = brandRepository;
            this.mapper = mapper;
        }
        public ApiResult DeleteBrand(int id)
        {
            Brand brand = GetBrand(id);
            if (brand == null)
                return ApiResult.ToErrorModel("برند یافت نشد");

            var result = brandRepository.Remove(brand);
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در حذف برند");
            return ApiResult.ToSuccessModel("حذف برند با موفقیت ثبت شد");
        }

        public List<Brand> GetAllBrand()
        {
            return brandRepository.GetAll().ToList();
        }

        public Brand GetBrand(int id)
        {
            return brandRepository.Find(e => e.Id == id).FirstOrDefault();
        }

        public BrandDto GetBrandDto(int id)
        {
            Brand brand = GetBrand(id);
            if (brand != null)
                return mapper.Map<BrandDto>(brand);
            return null;
        }

        public ApiResult InsertBrand(InsertBrandDto insertBrand)
        {
            Brand brand = mapper.Map<Brand>(insertBrand);

            brandRepository.Add(brand);
            int result = brandRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ثبت برند");
            return ApiResult.ToSuccessModel("ثبت برند با موفقیت ثبت شد");
        }

        public ApiResult UpdateBrand(EditBrandDto brand)
        {
            Brand brandTemp = GetBrand(brand.Id);
            if (brandTemp == null)
                return ApiResult.ToErrorModel("برند یافت نشد");

            brandTemp.BrandName = brand.BrandName;
            brandTemp.Logo = brand.Logo;
            brandTemp.IsActive = brand.IsActive;

            int result = brandRepository.SaveEntity();
            if (result <= 0)
                return ApiResult.ToErrorModel("خطا در ویرایش برند");
            return ApiResult.ToSuccessModel("ویرایش برند با موفقیت ثبت شد");

        }

    }
}
