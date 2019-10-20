using DevFramework.Northwind.Business.Abtract;
using DevFramework.Northwind.Business.Aspects.Postsharp;
using DevFramework.Northwind.Business.ValidationRules.FluentValidation;
using DevFramework.Northwind.DataAccess.Abstract;
using DevFramework.Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFramework.Northwind.Business.Concrete.Managers
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
           return _productDal.GetList();
        }
        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }
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
    }
}
