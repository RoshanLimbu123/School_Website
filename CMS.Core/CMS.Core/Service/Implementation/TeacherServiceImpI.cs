using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class TeacherServiceImpI : TeacherService
    {
        private readonly TeacherRepository _teacherRepo;
        private readonly TeacherMaker _teacherMaker;
        private readonly ItemCategoryRepository _itemCategoryRepo;
        private readonly IHostingEnvironment _hostingEnvironment;

        public TeacherServiceImpI(TeacherRepository teacherRepo,TeacherMaker teacherMaker,ItemCategoryRepository itemCategoryRepo,  IHostingEnvironment hostingEnvironment)
        {
            _teacherRepo = teacherRepo;
            _teacherMaker = teacherMaker;
            _itemCategoryRepo = itemCategoryRepo;
            _hostingEnvironment = hostingEnvironment;
        }

        public void delete(long teacher_id)
        {
            try
            {
                using(TransactionScope  tx= new TransactionScope(TransactionScopeOption.Required))
                {
                    var teacher = _teacherRepo.getById(teacher_id);
                    if(teacher==null)
                    {
                        throw new ItemNotFoundException($"{teacher_id} not found");
                    }
                    _teacherRepo.delete(teacher);
                    deleteImage(teacher.file_name);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void disable(long teacher_id)
        {
            try
            {
                var teacher = _teacherRepo.getById(teacher_id);
                if(teacher==null)
                {
                    throw new ItemNotFoundException($"{teacher_id} not found");
                }
                teacher.disable();
                _teacherRepo.update(teacher);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void enable(long teacher_id)
        {
            try
            {
                var teacher = _teacherRepo.getById(teacher_id);
                if(teacher==null)
                {
                    throw new ItemNotFoundException($"{teacher_id} not found");
                }
                teacher.enable();
                _teacherRepo.update(teacher);
            }
            catch (Exception exp)
            {

                throw exp;
            } 
        }

        public void save(TeacherDto teacherDto)
        {
            try
            {
                using(TransactionScope tx= new TransactionScope(TransactionScopeOption.Required))
                {
                    bool isNameValid = checkNameValidity(teacherDto);
                    if(!isNameValid)
                    {
                        throw new DuplicateItemException("Teacher With Same Name already exist");
                    }
                    Teacher teacher = new Teacher();
                    _teacherMaker.copy(ref teacher, teacherDto);
                    if(teacher.item_category_id !=0)
                    {
                        teacher.item_category = _itemCategoryRepo.getById(teacher.item_category_id) ?? throw new ItemNotFoundException($"Item Category with the id{teacher.item_category_id} doesnot exist");
                    }
                    _teacherRepo.insert(teacher);
                    tx.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void update(TeacherDto teacherDto)
        {
            try
            {
                using(TransactionScope tx=new TransactionScope(TransactionScopeOption.Required))
                {
                    bool isNameValid = checkNameValidity(teacherDto);
                    if(!isNameValid)
                    {
                        throw new DuplicateItemException("Teacher with same name already exist");
                    }
                    Teacher teacher = _teacherRepo.getById(teacherDto.teacher_id);
                    string oldImage = teacher.file_name;
                    _teacherMaker.copy( ref teacher, teacherDto);
                    if(teacher.item_category_id !=0)
                    {
                        teacher.item_category = _itemCategoryRepo.getById(teacher.item_category_id) ?? throw new ItemNotFoundException($"Item Category with the  id{teacher.item_category_id} doesnot exist");
                    }
                    _teacherRepo.update(teacher);
                    if(!string.IsNullOrWhiteSpace(teacherDto.file_name))
                    {
                        if(!string.IsNullOrWhiteSpace(oldImage))
                        {
                            deleteImage(oldImage);
                        }
                    }
                }
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }
        private void deleteImage(string file_name)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if(File.Exists(Path.Combine(filePath,file_name)))
            {
                File.Delete(Path.Combine(filePath, file_name));
            }
        }
        private bool checkNameValidity(TeacherDto teacherDto)
        {
            List<Teacher> teacherWithSameName = _teacherRepo.getByName(teacherDto.name);
            var teacherWithSameNameInSameCategory = teacherWithSameName.Where(a => a.item_category_id == teacherDto.item_category_id).SingleOrDefault();
            if(teacherWithSameNameInSameCategory==null || teacherWithSameNameInSameCategory.teacher_id==teacherDto.teacher_id)
            {
                return true;
            }
            return false;
        }
      
    }
}
