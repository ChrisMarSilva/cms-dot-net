using System.ComponentModel.DataAnnotations;

namespace VShop.DiscountApi.Dtos;

public class CouponDto
{
    public int CouponId { get; set; }

    [Required] 
    public string? CouponCode { get; set; }

    [Required] 
    public decimal Discount { get; set; }
}
