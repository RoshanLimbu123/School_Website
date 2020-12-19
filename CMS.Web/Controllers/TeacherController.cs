using AutoMapper;
using CMS.Core.Repository.Interface;
using CMS.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CMS.Web.Controllers
{
    [Route("teacher")]
    public class TeacherController : Controller
    {
        private readonly ItemCategoryRepository _itemCategoryRepository;
        private readonly TeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherController(ItemCategoryRepository itemCategoryRepository,TeacherRepository teacherRepository,IMapper mapper)
        {
            _itemCategoryRepository = itemCategoryRepository;
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            try
            {
                var teachers = _teacherRepository.getAll();
                if(teachers==null)
                {
                    return View(new TeacherDetail());
                }
                TeacherCategoryViewModel vm = new TeacherCategoryViewModel();
                vm.teachers = new List<TeacherDetail>();
                var teachersList = new TeacherDetail();

                foreach (var teacher in teachers)
                {
                    var data = _mapper.Map<TeacherDetail>(teacher);
                    vm.teachers.Add(data);
                }
                return View(vm);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet]
        [Route("detail/{slug}")]
        public IActionResult detail(string slug)
        {
            try
            {
                var teacherDetails = _teacherRepository.getBySlug(slug);
                if(teacherDetails==null)
                {
                    return View(new TeacherDetail());
                }
                var data = _mapper.Map<TeacherDetail>(teacherDetails);
                return View(data);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
