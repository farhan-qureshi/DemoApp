using DemoApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoApp.Data
{
    public interface IInterestsRepository
    {
        Task<Guid> RegisterInterest(string userName, InterestRegistration interestRegistration);
        void Create(InterestContent interestContent);
        void SubscribeInterest(string userName, InterestRegistration interestRegistration);
        void UnSubscribeInterest(string userName, InterestRegistration interestRegistration);
        Task<IEnumerable<InterestRegistration>> GetAll();
        Task<IEnumerable<InterestRegistration>> GetAllForUser(string userName);
        Task<string> GetContentByTitle(string title);
        Task<IEnumerable<string>> GetAllUsers();
    }
}