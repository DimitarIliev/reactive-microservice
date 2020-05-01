using FlixOne.BookStore.ProductService.Tests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Threading.Tasks;

namespace FlixOne.BookStore.ProductService.Tests.Persistance
{
    public interface IProductRepository
    {
        IObservable<IEnumerable<Product>> GetAll();
        IObservable<IEnumerable<Product>> GetAll(IScheduler scheduler);
        Product GetBy(Guid id);
        IObservable<Unit> Remove(Guid id);
        IObservable<Unit> Remove(Guid id, IScheduler scheduler);
        void DeleteProduct(Guid id);
        IEnumerable<Product> GetProducts();
    }
}
