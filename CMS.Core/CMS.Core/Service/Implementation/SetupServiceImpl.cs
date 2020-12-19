using CMS.Core.Entity;
using CMS.Core.Helper;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("CMS.Web")]
namespace CMS.Core.Service.Implementation
{
    public class SetupServiceImpl : SetupService
    {
        private readonly TransactionManager _transactionManager;
        private readonly SetupRepository _setupRepo;

        public SetupServiceImpl(TransactionManager transactionManager, SetupRepository setupRepo)
        {
            _setupRepo = setupRepo;
            _transactionManager = transactionManager;
        }

        public void saveOrUpdate(List<Setup> keyValue)
        {
            try
            {
                _transactionManager.beginTransaction();
                foreach (var kvp in keyValue)
                {
                    saveOrUpdate(kvp.key, kvp.value);
                }
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        public void saveOrUpdate(string key, string value)
        {
            try
            {
                _transactionManager.beginTransaction();
                var setup = _setupRepo.getByKey(key);

                if (setup == null)
                {
                    save(key, value);
                }
                else
                {
                    update(setup, value);
                }
                _transactionManager.commitTransaction();
            }
            catch (Exception)
            {
                _transactionManager.rollbackTransaction();
                throw;
            }
        }

        private void update(Setup setup, string value)
        {
            setup.value = value;
            _setupRepo.update(setup);
        }

        private void save(string key, string value)
        {
            Setup setup = new Setup();
            setup.key = key;
            setup.value = value;
            _setupRepo.insert(setup);
        }
    }
}
