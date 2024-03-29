﻿using System;
using System.Data;

namespace PorterGroup.Desafio.Business.Abstractions.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection Connection { get; }

        IDbTransaction Transaction { get; }

        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}
