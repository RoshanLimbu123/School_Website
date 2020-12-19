using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Makers.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMS.Core.Makers.Implementations
{
    public class ExamTermmakerImpl : ExamTermMaker
    {
        public void copy(ExamTerm exam_Term, ExamtermDto examtermDto)
        {
            exam_Term.exam_term_id = examtermDto.exam_term_id;
            exam_Term.is_active = examtermDto.is_active;
            exam_Term.name = examtermDto.name;
        }
    }
}
