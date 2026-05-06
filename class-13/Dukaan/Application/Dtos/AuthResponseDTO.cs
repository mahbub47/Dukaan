namespace Dukaan.Application.Dtos;

public record AuthResponseDTO(
    string Token,
    DateTime Expiration
);
