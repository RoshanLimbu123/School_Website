using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Implementations;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
  public class ExamTermServiceImpl : ExamTermService
    {
        private readonly ExamTermRepository _exam_TermRepository;
        private readonly ExamTermMaker _examTermmaker;

        public ExamTermServiceImpl(ExamTermMaker examTermmaker, ExamTermRepository exam_TermRepository)
        {
            _examTermmaker = examTermmaker;
            _exam_TermRepository = exam_TermRepository;

        }
      

        public void delete(long exam_term_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var ExamCategory = _exam_TermRepository.getById(exam_term_id);
                    if (ExamCategory == null)
                    {
                        throw new ItemNotFoundException($"exa category with id(exam_term_id) doesnt exist");
                }
                    _exam_TermRepository.delete(ExamCategory);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void update(ExamtermDto examtermDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {


                    ExamTerm examterm = _exam_TermRepository.getById(examtermDto.exam_term_id);

                    if (examterm == null)
                    {
                        throw new ItemNotFoundException($"exam term with ID {examtermDto.exam_term_id} doesnot Exit.");
                    }

                    _examTermmaker.copy(examterm, examtermDto);
                    _exam_TermRepository.update(examterm); ;
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(ExamtermDto examtermDto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {

                    ExamTerm exam_Term = new ExamTerm();
                    var designation_position = _exam_TermRepository.getAll();

                    _examTermmaker.copy(exam_Term, examtermDto);
                    _exam_TermRepository.insert(exam_Term);

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
