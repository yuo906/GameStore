using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

// public record class UpdateGameDto(
//     [Required][StringLength(50)] string Name,
//     // [Required][StringLength(20)] string Genre,
//     int GenreId,
//     [Range(1, 100)] decimal Price,
//     DateOnly ReleaseDate
// );

public record class UpdateGameDto
{
    /// <summary>
    /// 名稱
    /// </summary>
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    /// <summary>
    /// 類別ID
    /// </summary>
    [Required]
    public required int GenreId { get; set; }

    /// <summary>
    /// 價格
    /// </summary>
    [Range(1, 100)]
    public decimal Price { get; set; }

    /// <summary>
    /// 發行日期
    /// </summary>
    public DateOnly ReleaseDate { get; set; }
}