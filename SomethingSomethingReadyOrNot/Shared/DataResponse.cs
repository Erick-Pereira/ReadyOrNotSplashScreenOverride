using System.Collections.Generic;

namespace SomethingSomethingReadyOrNot.Shared
{
    public class DataResponse<T> : Response
    {
        public List<T> Data { get; set; }
    }
}