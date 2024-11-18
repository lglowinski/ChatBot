using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatBot.Users.Infrastructure.Persistance;

public class UsersDbContext(DbContextOptions<UsersDbContext> options) : IdentityUserContext<IdentityUser>(options);