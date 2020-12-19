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
    public class FacultyServiceImpl : FacultyService
    {
        private readonly FacultyMaker _facultyMaker;
        private FacultyRepository _facultyRepository;
        private IHostingEnvironment _hostingEnvironment;

        public FacultyServiceImpl(FacultyMaker facultyMaker, FacultyRepository facultyRepository, IHostingEnvironment hostingEnvironment)
        {
            _facultyMaker = facultyMaker;
            _facultyRepository = facultyRepository;
            _hostingEnvironment = hostingEnvironment;
         
        }
        public void delete(long faculty_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var FacultyCategory = _facultyRepository.getById(faculty_id);
                    if (FacultyCategory == null)
                    {
                        throw new ItemNotFoundException($"faculty Category With Id {FacultyCategory} doesnot Exist.");
                    }

                    _facultyRepository.delete(FacultyCategory);
                    tx.Complete();
                }
            }
                
            catch (Exception)
            {

                throw;
            }
        }

        public void save(FacultyDto facultyDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    Faculty faculty = new Faculty();
                    var designation_position = _facultyRepository.getAll();

                    _facultyMaker.copy( faculty, facultyDto);
                    _facultyRepository.insert(faculty);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(FacultyDto facultyDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    Faculty faculty = _facultyRepository.getById(facultyDto.faculty_id);

                    if (faculty == null)
                    {
                        throw new ItemNotFoundException($"faculty with ID {facultyDto.faculty_id} doesnot Exit.");
                    }

                    _facultyMaker.copy(faculty, facultyDto);
                    _facultyRepository.update(faculty); ;
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
    }

