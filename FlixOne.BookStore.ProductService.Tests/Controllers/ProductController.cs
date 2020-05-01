using FlixOne.BookStore.ProductService.Tests.Models;
using FlixOne.BookStore.ProductService.Tests.Persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Tests.Controllers
{
    public class ProductController: Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository(new Contexts.ProductContext());
        }

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var observable = _productRepository.GetAll();
            var result = observable.SelectMany(x => x).ToArray();
            return await result;
        }
    }
}
