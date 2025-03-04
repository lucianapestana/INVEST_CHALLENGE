using INVEST.API.Repository.Interfaces;
using INVEST.API.Service.Interfaces;
using Moq;

namespace INVEST.API.TEST.Repository
{
    public class ApiMockRepository
    {
        #region [ SERVICE ]
        
        public Mock<IAccountClientService> AccountClientService { get; set; }

        public Mock<IProductService> ProductService { get; set; }

        #endregion [ SERVICE ]

        #region [ REPOSITORY ]

        public Mock<IAccountClientRepository> AccountClientRepository { get; set; }

        public Mock<IClientRepository> ClientRepository { get; set; }
        
        public Mock<IOrderRepository> OrderRepository { get; set; }

        public Mock<IProductRepository> ProductRepository { get; set; }

        #endregion [ REPOSITORY ]

        public ApiMockRepository() {

            #region [ SERVICE ]

            AccountClientService = new Mock<IAccountClientService>();

            ProductService = new Mock<IProductService>();

            #endregion [ SERVICE ]

            #region [ REPOSITORY ]

            AccountClientRepository = new Mock<IAccountClientRepository>();

            ClientRepository = new Mock<IClientRepository>();
            
            OrderRepository = new Mock<IOrderRepository>();

            ProductRepository = new Mock<IProductRepository>();
            
            #endregion [ REPOSITORY ]
        }
    }
}
