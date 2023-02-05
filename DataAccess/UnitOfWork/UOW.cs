using DataAccess.Data;
using DataAccess.GenericRepository;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
	public class UOW : IUOW, IDisposable
	{
		private DataContext _context;
		public UOW(DataContext context)
		{
			_context = context;
		}

		public GenericRepository<Company> CompanyRepository()
		{
			return new GenericRepository<Company>(_context);
		}
		public GenericRepository<User> UserRepository()
		{
			return new GenericRepository<User>(_context);
		}
		public GenericRepository<Service> ServiceRepository()
		{
			return new GenericRepository<Service>(_context);
		}
		public GenericRepository<Survey> SurveyRepository()
		{
			return new GenericRepository<Survey>(_context);
		}
		public GenericRepository<QuestionType> QuestionTypeRepository()
		{
			return new GenericRepository<QuestionType>(_context);
		}
		public GenericRepository<Question> QuestionRepository()
		{
			return new GenericRepository<Question>(_context);
		}
		public GenericRepository<Customer> CustomerRepository()
		{
			return new GenericRepository<Customer>(_context);
		}
		public GenericRepository<Answer> AnswerRepository()
		{
			return new GenericRepository<Answer>(_context);
		}
		public GenericRepository<Condition> ConditionRepository()
		{
			return new GenericRepository<Condition>(_context);
		}
		public List<Survey> FilterSurvey(Guid ServiceId)
		{
			var survery = _context.Surveys?.Include(x => x.Service).Where(x => x.ServiceId == ServiceId);
			return survery.ToList();
		}
        public void Save()
		{
			_context.SaveChanges();
		}
        public void Dispose()
		{
			_context.Dispose();
		}

	}
}
