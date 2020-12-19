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
using System.Text;
using System.Transactions;

namespace CMS.Core.Service.Implementation
{
    public class MembersServiceImpl : MembersService
    {
        private readonly MembersRepository _membersRepository;
        private readonly MembersMaker _membersMaker;
        private readonly IHostingEnvironment _hostingEnvironment;

        public MembersServiceImpl(MembersRepository membersRepository, MembersMaker membersMaker, IHostingEnvironment hostingEnvironment)
        {
            _membersRepository = membersRepository;
            _membersMaker = membersMaker;
            _hostingEnvironment = hostingEnvironment;
        }
        public void delete(long member_id)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    var members = _membersRepository.getById(member_id);

                    if (members == null)
                    {
                        throw new ItemNotFoundException($"Members category with id {member_id} doesnot exist.");
                    }

                    _membersRepository.delete(members);
                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void save(MemberDto member_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Member member = new Member();
                    _membersMaker.copy(ref member, member_dto);
                    _membersRepository.insert(member);

                    tx.Complete();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void update(MemberDto member_dto)
        {
            try
            {
                using (TransactionScope tx = new TransactionScope(TransactionScopeOption.Required))
                {
                    Member member = _membersRepository.getById(member_dto.member_id);
                    if (member == null)
                    {
                        throw new ItemNotFoundException($"Member with ID {member_dto.member_id} doesnot Exit.");
                    }
                    string oldImage = member.image_url;
                    if (!string.IsNullOrWhiteSpace(member_dto.image_url))
                    {
                        if (!string.IsNullOrWhiteSpace(oldImage))
                        {
                            deleteImage(oldImage);
                        }
                    }
                    _membersMaker.copy(ref member, member_dto);
                    _membersRepository.update(member);

                    tx.Complete();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void deleteImage(string image_path)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images/custom");
            if (File.Exists(Path.Combine(filePath, image_path)))
            {
                File.Delete(Path.Combine(filePath, image_path));

            }
        }
    }
}
