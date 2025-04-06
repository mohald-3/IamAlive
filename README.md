IamAlive/
│
├── IamAlive.csproj
├── Program.cs
├── appsettings.json
│
├── 📁 Models/
│   ├── User.cs
│   ├── CheckIn.cs
│   ├── Friendship.cs
│
├── 📁 Data/
│   ├── AppDbContext.cs
│   └── Seed/FakeDataSeeder.cs
│
├── 📁 DTOs/
│   ├── UserDtos/
│   │   ├── UserDto.cs
│   │   ├── UserCreateDto.cs
│   │   ├── LoginDto.cs
│   │   ├── LoginResponseDto.cs
│   ├── CheckInDtos/
│   │   ├── CheckInDto.cs
│   ├── FriendshipDtos/
│       ├── FriendshipDto.cs
│
├── 📁 Services/
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   ├── ICheckInService.cs
│   │   ├── IAuthService.cs
│   ├── Implementations/
│       ├── UserService.cs
│       ├── CheckInService.cs
│       ├── AuthService.cs
│
├── 📁 Controllers/
│   ├── UserController.cs
│   ├── CheckInController.cs
│   ├── AuthController.cs
│
├── 📁 Mapping/
│   └── AutoMapperProfile.cs
│
├── 📁 Validators/
│   ├── UserValidators/
│   │   ├── UserCreateDtoValidator.cs
│   │   ├── LoginDtoValidator.cs
│   ├── CheckInValidators/
│       ├── CheckInDtoValidator.cs
│
├── 📁 Middleware/ (Optional)
│   └── ExceptionHandlingMiddleware.cs
│
├── 📁 Utils/ (Optional)
│   ├── JwtOptions.cs
│   └── Extensions.cs
