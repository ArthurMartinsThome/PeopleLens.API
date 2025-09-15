using PL.Domain.Dto.CreateTestDto;
using PL.Domain.Dto.TestFull;
using PL.Domain.Model;
using PL.Domain.Model.Filter;
using PL.Infra.DefaultResult.Interface;
using PL.Infra.Util.Model.Paged;

namespace PL.Application.Interface
{
    public interface ITestService
    {
        Task<IResult<Paged<Test>>> SearchPaged(TestFilter filter, int skip, int take);
        Task<IResult<IEnumerable<Test>>> Search(TestFilter filter);
        Task<IResult<int>> Insert(Test obj);
        Task<IResult<bool>> Update(Test obj);
        Task<IResult<TestFullResponseDto>> GetTestFull(int testId);
        Task<IResult<bool>> CreateTestFull(TestCreateRequestDto obj);
    }
}