﻿using Core.DataAccess.EntityFrameWork;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFrameWork
{
    public class EfOrderDal: EfEntityRepositoryBase<Order,NorthwindContext>,IOrderDal
    {
    }
}
