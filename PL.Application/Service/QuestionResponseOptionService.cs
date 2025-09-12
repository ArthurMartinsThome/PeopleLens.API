using PL.Adapter.PostgreSQL.Interface;
using PL.Application.Interface;
using PL.Domain.Model.Enum;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Model.Filter;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Service
{
    public class QuestionResponseOptionService : IQuestionResponseOptionService
    {
        private readonly IQuestionResponseOptionDataSource _dataSource;

        public QuestionResponseOptionService(IQuestionResponseOptionDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        private IEnumerable<Filter> GetFilters(QuestionResponseOptionFilter filter)
        {
            var filterList = new List<Filter>();
            if (filter == null)
                return filterList;
            if (filter.Id.HasValue && filter.Id > 0)
                filterList.Add(Filter.Create("Id", EOperator.Equal, new[] { filter.Id.Value }));
            if (filter.QuestionId.HasValue && filter.QuestionId > 0)
                filterList.Add(Filter.Create("QuestionId", EOperator.Equal, filter.QuestionId.Value));
            if (filter.ResponseTypeProfileId.HasValue && filter.ResponseTypeProfileId > 0)
                filterList.Add(Filter.Create("ResponseTypeProfileId", EOperator.Equal, filter.ResponseTypeProfileId.Value));
            if (filter.Weight.HasValue && filter.Weight > 0)
                filterList.Add(Filter.Create("Weight", EOperator.Equal, filter.Weight.Value));
            return filterList;
        }

        public async Task<IResult<Paged<Domain.Model.QuestionResponseOption>>> SearchPaged(QuestionResponseOptionFilter filter, int skip, int take)
        {
            try
            {
                var result = await _dataSource.SearchPaged(GetFilters(filter), skip, take);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<Paged<Domain.Model.QuestionResponseOption>>.Error("Houve uma falha ao buscar os dados da(s) opção(ões) de resposta da questão.", ex);
            }
        }

        public async Task<IResult<IEnumerable<Domain.Model.QuestionResponseOption>>> Search(QuestionResponseOptionFilter filter)
        {
            try
            {
                var filters = GetFilters(filter);
                var result = await _dataSource.Search(filters);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<IEnumerable<Domain.Model.QuestionResponseOption>>.Error("Houve uma falha ao buscar os dados da opção de resposta da questão.", ex);
            }
        }

        public async Task<IResult<int>> Insert(Domain.Model.QuestionResponseOption obj)
        {
            try
            {
                var result = await _dataSource.Insert(obj);
                return result;
            }
            catch (Exception ex)
            {
                return DefaultResult<int>.Error("Houve uma falha ao cadastrar a opção de resposta da questão.", ex);
            }
        }

        public async Task<IResult<bool>> Update(Domain.Model.QuestionResponseOption obj)
        {
            try
            {
                if (!obj.Id.HasValue || obj.Id <= 0)
                    return DefaultResult<bool>.Error("Falta referenciar qual o id para editar a opção de resposta da questão.", new Exception($"Update: newObj.Id == (null/0)"));

                var searchResult = await Search(new QuestionResponseOptionFilter { Id = obj.Id });
                if (!searchResult.Succeded || searchResult.Data == null)
                    return DefaultResult<bool>.Break("Opção de resposta da questão não encontrada para atualização.");

                var oldObj = searchResult.Data.First();
                var newObj = (Domain.Model.QuestionResponseOption)searchResult.Data.First().Clone();

                if (obj.QuestionId != null && obj.QuestionId > 0)
                    newObj.QuestionId = obj.QuestionId;
                if (!string.IsNullOrEmpty(obj.Text))
                    newObj.Text = obj.Text;
                if (obj.ResponseTypeProfileId != null && obj.ResponseTypeProfileId > 0)
                    newObj.ResponseTypeProfileId = obj.ResponseTypeProfileId;
                if (obj.Weight != null && obj.Weight > 0)
                    newObj.Weight = obj.Weight;

                var filters = GetFilters(new QuestionResponseOptionFilter() { Id = obj.Id });
                return await _dataSource.Update(filters, oldObj, newObj);
            }
            catch (Exception ex)
            {
                return DefaultResult<bool>.Error("Houve uma falha ao editar a opção de resposta da questão.", ex);
            }
        }
    }
}