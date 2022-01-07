using System;
using System.Data;
using PorterGroup.Desafio.Business.Abstractions.Data;
using PorterGroup.Desafio.Infra.Logger.Logging;
using PorterGroup.Desafio.Shared.Exceptions;

namespace PorterGroup.Desafio.Infra.Data.Context
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ILogWriter _logger;

        private int _transactionCounter;
        private bool _disposedValue;

        public UnitOfWork(
            ILogWriter logger,
            IDbConnection connection)
        {
            _logger = logger;

            if (connection.State is not ConnectionState.Open)
            {
                connection.Open();
            }

            Connection = connection;
        }

        public IDbConnection Connection { get; }

        public IDbTransaction Transaction { get; protected set; }

        public void BeginTransaction()
        {
            if (_transactionCounter == 0)
            {
                Transaction = Connection.BeginTransaction();
            }

            _transactionCounter++;
        }

        public void Commit()
        {
            try
            {
                TryCommit();
            }
            catch (NotOpenTransactionException)
            {
                throw;
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            if (Transaction is null)
            {
                return;
            }

            _transactionCounter = 0;
            Transaction.Rollback();
            ClearTransaction();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                LogLostedTransaction();

                if (disposing)
                {
                    Transaction?.Dispose();
                    Connection?.Dispose();
                }

                _disposedValue = true;
            }
        }

        private void TryCommit()
        {
            if (Transaction is null || _transactionCounter < 0)
            {
                throw new NotOpenTransactionException("Commit");
            }

            _transactionCounter--;
            if (_transactionCounter > 0)
            {
                return;
            }

            Transaction.Commit();

            ClearTransaction();
        }

        private void ClearTransaction()
        {
            Transaction.Dispose();
            Connection.Close();
        }

        private void LogLostedTransaction()
        {
            if (_transactionCounter == 0)
            {
                return;
            }

            _logger.Warn("There's a transaction pedding!");
        }
    }
}
