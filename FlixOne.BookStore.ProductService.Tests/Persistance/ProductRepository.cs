using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FlixOne.BookStore.ProductService.Tests.Contexts;
using FlixOne.BookStore.ProductService.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace FlixOne.BookStore.ProductService.Tests.Persistance
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;
        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void DeleteProduct(Guid id)
        {
            var product = GetBy(id);
            _context.Remove(product);
            _context.SaveChanges();
        }

        public IObservable<IEnumerable<Product>> GetAll()
            => Observable.Return(GetProducts());

        public IObservable<IEnumerable<Product>> GetAll(IScheduler scheduler)
            => Observable.Return(GetProducts(), scheduler);

        public Product GetBy(Guid id)
            => GetProducts().FirstOrDefault(x => x.Id == id);

        public IEnumerable<Product> GetProducts()
            => _context.Products
            .Include(x => x.Category)
            .OrderBy(x => x.Name)
            //.Select(x => x)
            .ToList();

        public IObservable<Unit> Remove(Guid id)
            => Remove(id, null);

        public IObservable<Unit> Remove(Guid id, IScheduler scheduler)
        {
            DeleteProduct(id);

            return scheduler != null
                ? Observable.Return(new Unit(), scheduler)
                : Observable.Return(new Unit());
        }
    }
}
