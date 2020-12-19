using CMS.Core.Dto;
using CMS.Core.Entity;
using CMS.Core.Exceptions;
using CMS.Core.Makers.Interface;
using CMS.Core.Repository.Interface;
using CMS.Core.Service.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
   public class RoutineServiceImpl: RoutineService
    {
        private readonly RoutineRepository _routineRepository;
        private readonly RoutineMaker _routineMaker;
        public RoutineServiceImpl(RoutineRepository routineRepository, RoutineMaker routineMaker)
        {
            _routineRepository = routineRepository;
            _routineMaker = routineMaker;
        }

        public void delete(long routine_id)
        {
            try
            {
                using (TransactionScope txe =new TransactionScope(TransactionScopeOption.Required))
                {
                    var routineCategory = _routineRepository.getById(routine_id);
                    if(routineCategory==null)
                    {
                        throw new ItemNotFoundException($"routine category id{routine_id} doesnot exist");
                    }
                    _routineRepository.delete(routineCategory);
                    txe.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }        }

        public void save(RoutineDto routineDto)
        {
            try
            {
                using(TransactionScope txe=new TransactionScope(TransactionScopeOption.Required))
                {
                    var routineCategory = new Routine();
                    _routineMaker.copy(routineCategory,routineDto);
                    _routineRepository.insert(routineCategory);
                    txe.Complete();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }        }

        public void update(RoutineDto routineDto)
        {
            try
            {
                using (TransactionScope txe=new TransactionScope(TransactionScopeOption.Required))
                {
                    Routine routine = _routineRepository.getById(routineDto.routine_id);
                    if(routine==null)
                    {
                        throw new ItemNotFoundException($"routine id{routineDto.routine_id}doesnot exist");
                    }
                    _routineMaker.copy(routine, routineDto);
                    _routineRepository.update(routine);
                    txe.Complete();

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
