using PureFood.BaseCommands;
using PureFood.ProductCommands.Commands;
using PureFood.ProductCommands.Queries;
using PureFood.ProductReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace PureFood.ProductManager.Shared
{
    [ServiceContract]
    public interface IProductReviewService
    {
        [OperationContract]
        Task<BaseCommandResponse> Add(ProductReviewAddCommand command);
        [OperationContract]
        Task<BaseCommandResponse> Change(ProductReviewChangeCommand command);
        [OperationContract]
        Task<BaseCommandResponse<RProductReview>> Get(ProductReviewGetQuery query);
        [OperationContract]
        Task<BaseCommandResponse<RProductReview[]>> Gets(ProductReviewGetsQuery query);
    }
}
