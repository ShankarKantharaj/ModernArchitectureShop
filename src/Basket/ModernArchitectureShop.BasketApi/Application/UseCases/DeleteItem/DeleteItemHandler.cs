using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ModernArchitectureShop.Basket.Application.Persistence;
using ModernArchitectureShop.BasketApi.Infrastructure.Dapr.Publishers;
using ModernArchitectureShop.BasketApi.Infrastructure.Dapr.Publishers.Messages;

namespace ModernArchitectureShop.BasketApi.Application.UseCases.DeleteItem
{
    public class DeleteItemHandler : IRequestHandler<DeleteItemCommand, bool>
    {
        private readonly IItemRepository _itemRepository;
        private readonly BasketItemNotificationHandler _basketItemNotificationHandler;

        public DeleteItemHandler(IItemRepository itemRepository, BasketItemNotificationHandler basketItemNotificationHandler)
        {
            _itemRepository = itemRepository;
            _basketItemNotificationHandler = basketItemNotificationHandler;
        }

        public async Task<bool> Handle(DeleteItemCommand command, CancellationToken cancellationToken)
        {
            await _itemRepository.RemoveAsync(command.ItemId, cancellationToken);

            var result = await _itemRepository.SaveChangesAsync(cancellationToken);

            await _basketItemNotificationHandler.Handle(new BasketItemDeletedMessage
            {
                ItemId = command.ItemId
            },
                cancellationToken);

            return result > 0;
        }
    }
}
