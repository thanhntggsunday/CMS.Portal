namespace Common.ViewModel.Common
{
    public class ApiResult<T> : TransactionalInformation
    {
        public T ResultObj { get; set; }
    }
}