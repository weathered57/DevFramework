using DevFramework.Northwind.Business.Abtract;
using DevFramework.Northwind.Business.Aspects.Postsharp.TransactionAspect;
using DevFramework.Northwind.Business.Aspects.Postsharp.ValidationAspect;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using DevFramework.Northwind.Business.Aspects.Postsharp.CacheAspect;
using DevFramework.Northwind.Business.Caching.Microsoft;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Product> GetAll()
        {
           return _productDal.GetList();
        }
        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(ProductValidation))]
        public Product Add(Product product)
        {
           return  _productDal.Add(product);
        }
        [FluentValidationAspect(typeof(ProductValidation))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }
        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            _productDal.Add(product2);

        }
    }
}
