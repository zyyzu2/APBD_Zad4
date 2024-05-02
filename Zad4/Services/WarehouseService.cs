using Microsoft.JSInterop.Infrastructure;
using Zad4.DTOs;
using Zad4.Exceptions;
using Zad4.Models;
using Zad4.Repos;

namespace Zad4.Services;

public class WarehouseService(
    IWarehouseRepository warehouseRepository,
    IOrderService orderService,
    IProductService productService)
    : IWarehouseService
{
    public async Task<int?> RegisterProductAsync(WarehouseDto dto)
    {
        if (dto.Amount <= 0) throw new ArgumentException("Amount must be greater than 0");
        if (await productService.CheckProductPresence(dto.IdProduct) <= 0) throw new NotFoundException("Product with provided ID not exists");
        if (await warehouseRepository.CheckWarehousePresence(dto.IdWarehouse) <= 0) throw new NotFoundException("Warehouse with provided ID not exists");
        if (await orderService.CheckOrderPresence(dto.IdProduct, dto.Amount, dto.CreatedAt) <= 0) throw new NotFoundException("Cannot find order with provided data");
        var order = await orderService.FindOrderByDataAsync(dto.IdProduct, dto.Amount, dto.CreatedAt);
        if (await CheckFulfilledStatus(order!.IdOrder)) throw new ConflictException("This order was fulfilled");
        return await warehouseRepository.RegisterProductInWarehouseAsync(dto.IdWarehouse, dto.IdProduct, order.IdOrder, dto.Amount);
    }

    public async Task<bool> CheckFulfilledStatus(int idOrder)
    {
        return await warehouseRepository.CheckFulfilledStatusAsync(idOrder);
    }
}