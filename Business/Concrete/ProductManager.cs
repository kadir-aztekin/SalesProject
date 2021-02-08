using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }

        public List<Product> GetAllByCategory(int ıd)
        {
            return _productDal.GetAll(c => c.CategoryId == ıd);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
//BEN DIYORUM Kİ DATAACCESSE SOR BAKALIM  IZNI VARMI  GİBİ DÜŞÜNEBİLİRİZ
// OYUZDEN CONSTRUCTOR YAPIYRUZ _PRODUCTDAL ORDAN VERİ ALIYOR 