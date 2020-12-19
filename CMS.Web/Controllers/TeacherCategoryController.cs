using AutoMapper;
using CMS.Core.Dto;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Web.Controllers
{
    [Route("teacher-category")]
    public class TeacherCategoryController : Controller
    {
        private readonly ItemCategoryRepository _itemCategoryRepository;
        private readonly TeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

       
        public TeacherCategoryController(ItemCategoryRepository itemCategoryRepository,TeacherRepository teacherRepository,IMapper mapper)
        {
            _itemCategoryRepository = itemCategoryRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        [Route("{slug}")]
        public IActionResult Index(string slug)
        {
            TeacherCategoryViewModel vm = new TeacherCategoryViewModel();
            vm.teachers = new List<TeacherDetail>();
            var itemCategory = _itemCategoryRepository.getBySlug(slug);
            if(itemCategory==null)
            {
                vm.category_details = new ItemCategoryDto();
                return View(vm);
            }
            var teachers = itemCategory.teachers;
            foreach(var teacher in teachers)
            {
                vm.teachers.Add(_mapper.Map<TeacherDetail>(teacher));
            }
            vm.category_details = _mapper.Map<ItemCategoryDto>(itemCategory);
            return View(vm);
        }
    }
}
