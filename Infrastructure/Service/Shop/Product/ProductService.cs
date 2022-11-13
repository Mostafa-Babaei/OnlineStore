using Application.Common.Model;
using Application.Model;
using AutoMapper;
using Domain.Models;
using infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<Product> productRepository;

        public ProductService(
            IMapper mapper, IGenericRepository<Product> productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }


    }
}
