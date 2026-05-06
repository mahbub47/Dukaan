namespace Dukaan.Application.Dtos;

public record LoginRequestDTO(
    string Email,
    string Password
);