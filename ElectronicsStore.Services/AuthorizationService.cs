using AutoMapper;
using ElectronicsStore.Data.Entities;
using ElectronicsStore.Data.Interfaces;
using ElectronicsStore.Services.Exceptions;
using ElectronicsStore.Services.Interfaces;
using ElectronicsStore.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElectronicsStore.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IElectronicsStoreUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AuthorizationService(
        IElectronicsStoreUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordHasher<User> passwordHasher,
        AuthenticationSettings authenticationSettings)
    {
        _passwordHasher = passwordHasher;
        _authenticationSettings = authenticationSettings;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task RegisterUser(RegisterDto dto, int roleId = 1)
    {
        var newUser = new User
        {
            Email = dto.Email,
            RoleId = roleId
        };

        var hash = _passwordHasher.HashPassword(newUser, dto.Password);
        newUser.PasswordHash = hash;

        await _unitOfWork.Users.AddAsync(newUser);
        await _unitOfWork.CommitAsync();
    }

    public async Task<string> GenerateJwt(LoginDto dto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        if (user == null)
            throw new BadRequestException("Invalid email or password");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new BadRequestException("Invalid email or password");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name),
            new Claim(ClaimTypes.Email, user.Email),
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(
            _authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            claims: claims,
            expires: expires,
            signingCredentials: credentials);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserDto> GetUser(LoginDto dto)
    {
        var token = await GenerateJwt(dto);
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email);
        var userDto = _mapper.Map<UserDto>(user);
        userDto.Token = token;
        return userDto;
    }
}
