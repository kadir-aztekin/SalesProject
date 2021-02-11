﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult  Add(Product product)
        {
            if (product.ProductName.Length<2)
            {
                //magic strings ("  " yazılanlara  denir) 
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MainintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategory(int ıd)
        {
            return   new SuccessDataResult<List<Product>>(_productDal.GetAll(c => c.CategoryId == ıd));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return  new SuccessDataResult<Product> (_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        
    }
}
//BEN DIYORUM Kİ DATAACCESSE SOR BAKALIM  IZNI VARMI  GİBİ DÜŞÜNEBİLİRİZ
// OYUZDE N CONSTRUCTOR YAPIYRUZ _PRODUCTDAL ORDAN VERİ ALIYOR 