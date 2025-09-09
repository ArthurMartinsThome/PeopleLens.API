using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface IResponseOptionService
    {
        Task<IResult<Paged<ResponseOption>>> SearchPaged(ResponseOptionFilter filter, int skip, int take);
        Task<IResult<IEnumerable<ResponseOption>>> Search(ResponseOptionFilter filter);
        Task<IResult<int>> Insert(ResponseOption obj);
        Task<IResult<bool>> Update(ResponseOption obj);
    }
}