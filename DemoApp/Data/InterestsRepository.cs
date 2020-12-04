using Dapper;
using DemoApp.Configuration;
using DemoApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DemoApp.Data
{
    public class InterestsRepository : IInterestsRepository
    {
		private readonly IDatabaseSettings _databaseSettings;
		public InterestsRepository(IDatabaseSettings databaseSettings)
        {
			_databaseSettings = databaseSettings;
		}

        public void Create(InterestContent interestContent)
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "CreateInterest";
				var values = new { ApiKey = interestContent.ApiKey, Content = interestContent.Content };
				connection.Execute(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}

        public async Task<IEnumerable<InterestRegistration>> GetAll()
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "GetAll";
				return await connection.QueryAsync<InterestRegistration>(procedure, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<InterestRegistration>> GetAllForUser(string userName)
		{
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "GetAllForUser";
				var values = new { UserName = userName };
				return await connection.QueryAsync<InterestRegistration>(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<string> GetContentByTitle(string title)
		{
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "GetContentByTitle";
				var values = new { Title = title };
				return await connection.QuerySingleAsync<string>(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<IEnumerable<string>> GetAllUsers()
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "GetAllUsers";
				return await connection.QueryAsync<string>(procedure, commandType: CommandType.StoredProcedure);
			}
		}

		public async Task<Guid> RegisterInterest(string userName, InterestRegistration interestRegistrationData)
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "RegisterInterest";
				var values = new { UserName = userName, Title = interestRegistrationData.Title, Description = interestRegistrationData.Description};
				return await connection.QuerySingleAsync<Guid>(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}

        public void SubscribeInterest(string userName, InterestRegistration interestRegistration)
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "SubscribeInterest";
				var values = new { UserName = userName, Title = interestRegistration.Title, Description = interestRegistration.Description };
				connection.Execute(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}

        public void UnSubscribeInterest(string userName, InterestRegistration interestRegistration)
        {
			using (var connection = new SqlConnection(_databaseSettings.InterestsConnectionString))
			{
				var procedure = "UnSubscribeInterest";
				var values = new { UserName = userName, Title = interestRegistration.Title };
				connection.Execute(procedure, values, commandType: CommandType.StoredProcedure);
			}
		}
	}
}
