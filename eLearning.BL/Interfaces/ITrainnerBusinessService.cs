

using Common.ViewModel.Common;
using eLearning.Common.Dto;

namespace eLearning.BL.Interfaces
{
    public interface ITrainnerBusinessService
    {
        void Create(ApiResult<TrainnerDto> viewModel);
        void Delete(ApiResult<TrainnerDto> viewModel);
        DataTableViewModel<TrainnerDto> GetAll();
        DataTableViewModel<TrainnerDto> GetAllPaging(int startIndex, int endIndex, int pageSize, int intDraw, string search);
        ApiResult<TrainnerDto> GetById(TrainnerDto dto);
        void Update(ApiResult<TrainnerDto> viewModel);
    }
}