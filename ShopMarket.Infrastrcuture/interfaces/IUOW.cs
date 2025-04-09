﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMarket.Infrastrcuture.interfaces
{
    public interface IUOW : IDisposable
    {
        IGenericInterface<T> GenericRepository<T>() where T : class;
        Task<int> Complete();
    }
}
