using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class ClassesServiceImpl : ClassesService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ClassesRepository _classesRepository;
        private readonly ClassesMaker _classesMaker;
        public ClassesServiceImpl(ClassesRepository classesRepository, ClassesMaker classesMaker, IHostingEnvironment hostingEnvironment)
        {
          
            _classesRepository = classesRepository;
            _classesMaker = classesMaker;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long class_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var classCategory = _classesRepository.getById(class_id);
                    if (classCategory == null)
                    {
                        throw new ItemNotFoundException($"faculty Category With Id {classCategory} doesnot Exist.");
                    }

                    _classesRepository.delete(classCategory);
                    tx.Complete();
                }
            }

            catch (Exception)
            {

                throw;
            }
        }

        public void save(ClassesDto classesDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Classes classes = new Classes();
                    _classesMaker.copy(classes, classesDto);
                    _classesRepository.insert(classes);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }



        public void update(ClassesDto classesDto)
        {
            using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
            {
                Classes classes = _classesRepository.getById(classesDto.class_id);
                if (classes == null)
                {
                    throw new ItemNotFoundException($"Classes with ID {classesDto.class_id} doesnot Exit.");
                }

                _classesMaker.copy(classes, classesDto);
                _classesRepository.update(classes);
                tx.Complete();
            }
        }
    }
}
