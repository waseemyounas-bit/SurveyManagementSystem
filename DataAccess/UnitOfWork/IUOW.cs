using DataAccess.GenericRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUOW
    {
        GenericRepository<User> UserRepository(); 
        GenericRepository<Company> CompanyRepository();
        GenericRepository<Service> ServiceRepository();
        GenericRepository<Survey> SurveyRepository();
        GenericRepository<QuestionType> QuestionTypeRepository();
        GenericRepository<Question> QuestionRepository();
        GenericRepository<Customer> CustomerRepository();
        GenericRepository<Answer> AnswerRepository();
        GenericRepository<Condition> ConditionRepository();
        List<Survey> FilterSurvey(Guid ServiceId);
        void Save();
        void Dispose();
       
    }
}
