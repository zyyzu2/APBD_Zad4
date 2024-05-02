using System.Runtime.CompilerServices;

namespace Zad4.Models;

public class Order
{
    public int IdOrder { get; set; }
    public int IdProduct { get; set; }
    public int Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime FullfilledAt { get; set; }
}